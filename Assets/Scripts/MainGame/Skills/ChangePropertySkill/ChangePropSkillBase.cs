using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChangePropSkillBase : SkillBase//�Ȯɤ���
{

    public ChangePropSkillBase(PlayerController playerController) : base(playerController) {}

    public abstract void SkillHandler();
}
