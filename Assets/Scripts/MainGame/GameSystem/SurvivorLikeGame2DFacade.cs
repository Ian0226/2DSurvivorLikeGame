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
    /// ���PlayerController
    /// </summary>
    /// <returns></returns>
    public PlayerController GetPlayerController()
    {
        return _playerController;
    }
    /// <summary>
    /// ������a��e��m
    /// </summary>
    /// <returns></returns>
    public Vector2 GetPlayerPos()
    {
        return _playerController.PlayerPos;
    }

    /// <summary>
    /// ���a�����B�z
    /// </summary>
    public void HandleAttack()
    {
        if (_playerController != null)
            _playerController.HandleAttack();
    }

    /// <summary>
    /// ������a��e�˷Ǥ�V
    /// </summary>
    /// <returns></returns>
    public Vector2 GetPlayerAimDir()
    {
        return _playerController.AimDirection;
    }

    /// <summary>
    /// ���a����ˮ`
    /// </summary>
    /// <param name="damage"></param>
    public void PlayerTakeDamage(int damage)
    {
        if (_playerController != null)
            _playerController.TakeDamage(damage);
    }

    /// <summary>
    /// ������a��q
    /// </summary>
    /// <returns></returns>
    public int GetPlayerHP()
    {
        return _playerController.Hp;
    }

    /// <summary>
    /// �K�[���a����
    /// </summary>
    /// <param name="score"></param>
    public void SetPlayerScore(int score)
    {
        if (_playerController != null)
            _playerController.SetPlayerScore(score);
    }

    /// <summary>
    /// ������a����
    /// </summary>
    /// <returns></returns>
    public long GetPlayerScore()
    {
        return _playerController.PlayerScore;
    }

    /// <summary>
    /// �]�w���a�欰Action
    /// </summary>
    /// <returns></returns>
    public void SetPlayerBehaviourAction(System.Action action)
    {
        _playerController.PlayerBehaviourAction = action;
    }

    /// <summary>
    /// ��l�ƪ��aAction
    /// </summary>
    public void InitPlayerAction()
    {
        _playerController.InitActions();
    }

    //Input Manager
    /// <summary>
    /// ���InputManager
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
    /// �������CD�ɶ�UI�Ϥ�
    /// </summary>
    /// <returns></returns>
    public Image GetAttackCDHintImg()
    {
        return _followPlayerHintUI.AttackCDHintImg;
    }

    /// <summary>
    /// �]�w����CD�ɶ�������UI��FillAmount
    /// </summary>
    /// <param name="fillAmount"></param>
    public void SetAttackCDImgFillAmount(float fillAmount)
    {
        if (_followPlayerHintUI != null)
            _followPlayerHintUI.SetAttackCDImgFillAmount(fillAmount);
    }

    /// <summary>
    /// �]�w���a���ƴ���UI��FillAmount
    /// </summary>
    /// <param name="fillAmount"></param>
    public void SetPlayerScoreHintImgFillAmount(float fillAmount)
    {
        if (_followPlayerHintUI != null)
            _followPlayerHintUI.SetPlayerScoreHintImgFillAmount(fillAmount);
    }

    //EnemyInsSystem
    /// <summary>
    /// �ͦ��ĤH
    /// </summary>
    /// <param name="index"></param>
    public void InsEnemy()
    {
        if(_enemyInsSystem != null)
            _enemyInsSystem.InsEnemy();
    }

    /// <summary>
    /// ��index�����e�C���������s�b���S�w�ĤH
    /// </summary>
    /// <param name="index">�ĤH������</param>
    /// <returns></returns>
    public EnemyBase GetCurrentInGameEnemies(int index)
    {
        return _enemyInsSystem.GetEnemyInEnemies(index);
    }

    /// <summary>
    /// �����e�i��
    /// </summary>
    /// <returns></returns>
    public float GetCurrentWave()
    {
        return _waveSystem.Wave;
    }

    /// <summary>
    /// �C������
    /// </summary>
    /// <param name="gameWin"></param>
    public void GameSettlement(bool gameWin)
    {
        if (_gameResultHandler != null)
            _gameResultHandler.GameSettlement(gameWin);
    }

    /// <summary>
    /// ��ܧޯ�
    /// </summary>
    public void ChooseSkill()
    {
        if (_chooseSkillSystem != null)
            _chooseSkillSystem.ChooseSkill();
    }

    /// <summary>
    /// �C���Ȱ�
    /// </summary>
    public void GamePause()
    {
        if (_gameTimeManager != null)
            _gameTimeManager.GamePause();
    }

    /// <summary>
    /// �C���~��
    /// </summary>
    public void GameContinue()
    {
        if (_gameTimeManager != null)
            _gameTimeManager.GameContinue();
    }
    #endregion
}
