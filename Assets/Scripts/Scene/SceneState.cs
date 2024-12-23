using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneState
{
    private string stateName = "";
    public string StateName
    {
        get { return stateName; }
        set { stateName = value; }
    }

    protected SceneStateController controller = null;

    public SceneState(SceneStateController controller)
    {
        this.controller = controller;
    }

    public virtual void StateBegin() { }
    public virtual void StateEnd() { }
    public virtual void StateUpdate() { }
}
