using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface
{
    protected SurvivorLikeGame2DFacade survivorLikeGame = null;
    protected GameObject rootUI = null;
    private bool active = true;

    public UserInterface(SurvivorLikeGame2DFacade survivorLikeGame)
    {
        this.survivorLikeGame = survivorLikeGame;
    }

    public bool IsVisible()
    {
        return active;
    }

    public virtual void Show()
    {
        rootUI.SetActive(true);
        active = true;
    }

    public virtual void Hide()
    {
        rootUI.SetActive(false);
        active = false;
    }

    public virtual void Initialize() { }
    public virtual void Release() { }
    public virtual void Update() { }
}
