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
    /// �P�_�i�ƨð�������i�ƪ��B�z
    /// </summary>
    private void HandleWave()
    {
        switch (wave)
        {
            case 3:
                //��3�i�}�l�A�C5�i�ĤH�t�׼W�[0.1�A�̤j�W�[��1(�ȩw)�C
                break;
            case 5:
                //��5�i�}�l�A�ĤH�ͦ��t�ץ[�֡A�C3�i�W�[0.5f�C�C���ͦ��ĤH�ƶq��אּ2�C
                break;
            case 7:
                //��7�i�}�l�A�ͦ��s�������ĤH�C
                break;
        }
    }

    private void InsEnemyAction_01()
    {
        survivorLikeGame.InsEnemy();
    }
}
