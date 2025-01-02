using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAddSpeed : SkillBase
{
    private System.Action[] skillActions;
    public SkillAddSpeed(PlayerController playerController) : base(playerController)
    {
        Initialize();
    }

    public override void Initialize()
    {
        this.skillName = "增加速度_L1";
        this.skillLevel = 1;
        this.skillMaxLevel = 3;

        skillActions = new System.Action[] { AddSpeed1, AddSpeed2, AddSpeed3 };
    }

    public override void UseSkill()
    {
        AddSpeedSkillHandler();

        if (this.skillLevel < this.skillMaxLevel)
        {
            this.skillLevel++;
            this.skillName = $"增加速度_L{this.skillLevel}";
        }
        else
        {
            this.skillName = $"增加速度_Max";
        }
    }

    private void AddSpeedSkillHandler()
    {
        skillActions[this.skillLevel-1].Invoke();
    }

    private void AddSpeed1()
    {
        _playerController.MoveSpeed += 0.3f;
    }

    private void AddSpeed2()
    {
        _playerController.MoveSpeed += 0.3f;
    }

    private void AddSpeed3()
    {
        _playerController.MoveSpeed += 0.5f;
    }
}
