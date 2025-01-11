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

    private int insEnemyCount = 0;

    private Action insEnemyAction;
    private Action gameFlowAction;
    public WaveSystem(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public int GameTime { get => gameTime; set => gameTime = value; }
    public int Wave { get => wave; set => wave = value; }

    public override void Initialize()
    {
        wave = 1;
        //waveTime = 60;
        waveTime = 10;//Test
        insEnemyInterval = 1;

        insEnemyTypeIndex = 1;

        insEnemyCount = 2;//每次生成敵人的數量

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
        gameTime++;
        if(gameTime % 60 == 0)
        {
            wave++;
            HandleWave();
        }
    }

    /// <summary>
    /// 判斷波數並執行對應波數的處理
    /// </summary>
    private void HandleWave()
    {
        switch (wave)
        {
            case 2:
                CoroutineTool.StopInsEnemyCoroutine();
                insEnemyCount += 1;//生成敵人數量增加
                //insEnemyAction += InsEnemyAction_02;//生成等級2敵人
                CoroutineTool.ExcuteInvokeRepeatInsEnemy(insEnemyAction, this.insEnemyInterval);
                break;
            case 3:
                //第3波開始，每5波敵人速度增加0.1，最大增加到1(暫定)。生成新種類敵人。
                CoroutineTool.StopInsEnemyCoroutine();
                insEnemyAction += InsEnemyAction_02;//生成等級2敵人
                CoroutineTool.ExcuteInvokeRepeatInsEnemy(insEnemyAction, this.insEnemyInterval);
                break;
            case 5:
                //第5波開始，敵人生成速度加快，每3波增加0.5f。每次生成敵人數量加2。
                break;
            case 7:
                //
                break;
        }
    }

    /// <summary>
    /// 生成敵人
    /// </summary>
    private void InsEnemyAction_01()
    {
        for (int i = 0; i < insEnemyCount; i++)
            survivorLikeGame.InsEnemy(0);
    }

    /// <summary>
    /// 
    /// </summary>
    private void InsEnemyAction_02()
    {
        for (int i = 0; i < insEnemyCount; i++)
            survivorLikeGame.InsEnemy(1);
    }

    /// <summary>
    /// 
    /// </summary>
    private void InsEnemyAction_03()
    {
        survivorLikeGame.InsEnemy(0);
    }
}
