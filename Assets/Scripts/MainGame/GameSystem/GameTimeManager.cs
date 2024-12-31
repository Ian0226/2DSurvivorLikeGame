using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 處理遊戲暫停與開始
/// </summary>
public class GameTimeManager : GameSystemBase
{
    public GameTimeManager(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        
    }

    /// <summary>
    /// 遊戲暫停
    /// </summary>
    public void GamePause()
    {
        Time.timeScale = 0;
        survivorLikeGame.SetPlayerBehaviourAction(null);
        survivorLikeGame.GetInputManager().Release();
    }

    /// <summary>
    /// 遊戲繼續
    /// </summary>
    public void GameContinue()
    {
        survivorLikeGame.InitPlayerAction();
        survivorLikeGame.GetInputManager().EnableInputManager();
        Time.timeScale = 1;
    }
}
