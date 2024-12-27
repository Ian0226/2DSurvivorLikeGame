using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.CustomTool;
using System.Threading.Tasks;
using System;
using UnityEngine.Pool;

public class PlayerController : GameSystemBase
{
    private InputManager inputManager;
    private Transform playerTransform;

    #region ���a�ƭ�

    /// <summary>
    /// ���a���ʳt��
    /// </summary>
    private float moveSpeed = 5f;

    /// <summary>
    /// ����CD�ɶ��A��쬰�@��
    /// </summary>
    private int attackCDTime = 10;//�ק�o�Өӧ���CD���ֺC�A�ƭȶV�j�V�C�A�V�p�V��
    private int attackCDMax = 1;
    private float currentAttackCD = 0;
    private bool canAttack = false;

    /// <summary>
    /// ���a��q
    /// </summary>
    private int hp = 0;

    #endregion

    /// <summary>
    /// ���a��Ц�m
    /// </summary>
    private Vector2 mousePos;

    /// <summary>
    /// ���a��e��m
    /// </summary>
    private Vector2 playerPos;

    /// <summary>
    /// ���a�˷Ǧ�m
    /// </summary>
    private Vector2 aimDirection;

    #region ��������

    private PlayerShootHandler _shootProjectileHandler = null;

    /// <summary>
    /// ���a�����Ҧ�
    /// </summary>
    public enum PlayerAttackModeEnum
    {
        projectile,
        ray,
        other
    }
    private PlayerAttackModeEnum playerAttackMode;

    /// <summary>
    /// ���a��e�ϥΪ���g��
    /// </summary>
    private GameObject playerCurrentProjectile = null;

    /// <summary>
    /// ���a�i�H�g����g���ƶq�A�w�]��1
    /// </summary>
    private int projectilesCount = 1;

    #endregion

    public bool CanAttack { get => canAttack; set => canAttack = value; }
    public Vector2 MousePos { get => mousePos;}
    public Vector2 PlayerPos { get => playerPos;}
    public GameObject PlayerCurrentProjectile { get => playerCurrentProjectile; set => playerCurrentProjectile = value; }
    public PlayerAttackModeEnum PlayerAttackMode { get => playerAttackMode; set => playerAttackMode = value; }
    public int ProjectilesCount { get => projectilesCount; set => projectilesCount = value; }
    public Vector2 AimDirection { get => aimDirection;}
    public int Hp { get => hp;}

    public PlayerController(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        inputManager = SurvivorLikeGame2DFacade.Instance.GetInputManager();

        playerTransform = UnityTool.FindGameObject("Player").transform;

        _shootProjectileHandler = new PlayerShootHandler(this);

        InitProperties();
    }

    public override void Update()
    {
        SetVectors();
        
        HandleMovement();
        HandleLookDir();
    }

    private void InitProperties()
    {
        attackCDMax = 1;
        currentAttackCD = attackCDMax;
        attackCDTime = 10;
        canAttack = true;

        hp = 5;

        PlayerCurrentProjectile = (GameObject)Resources.Load("Prefabs/Projectiles/DefaultProjectile");
        
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
        Debug.Log("CD�����A�i�H����");
        currentAttackCD = attackCDMax;
        survivorLikeGame.SetAttackCDImgFillAmount(currentAttackCD);
        canAttack = true;
    }

    /// <summary>
    /// �����B�z
    /// </summary>
    public void HandleAttack()
    {
        if (!canAttack) return;

        CDCount();//�}�l�p��CD�ɶ�
        canAttack = false;

        Attack();//�I�s�����禡
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Attack()
    {
        Debug.Log("Attack");
        _shootProjectileHandler.Shoot();
    }

    public void TakeDamage(int damage)
    {
        if(hp <= damage)
        {
            hp = 0;
            Debug.Log("���a���`");
            return;
        }
        Debug.Log("�������");
        hp -= damage;
    }
}
