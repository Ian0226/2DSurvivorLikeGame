using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.CustomTool;
using System.Threading.Tasks;
using System;
using UnityEngine.Pool;

public class PlayerController : GameSystemBase
{
    private Transform playerTransform;

    private Action playerBehaviourAction = null;

    #region 玩家數值

    /// <summary>
    /// 玩家移動速度
    /// </summary>
    private float moveSpeed = 5f;

    /// <summary>
    /// 攻擊CD時間，單位為毫秒
    /// </summary>
    private int attackCDTime = 10;//修改這個來改變CD的快慢，數值越大越慢，越小越快
    private int attackCDMax = 1;
    private float currentAttackCD = 0;
    private bool canAttack = false;

    /// <summary>
    /// 玩家總分數
    /// </summary>
    private long playerScore = 0;

    /// <summary>
    /// 玩家獲得能力三選一需要累積的分數
    /// </summary>
    private int playerSkillScore = 0;

    /// <summary>
    /// 玩家當前累積分數，累積到一定分數即可獲得三選一能力
    /// </summary>
    private int pyCurrentAccumulateScore = 0;

    /// <summary>
    /// 玩家血量
    /// </summary>
    private int hp = 0;

    #endregion

    #region Vectors
    /// <summary>
    /// 玩家游標位置
    /// </summary>
    private Vector2 mousePos;

    /// <summary>
    /// 玩家當前位置
    /// </summary>
    private Vector2 playerPos;

    /// <summary>
    /// 玩家瞄準位置
    /// </summary>
    private Vector2 aimDirection;
    #endregion

    #region 攻擊相關
    /// <summary>
    /// 玩家當前擁有的技能
    /// </summary>
    private List<SkillBase> playerCurrentSkills = null;
    /// <summary>
    /// 玩家當前擁有的被動技能
    /// </summary>
    private List<SkillBase> playerCurrentPassiveSkills = null;

    /// <summary>
    /// 玩家攻擊模式
    /// </summary>
    public enum PlayerAttackModeEnum
    {
        projectile,
        ray,
        other
    }
    private PlayerAttackModeEnum playerAttackMode;

    /// <summary>
    /// 玩家當前使用的投射物
    /// </summary>
    private ProjectileBase playerCurrentProjectile = null;

    /// <summary>
    /// 玩家可以射的投射物數量，預設為1
    /// </summary>
    private int projectilesCount = 1;

    /// <summary>
    /// 玩家攻擊狀態
    /// </summary>
    private bool attackeState = false;

    #endregion

    //類別成員
    private InputManager inputManager = null;
    private PlayerEffectHandler _playerEffectHandler = null;
    private PlayerShootHandler _shootProjectileHandler = null;

    private RaycastHit2D rayHitX;
    private RaycastHit2D rayHitY;

    private int boundaryLayerMask = 0;//用於檢測邊界射線的Layer Mask

    public bool CanAttack { get => canAttack; set => canAttack = value; }
    public Vector2 MousePos { get => mousePos;}
    public Vector2 PlayerPos { get => playerPos;}
    public ProjectileBase PlayerCurrentProjectile { get => playerCurrentProjectile; set => playerCurrentProjectile = value; }
    public PlayerAttackModeEnum PlayerAttackMode { get => playerAttackMode; set => playerAttackMode = value; }
    public int ProjectilesCount { get => projectilesCount; set => projectilesCount = value; }
    public Vector2 AimDirection { get => aimDirection;}
    public int Hp { get => hp; set => hp = value; }
    public long PlayerScore { get => playerScore;}
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Action PlayerBehaviourAction { get => playerBehaviourAction; set => playerBehaviourAction = value; }
    public int AttackCDTime { get => attackCDTime; set => attackCDTime = value; }
    public Transform PlayerTransform { get => playerTransform;}
    public bool AttackeState { get => attackeState; set => attackeState = value; }

    public PlayerController(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        inputManager = SurvivorLikeGame2DFacade.Instance.GetInputManager();

        playerTransform = UnityTool.FindGameObject("Player").transform;

        InitProperties();

        _shootProjectileHandler = new PlayerShootHandler(this);
        _playerEffectHandler = new PlayerEffectHandler(this);
        InitActions();

    }

    public override void Update()
    {
        if(playerBehaviourAction != null)
            playerBehaviourAction();

        if(attackeState)
            HandleAttack();

        _playerEffectHandler.Update();
        //SetVectors();

        //HandleMovement();
        //HandleLookDir();
    }

    private void InitProperties()
    {
        attackCDMax = 1;
        currentAttackCD = attackCDMax;
        attackCDTime = 7;//調整這個即可調整起始攻擊CD
        canAttack = true;

        playerScore = 0;
        playerSkillScore = 20;

        hp = 100;

        //Set default projectile
        GameObject projectileObj = (GameObject)Resources.Load("Prefabs/Projectiles/DefaultProjectile");
        playerCurrentProjectile = projectileObj.GetComponent<ProjectileBase>();

        boundaryLayerMask = LayerMask.GetMask("GroundBoundary");

        playerCurrentSkills = new List<SkillBase>();
        playerCurrentPassiveSkills = new List<SkillBase>();
    }

    public void InitActions()
    {
        playerBehaviourAction += SetVectors;
        playerBehaviourAction += HandleMovement;
        playerBehaviourAction += HandleLookDir;
    }

    private void SetVectors()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector2(inputManager.LookInput.x, inputManager.LookInput.y));
        playerPos = playerTransform.position;
        aimDirection = MousePos - PlayerPos;
        Debug.DrawRay(PlayerPos, MousePos - PlayerPos, Color.green);
    }

    private void HandleMovement()
    {
        Vector2 moveDirection = new Vector2(inputManager.MoveInput.x, inputManager.MoveInput.y);
        moveDirection = BoundaryRayHandler(moveDirection);
        Vector2 playerNewPos = PlayerPos + moveDirection * moveSpeed * Time.deltaTime;
        playerTransform.position = playerNewPos;

        //Debug.Log(moveDirection);
        //rb.MovePosition(playerPos + moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleLookDir()
    {
        if (inputManager.LookInput == Vector2.zero) return;

        playerTransform.rotation = Quaternion.FromToRotation(Vector2.up, AimDirection);

        //Debug.Log(inputManager.LookInput.x + " " + inputManager.LookInput.y);
        //playerTransform.up = (target - playerPos).normalized;
    }

    private Vector2 BoundaryRayHandler(Vector2 moveDirection)
    {
        _playerEffectHandler.SetParticleVelocity(moveDirection);
        //Debug.Log(inputManager.MoveInput);
        if(inputManager.MoveInput.x > 0)
        {
            //Go right
            rayHitX = Physics2D.Raycast(this.playerPos, Vector2.right, 1f, boundaryLayerMask);
            if (rayHitX)
                moveDirection.x = 0; 
            Debug.DrawRay(this.playerPos, Vector2.right,Color.red);
        }
        else if(inputManager.MoveInput.x < 0)
        {
            //Go left
            rayHitX = Physics2D.Raycast(this.playerPos, Vector2.left, 1f, boundaryLayerMask);
            if (rayHitX)
                moveDirection.x = 0;
            Debug.DrawRay(this.playerPos, Vector2.left, Color.red);
        }
        if(inputManager.MoveInput.y > 0)
        {
            //Go up
            rayHitY = Physics2D.Raycast(this.playerPos, Vector2.up, 1f, boundaryLayerMask);
            if (rayHitY)
                moveDirection.y = 0;
            Debug.DrawRay(this.playerPos, Vector2.up, Color.red);
        }
        else if(inputManager.MoveInput.y < 0)
        {
            //Go down
            rayHitY = Physics2D.Raycast(this.playerPos, Vector2.down, 1f, boundaryLayerMask);
            if (rayHitY)
                moveDirection.y = 0;
            Debug.DrawRay(this.playerPos, Vector2.down, Color.red);
        }
        //boundaryDetectRay1 = new Ray2D(this.playerTransform.position,)

        return moveDirection;
    }

    private async void CDCount()
    {
        while(currentAttackCD > 0)
        {
            if (survivorLikeGame.GetAttackCDHintImg() == null) return;
            currentAttackCD -= 0.01f;
            survivorLikeGame.SetAttackCDImgFillAmount(currentAttackCD);
            //Debug.Log(currentAttackCD);
            await Task.Delay(attackCDTime);
        }
        Debug.Log("CD結束，可以攻擊");
        currentAttackCD = attackCDMax;
        survivorLikeGame.SetAttackCDImgFillAmount(currentAttackCD);
        canAttack = true;
    }

    /// <summary>
    /// 攻擊處理
    /// </summary>
    public void HandleAttack()
    {
        if (!canAttack) return;

        CDCount();//開始計算CD時間
        canAttack = false;

        Attack();//呼叫攻擊函式
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {
        Debug.Log("Attack");
        _shootProjectileHandler.Shoot();
    }

    /// <summary>
    /// 玩家受到攻擊
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if(hp > damage)
        {
            Debug.Log("受到攻擊");
            _playerEffectHandler.PlayerTakeDamageEffect();
            hp -= damage;
            return;
        }
        hp = 0;
        //Debug.Log("玩家死亡，遊戲結束");
        PlayerDeadHandler();
    }

    /// <summary>
    /// 添加玩家玩家分數
    /// </summary>
    public void SetPlayerScore(int score)
    {
        this.playerScore += score;
        this.pyCurrentAccumulateScore++;
        survivorLikeGame.SetPlayerScoreHintImgFillAmount((1f / playerSkillScore) * this.pyCurrentAccumulateScore);
        if(this.pyCurrentAccumulateScore >= this.playerSkillScore)
        {
            this.pyCurrentAccumulateScore = 0;
            this.playerSkillScore += 5;//每次獲得特殊能力，下次獲得就需要更多分數
            survivorLikeGame.ChooseSkill();
            //GamePause();
            survivorLikeGame.GamePause();
            Debug.Log("玩家獲得特殊能力");
        }
        //Debug.Log((1f / playerSkillScore) * this.playerScore);
    }

    /// <summary>
    /// 玩家死亡處理
    /// </summary>
    private void PlayerDeadHandler()
    {
        //GamePause();
        survivorLikeGame.GamePause();
        survivorLikeGame.GameSettlement(SetGameResultInfo());//呼叫遊戲結算
    }

    private GameResult SetGameResultInfo()
    {
        GameResult gameResult;
        gameResult.Score = this.playerScore;
        gameResult.GameTime = survivorLikeGame.GetGameTime();

        return gameResult;
    }
}
