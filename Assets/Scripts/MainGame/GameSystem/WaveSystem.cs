using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveSystem : GameSystemBase
{
    private int gameTime = 0;
    private int wave = 0;
    private int waveTime = 0;//�i�Ʈɶ��A�ثe�ȩw�C60��@�i�A���ծ�10��@�i
    private float insEnemyInterval = 0;//�ͦ��ĤH���j

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

        insEnemyCount = 2;//�C���ͦ��ĤH���ƶq

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
    /// �P�_�i�ƨð�������i�ƪ��B�z
    /// </summary>
    private void HandleWave()
    {
        switch (wave)
        {
            case 2:
                CoroutineTool.StopInsEnemyCoroutine();
                insEnemyCount += 1;//�ͦ��ĤH�ƶq�W�[
                //insEnemyAction += InsEnemyAction_02;//�ͦ�����2�ĤH
                CoroutineTool.ExcuteInvokeRepeatInsEnemy(insEnemyAction, this.insEnemyInterval);
                break;
            case 3:
                //��3�i�}�l�A�C5�i�ĤH�t�׼W�[0.1�A�̤j�W�[��1(�ȩw)�C�ͦ��s�����ĤH�C
                CoroutineTool.StopInsEnemyCoroutine();
                insEnemyAction += InsEnemyAction_02;//�ͦ�����2�ĤH
                CoroutineTool.ExcuteInvokeRepeatInsEnemy(insEnemyAction, this.insEnemyInterval);
                break;
            case 5:
                //��5�i�}�l�A�ĤH�ͦ��t�ץ[�֡A�C3�i�W�[0.5f�C�C���ͦ��ĤH�ƶq�[2�C
                break;
            case 7:
                //
                break;
        }
    }

    /// <summary>
    /// �ͦ��ĤH
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
