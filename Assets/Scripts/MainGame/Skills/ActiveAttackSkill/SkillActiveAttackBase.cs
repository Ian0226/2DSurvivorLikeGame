using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillActiveAttackBase : SkillBase
{
    public SkillActiveAttackBase(PlayerController playerController) : base(playerController) { }

    public abstract void SkillAction(GameObject projectileObj);

}
