using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillChangeProjectile : SkillActiveAttackBase
{
    private GameObject projectileObj = null;
    public SkillChangeProjectile(PlayerController playerController) : base(playerController) { }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void UseSkill()
    {
        _playerController.PlayerCurrentProjectile = projectileObj.GetComponent<ProjectileBase>();
        Debug.Log(this.skillName);
        SkillManager.Instance.SearchSkillOption(this.skillName).Weight = 0;
        
    }

    /// <summary>
    /// 初始化要呼叫這個方法
    /// </summary>
    /// <param name="projectile"></param>
    public override void SkillAction(GameObject projectile)
    {
        SkillProp skillProp = (SkillProp)Resources.Load($"ScriptableObject/Skill/Skill_{projectile.name}");
        this.projectileObj = projectile;

        this.skillName = $"Skill_{projectile.name}";
        Debug.Log(this.skillName + " Skill Action");
        this.skillDisplayName = skillProp.skillName;
        this.skillLevel = skillProp.skillLevel;
        this.skillMaxLevel = skillProp.skillMaxLevel;

        Initialize();
    }
}
