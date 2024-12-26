using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : GameSystemBase
{
    private int wave = 0;
    private int waveTime = 0;

    private int insEnemyTypeIndex = 0;
    private float insWeight = 0.0f;
    public WaveSystem(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        wave = 1;
        waveTime = 60;

        insEnemyTypeIndex = 1;
    }

    public override void Update()
    {
        
    }
}
