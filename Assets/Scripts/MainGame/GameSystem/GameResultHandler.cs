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
    /// �i��C������
    /// </summary>
    public void GameSettlement(bool gameWin)
    {
        if (gameWin)
        {
            Debug.Log("�C�������A�ӧQ");
        }
        else
        {
            Debug.Log("�C�������A����");
        }
    }
}
