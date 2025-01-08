using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能 : 附加攻擊力
/// </summary>
public class SkillAddDamage : SkillBase
{
    private int addDamage = 0;
    private System.Action<EnemyBase, int>[] skillActions;
    public SkillAddDamage(PlayerController playerController) : base(playerController)
    {
        Initialize();
    }

    public override void Initialize()
    {
        this.skillLevel = 1;
        this.skillMaxLevel = 3;
        this.skillName = "附加攻擊力_L1";
        
        addDamage = 1;

        skillActions = new System.Action<EnemyBase, int>[] { DamageFunc1, DamageFunc2, DamageFunc3};
    }

    /// <summary>
    /// 呼叫該方法以執行技能邏輯
    /// </summary>
    public override void UseSkill()
    {
        //skillAction.Invoke();
        Debug.Log("選中技能 : 附加攻擊力_L1");
        AddSkillHandler();

        if (this.skillLevel < this.skillMaxLevel)
        {
            this.skillLevel++;
            this.skillName = $"附加攻擊力_L{this.skillLevel}";
        }   
        else
        {
            this.skillName = $"附加攻擊力_Max";
            this.canChooseThisSkill = false;
        }
    }

    /// <summary>
    /// 修改子彈的攻擊函式
    /// </summary>
    private void AddSkillHandler()
    {
        _playerController.PlayerCurrentProjectile.DamageAction = skillActions[this.skillLevel-1];//修改子彈的攻擊函式
    }

    /// <summary>
    /// 新的攻擊函式_等級1，攻擊力+1
    /// </summary>
    /// <param name="targetEnemy"></param>
    /// <param name="damage"></param>
    private void DamageFunc1(EnemyBase targetEnemy, int damage)
    {
        targetEnemy.TakeDamage(damage + addDamage);
    }

    /// <summary>
    /// 新的攻擊函式_等級2，攻擊力+2
    /// </summary>
    /// <param name="targetEnemy"></param>
    /// <param name="damage"></param>
    private void DamageFunc2(EnemyBase targetEnemy, int damage)
    {
        targetEnemy.TakeDamage(damage + (addDamage+=20));
    }

    /// <summary>
    /// 新的攻擊函式_等級3，攻擊力+5
    /// </summary>
    /// <param name="targetEnemy"></param>
    /// <param name="damage"></param>
    private void DamageFunc3(EnemyBase targetEnemy, int damage)
    {
        targetEnemy.TakeDamage(damage + (addDamage+=50));
    }

    public void SkillUpdate() { }
}
