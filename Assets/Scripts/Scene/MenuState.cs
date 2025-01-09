using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.CustomTool;

public class MenuState : SceneState
{
    public MenuState(SceneStateController controller) : base(controller)
    {
        this.StateName = "MenuState";
    }

    public override void StateBegin()
    {
        MainMenuHandler.Instance.Initailize();
        MainMenuHandler.Instance.ChangeSceneStateAction = ChangeState;
    }

    public override void StateUpdate()
    {
        MainMenuHandler.Instance.Update();
    }

    public override void StateEnd()
    {
        MainMenuHandler.Instance.Release();
    }

    public void ChangeState()
    {
        this.controller.SetState(new MainGameState(controller), "MainGameScene");
    }
}
