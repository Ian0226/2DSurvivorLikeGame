using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveSystem : GameSystemBase
{
    private int gameTime = 0;
    private int wave = 0;
    private int waveTime = 0;//�i�Ʈɶ��A�ثe�ȩw�C60��@�i
    private int insEnemyInterval = 0;//�ͦ��ĤH���j

    private int insEnemyTypeIndex = 0;
    private float insWeight = 0.0f;

    private Action insEnemyAction;
    private Action gameFlowAction;
    public WaveSystem(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        wave = 1;
        waveTime = 60;
        insEnemyInterval = 1;

        insEnemyTypeIndex = 1;

        insEnemyAction = InsEnemyAction_01;
        gameFlowAction = GameWaveHandler;

        CoroutineTool.ExcuteInvokeRepeatInsEnemy(insEnemyAction, this.insEnemyInterval);
        CoroutineTool.ExcuteTimer(gameFlowAction);
    }

    public override void Update()
    {
        
    }

    private void GameWaveHandler()
    {
        if(CoroutineTool.TimeSecond % 60 == 0)
        {
            wave++;
        }
    }

    private void InsEnemyAction_01()
    {
        survivorLikeGame.InsEnemy();
    }
}
