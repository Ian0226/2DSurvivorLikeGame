using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillDecorator : SkillBase
{
    private SkillBase m_Skill;
    public SkillDecorator(PlayerController playerController,SkillBase skillBase) : base(playerController)
    {
        m_Skill = skillBase;
    }

    public override void UseSkill()
    {
        m_Skill.UseSkill();
    }
}
