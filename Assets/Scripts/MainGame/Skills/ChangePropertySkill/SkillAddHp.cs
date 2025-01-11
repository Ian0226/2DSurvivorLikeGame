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
        this.skillDisplayName = "增加血量_L1";
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
            this.skillDisplayName = $"增加血量_L{this.skillLevel}";
        }
        else
        {
            this.skillDisplayName = $"增加血量_Max";
        }    
    }

    private void AddHpSkillHandler()
    {
        skillActions[this.skillLevel-1].Invoke();
    }

    private void AddHp1()
    {
        _playerController.HpMax += 20;
        _playerController.AddPlayerHp(20);
    }

    private void AddHp2()
    {
        _playerController.HpMax += 40;
        _playerController.AddPlayerHp(40);
    }

    private void AddHp3()
    {
        _playerController.HpMax += 80;
        _playerController.AddPlayerHp(80);
    }
}
