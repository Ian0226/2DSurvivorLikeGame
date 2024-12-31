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
        this.skillName = "增加血量_L1";
        this.skillLevel = 1;
        this.skillMaxLevel = 3;

        skillActions = new System.Action[] { AddHp1, AddHp2, AddHp3 };
    }

    public override void UseSkill()
    {
        AddHpSkillHandler();
        if(this.skillLevel < this.skillMaxLevel)
            this.skillLevel++;
        this.skillName = $"增加血量_L{this.skillLevel}";
    }

    private void AddHpSkillHandler()
    {
        skillActions[this.skillLevel-1].Invoke();
    }

    private void AddHp1()
    {
        _playerController.Hp += 1;
    }

    private void AddHp2()
    {
        _playerController.Hp += 2;
    }

    private void AddHp3()
    {
        _playerController.Hp += 4;
    }
}
