using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �B�z�C���y�{�p�Ȱ��B�}�l
/// </summary>
public class MainGameManager : GameSystemBase
{
    private bool gameWin = false;
    private long score = 0;
    private int gameTime = 0;

    public bool GameWin { get => gameWin; set => gameWin = value; }
    public long Score { get => score; set => score = value; }
    public int GameTime { get => gameTime; set => gameTime = value; }

    public MainGameManager(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        
    }

    /// <summary>
    /// �C���Ȱ�
    /// </summary>
    public void GamePause()
    {
        Time.timeScale = 0;
        survivorLikeGame.SetPlayerBehaviourAction(null);
        survivorLikeGame.GetInputManager().Release();
    }

    /// <summary>
    /// �C���~��
    /// </summary>
    public void GameContinue()
    {
        survivorLikeGame.InitPlayerAction();
        survivorLikeGame.GetInputManager().EnableInputManager();
        Time.timeScale = 1;
    }

}
