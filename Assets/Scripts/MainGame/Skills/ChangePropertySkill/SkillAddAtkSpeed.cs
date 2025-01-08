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
        this.skillLevel = 1;
        this.skillMaxLevel = 3;
        this.skillName = "�W�[�\�t_L1";

        atkSpeedValues = new int[] { -1, -2, -3 };
    }

    public override void UseSkill()
    {
        _playerController.AttackCDTime += atkSpeedValues[this.skillLevel - 1];

        if (this.skillLevel < this.skillMaxLevel)
        {
            this.skillLevel++;
            this.skillName = $"�W�[�\�t_L{this.skillLevel}";
        }
        else
        {
            this.skillName = $"�W�[�\�t_Max";
            this.canChooseThisSkill = false;
        }
    }

}
