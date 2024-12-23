using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : SceneState
{
    public StartState(SceneStateController controller) : base(controller)
    {
        this.StateName = "StartState";
    }

    public override void StateBegin()
    {
        Debug.Log("Now Scene is " + this.StateName);
    }

    public override void StateUpdate()
    {
        this.controller.SetState(new MenuState(controller), "MenuScene");
    }
}
