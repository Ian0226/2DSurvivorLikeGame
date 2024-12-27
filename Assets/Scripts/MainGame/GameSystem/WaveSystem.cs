using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveSystem : GameSystemBase
{
    private int gameTime = 0;
    private int wave = 0;
    private int waveTime = 0;//波數時間，目前暫定每60秒一波，測試時10秒一波
    private float insEnemyInterval = 0;//生成敵人間隔

    private int insEnemyTypeIndex = 0;
    private float insWeight = 0.0f;

    private Action insEnemyAction;
    private Action gameFlowAction;
    public WaveSystem(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public int Wave { get => wave; set => wave = value; }

    public override void Initialize()
    {
        wave = 0;
        //waveTime = 60;
        waveTime = 10;//Test
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
        HandleWave();
    }

    /// <summary>
    /// 判斷波數並執行對應波數的處理
    /// </summary>
    private void HandleWave()
    {
        switch (wave)
        {
            case 3:
                //第3波開始，每5波敵人速度增加0.1，最大增加到1(暫定)。
                break;
            case 5:
                //第5波開始，敵人生成速度加快，每3波增加0.5f。每次生成敵人數量更改為2。
                break;
            case 7:
                //第7波開始，生成新種類的敵人。
                break;
        }
    }

    private void InsEnemyAction_01()
    {
        survivorLikeGame.InsEnemy();
    }
}
