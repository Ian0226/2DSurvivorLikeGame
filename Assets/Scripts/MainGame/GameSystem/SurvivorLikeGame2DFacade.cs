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

    //UI class
    private PlayerAttackCDUI _playerAttackCDUI = null;
    private GameInfoUI _gameInfoUI = null;

    #endregion

    public void Initialize()
    {
        //Game Systems
        _inputManager = new InputManager(this);
        _playerController = new PlayerController(this);
        _enemyInsSystem = new EnemyInsSystem(this);
        _waveSystem = new WaveSystem(this);

        //UI
        _playerAttackCDUI = new PlayerAttackCDUI(this);
        _gameInfoUI = new GameInfoUI(this);
    }

    public void Update()
    {
        _playerController.Update();
        _enemyInsSystem.Update();
        _waveSystem.Update();

        //UI
        _gameInfoUI.Update();
    }

    public void Release()
    {
        _inputManager.Release();
    }

    #region Functions

    /// <summary>
    /// 獲取玩家當前位置
    /// </summary>
    /// <returns></returns>
    public Vector2 GetPlayerPos()
    {
        return _playerController.PlayerPos;
    }

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

    /// <summary>
    /// 玩家攻擊處理
    /// </summary>
    public void HandleAttack()
    {
        if (_playerController != null)
            _playerController.HandleAttack();
    }

    /// <summary>
    /// 獲取攻擊CD時間UI圖片
    /// </summary>
    /// <returns></returns>
    public Image GetAttackCDHintImg()
    {
        return _playerAttackCDUI.AttackCDHintImg;
    }

    /// <summary>
    /// 設定攻擊CD時間的提示UI的FillAmount
    /// </summary>
    /// <param name="fillAmount"></param>
    public void SetAttackCDImgFillAmount(float fillAmount)
    {
        if (_playerAttackCDUI != null)
            _playerAttackCDUI.SetAttackCDImgFillAmount(fillAmount);
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
    /// 生成敵人
    /// </summary>
    /// <param name="index"></param>
    public void InsEnemy()
    {
        if(_enemyInsSystem != null)
            _enemyInsSystem.InsEnemy();
    }
    #endregion
}
