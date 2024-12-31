using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �B�z�C���Ȱ��P�}�l
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
