using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameSystemBase
{
    protected SurvivorLikeGame2DFacade survivorLikeGame = null;
    public GameSystemBase(SurvivorLikeGame2DFacade survivorLikeGame)
    {
        this.survivorLikeGame = survivorLikeGame;
    }

    public virtual void Initialize() { }
    public virtual void Update() { }
    public virtual void Release() { }
}
