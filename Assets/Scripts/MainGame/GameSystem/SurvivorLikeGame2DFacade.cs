using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivorLikeGame2DFacade
{
    private SurvivorLikeGame2DFacade() { }

    private static SurvivorLikeGame2DFacade _instance = null;
    public static SurvivorLikeGame2DFacade Instance
    {
        get { if(_instance == null)
                _instance = new SurvivorLikeGame2DFacade();
            return _instance;
        }
    }

    #region Class Member
    //Game Systems
    private InputManager _inputManager = null;
    private PlayerController _playerController = null;
    private EnemyInsSystem _enemyInsSystem = null;
    private WaveSystem _waveSystem = null;
    private GameResultHandler _gameResultHandler = null;
    private ChooseSkillSystem _chooseSkillSystem = null;
    private GameTimeManager _gameTimeManager = null;

    //UI class
    private FollowPlayerHintUI _followPlayerHintUI = null;
    private GameInfoUI _gameInfoUI = null;

    #endregion

    public void Initialize()
    {
        //Game Systems
        _inputManager = new InputManager(this);
        _playerController = new PlayerController(this);
        _enemyInsSystem = new EnemyInsSystem(this);
        _waveSystem = new WaveSystem(this);
        _gameResultHandler = new GameResultHandler(this);
        _chooseSkillSystem = new ChooseSkillSystem(this);
        _gameTimeManager = new GameTimeManager(this);

        //UI
        _followPlayerHintUI = new FollowPlayerHintUI(this);
        _gameInfoUI = new GameInfoUI(this);
    }

    public void Update()
    {
        _playerController.Update();
        _enemyInsSystem.Update();
        _waveSystem.Update();
        _chooseSkillSystem.Update();

        //UI
        _gameInfoUI.Update();
    }

    public void Release()
    {
        _inputManager.Release();
    }

    #region Functions

    //PlayerController
    /// <summary>
    /// 獲取PlayerController
    /// </summary>
    /// <returns></returns>
    public PlayerController GetPlayerController()
    {
        return _playerController;
    }
    /// <summary>
    /// 獲取玩家當前位置
    /// </summary>
    /// <returns></returns>
    public Vector2 GetPlayerPos()
    {
        return _playerController.PlayerPos;
    }

    /// <summary>
    /// 玩家攻擊處理
    /// </summary>
    public void HandleAttack()
    {
        if (_playerController != null)
            _playerController.HandleAttack();
    }

    /// <summary>
    /// 獲取玩家當前瞄準方向
    /// </summary>
    /// <returns></returns>
    public Vector2 GetPlayerAimDir()
    {
        return _playerController.AimDirection;
    }

    /// <summary>
    /// 玩家受到傷害
    /// </summary>
    /// <param name="damage"></param>
    public void PlayerTakeDamage(int damage)
    {
        if (_playerController != null)
            _playerController.TakeDamage(damage);
    }

    /// <summary>
    /// 獲取玩家血量
    /// </summary>
    /// <returns></returns>
    public int GetPlayerHP()
    {
        return _playerController.Hp;
    }

    /// <summary>
    /// 添加玩家分數
    /// </summary>
    /// <param name="score"></param>
    public void SetPlayerScore(int score)
    {
        if (_playerController != null)
            _playerController.SetPlayerScore(score);
    }

    /// <summary>
    /// 獲取玩家分數
    /// </summary>
    /// <returns></returns>
    public long GetPlayerScore()
    {
        return _playerController.PlayerScore;
    }

    /// <summary>
    /// 設定玩家行為Action
    /// </summary>
    /// <returns></returns>
    public void SetPlayerBehaviourAction(System.Action action)
    {
        _playerController.PlayerBehaviourAction = action;
    }

    /// <summary>
    /// 初始化玩家Action
    /// </summary>
    public void InitPlayerAction()
    {
        _playerController.InitActions();
    }

    //Input Manager
    /// <summary>
    /// 獲取InputManager
    /// </summary>
    public InputManager GetInputManager()
    {
        if (_inputManager != null)
            return _inputManager;
        else
            return null;
    }

    //PlayerAttackCDUI
    /// <summary>
    /// 獲取攻擊CD時間UI圖片
    /// </summary>
    /// <returns></returns>
    public Image GetAttackCDHintImg()
    {
        return _followPlayerHintUI.AttackCDHintImg;
    }

    /// <summary>
    /// 設定攻擊CD時間的提示UI的FillAmount
    /// </summary>
    /// <param name="fillAmount"></param>
    public void SetAttackCDImgFillAmount(float fillAmount)
    {
        if (_followPlayerHintUI != null)
            _followPlayerHintUI.SetAttackCDImgFillAmount(fillAmount);
    }

    /// <summary>
    /// 設定玩家分數提示UI的FillAmount
    /// </summary>
    /// <param name="fillAmount"></param>
    public void SetPlayerScoreHintImgFillAmount(float fillAmount)
    {
        if (_followPlayerHintUI != null)
            _followPlayerHintUI.SetPlayerScoreHintImgFillAmount(fillAmount);
    }

    //EnemyInsSystem
    /// <summary>
    /// 生成敵人
    /// </summary>
    /// <param name="index"></param>
    public void InsEnemy()
    {
        if(_enemyInsSystem != null)
            _enemyInsSystem.InsEnemy();
    }

    /// <summary>
    /// 用index獲取當前遊戲場景中存在的特定敵人
    /// </summary>
    /// <param name="index">敵人的索引</param>
    /// <returns></returns>
    public EnemyBase GetCurrentInGameEnemies(int index)
    {
        return _enemyInsSystem.GetEnemyInEnemies(index);
    }

    /// <summary>
    /// 獲取當前波數
    /// </summary>
    /// <returns></returns>
    public float GetCurrentWave()
    {
        return _waveSystem.Wave;
    }

    /// <summary>
    /// 遊戲結算
    /// </summary>
    /// <param name="gameWin"></param>
    public void GameSettlement(bool gameWin)
    {
        if (_gameResultHandler != null)
            _gameResultHandler.GameSettlement(gameWin);
    }

    /// <summary>
    /// 選擇技能
    /// </summary>
    public void ChooseSkill()
    {
        if (_chooseSkillSystem != null)
            _chooseSkillSystem.ChooseSkill();
    }

    /// <summary>
    /// 遊戲暫停
    /// </summary>
    public void GamePause()
    {
        if (_gameTimeManager != null)
            _gameTimeManager.GamePause();
    }

    /// <summary>
    /// 遊戲繼續
    /// </summary>
    public void GameContinue()
    {
        if (_gameTimeManager != null)
            _gameTimeManager.GameContinue();
    }
    #endregion
}
