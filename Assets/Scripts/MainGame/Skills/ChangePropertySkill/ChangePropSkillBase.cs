using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChangePropSkillBase : SkillBase//¼È®É¤£¥Î
{

    public ChangePropSkillBase(PlayerController playerController) : base(playerController) {}

    public abstract void SkillHandler();
}
