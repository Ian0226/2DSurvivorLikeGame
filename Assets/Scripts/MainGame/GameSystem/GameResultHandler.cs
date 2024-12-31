using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultHandler : GameSystemBase
{
    public GameResultHandler(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        
    }

    /// <summary>
    /// 進行遊戲結算
    /// </summary>
    public void GameSettlement(bool gameWin)
    {
        if (gameWin)
        {
            Debug.Log("遊戲結束，勝利");
        }
        else
        {
            Debug.Log("遊戲結束，失敗");
        }
    }
}
