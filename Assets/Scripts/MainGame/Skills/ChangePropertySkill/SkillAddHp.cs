using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAddHp : SkillBase
{
    private System.Action[] skillActions;
    public SkillAddHp(PlayerController playerController) : base(playerController)
    {
        Initialize();
    }

    public override void Initialize()
    {
        this.skillName = "Skill_AddHp";
        this.skillDisplayName = "�W�[��q_L1";
        this.skillLevel = 1;
        this.skillMaxLevel = 3;

        skillActions = new System.Action[] { AddHp1, AddHp2, AddHp3 };

        base.Initialize();
    }

    public override void UseSkill()
    {
        AddHpSkillHandler();
        if(this.skillLevel < this.skillMaxLevel)
        {
            this.skillLevel++;
            this.skillDisplayName = $"�W�[��q_L{this.skillLevel}";
        }
        else
        {
            this.skillDisplayName = $"�W�[��q_Max";
        }    
    }

    private void AddHpSkillHandler()
    {
        skillActions[this.skillLevel-1].Invoke();
    }

    private void AddHp1()
    {
        _playerController.Hp += 10;
    }

    private void AddHp2()
    {
        _playerController.Hp += 20;
    }

    private void AddHp3()
    {
        _playerController.Hp += 40;
    }
}
