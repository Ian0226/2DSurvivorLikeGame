using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillBase
{
    protected PlayerController _playerController = null;

    protected string skillName = "";
    protected int skillLevel = 0;
    protected int skillMaxLevel = 0;

    protected Sprite skillIcon = null;
    protected string skillDisplayName = "";
    protected string skillInfo = "";

    protected bool canChooseThisSkill = true;

    public string SkillDisplayName { get => skillDisplayName; }
    public bool CanChooseThisSkill { get => canChooseThisSkill;}
    public Sprite SkillIcon { get => skillIcon;}
    public string SkillInfo { get => skillInfo; }

    public SkillBase(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public virtual void Initialize() 
    {
        if (Resources.Load($"Sprites/SkillIcon/{this.skillName}") != null)
            skillIcon = Resources.Load<Sprite>($"Sprites/SkillIcon/{this.skillName}");
    }

    public virtual void UseSkill() { }
    
}
