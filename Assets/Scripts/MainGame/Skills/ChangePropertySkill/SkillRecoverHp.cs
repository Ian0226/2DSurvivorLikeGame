using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRecoverHp : SkillBase
{
    public SkillRecoverHp(PlayerController playerController) : base(playerController)
    {
        Initialize();
    }

    public override void Initialize()
    {
        this.skillName = "Skill_RecoverHp";
        this.skillDisplayName = "¦^´_30%¥Í©R­È";
        this.skillLevel = 1;
        this.skillMaxLevel = 1;

        base.Initialize();
    }

    public override void UseSkill()
    {
        _playerController.AddPlayerHp(_playerController.HpMax * 30 / 100);
    }
}
