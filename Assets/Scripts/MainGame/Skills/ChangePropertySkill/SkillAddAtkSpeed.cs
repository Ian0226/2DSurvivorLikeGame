using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAddAtkSpeed : SkillBase
{
    private int[] atkSpeedValues;
    public SkillAddAtkSpeed(PlayerController playerController) : base(playerController)
    {
        Initialize();
    }

    public override void Initialize()
    {
        this.skillName = "Skill_AddAtkSpeed";
        this.skillLevel = 1;
        this.skillMaxLevel = 3;
        this.skillDisplayName = "�W�[�\�t_L1";

        atkSpeedValues = new int[] { -5, -7, -10 };

        base.Initialize();
    }

    public override void UseSkill()
    {
        _playerController.AttackCDTime += atkSpeedValues[this.skillLevel - 1];

        if (this.skillLevel < this.skillMaxLevel)
        {
            this.skillLevel++;
            this.skillDisplayName = $"�W�[�\�t_L{this.skillLevel}";
        }
        else
        {
            this.skillDisplayName = $"�W�[�\�t_Max";
            this.canChooseThisSkill = false;
        }
    }

}
