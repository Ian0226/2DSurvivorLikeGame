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
    private InputManager _inputManager = null;
    private PlayerController _playerController = null;

    //UI class
    PlayerAttackCDUI _playerAttackCDUI = null;

    #endregion

    public void Initialize()
    {
        _inputManager = new InputManager(this);
        _playerController = new PlayerController(this);

        _playerAttackCDUI = new PlayerAttackCDUI(this);
    }

    public void Update()
    {
        _playerController.Update();
    }

    public void Release()
    {
        _inputManager.Release();
    }

    #region Functions
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
    #endregion
}
