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
    public void GameSettlement(GameResult result)
    {
        GameEnd(result);
    }

    /// <summary>
    /// 遊戲結束
    /// </summary>
    private void GameEnd(GameResult gameResult)
    {
        survivorLikeGame.SetGameResultUI(gameResult);
        survivorLikeGame.ShowGameResultUI();
    }
}

public struct GameResult
{
    public long Score;
    public int GameTime;
    //Own Skills
}
