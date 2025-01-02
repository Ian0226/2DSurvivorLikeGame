using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase
{
    protected PlayerController _playerController = null;
    protected string skillName = "";
    protected int skillLevel = 0;
    protected int skillMaxLevel = 0;
    protected bool canChooseThisSkill = true;

    public string SkillName { get => skillName;}
    public bool CanChooseThisSkill { get => canChooseThisSkill;}

    public SkillBase(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public virtual void Initialize() { }
    public virtual void UseSkill() { }
    
}
