using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ޯ� : ���[�����O
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
        this.skillName = "���[�����O_L1";
        
        addDamage = 1;

        skillActions = new System.Action<EnemyBase, int>[] { DamageFunc1, DamageFunc2, DamageFunc3};
    }

    /// <summary>
    /// �I�s�Ӥ�k�H����ޯ��޿�
    /// </summary>
    public override void UseSkill()
    {
        //skillAction.Invoke();
        Debug.Log("�襤�ޯ� : ���[�����O_L1");
        AddSkillHandler();

        if (this.skillLevel < this.skillMaxLevel)
        {
            this.skillLevel++;
            this.skillName = $"���[�����O_L{this.skillLevel}";
        }   
        else
        {
            this.skillName = $"���[�����O_Max";
            this.canChooseThisSkill = false;
        }
    }

    /// <summary>
    /// �ק�l�u�������禡
    /// </summary>
    private void AddSkillHandler()
    {
        _playerController.PlayerCurrentProjectile.DamageAction = skillActions[this.skillLevel-1];//�ק�l�u�������禡
    }

    /// <summary>
    /// �s�������禡_����1�A�����O+1
    /// </summary>
    /// <param name="targetEnemy"></param>
    /// <param name="damage"></param>
    private void DamageFunc1(EnemyBase targetEnemy, int damage)
    {
        targetEnemy.TakeDamage(damage + addDamage);
    }

    /// <summary>
    /// �s�������禡_����2�A�����O+2
    /// </summary>
    /// <param name="targetEnemy"></param>
    /// <param name="damage"></param>
    private void DamageFunc2(EnemyBase targetEnemy, int damage)
    {
        targetEnemy.TakeDamage(damage + (addDamage+=20));
    }

    /// <summary>
    /// �s�������禡_����3�A�����O+5
    /// </summary>
    /// <param name="targetEnemy"></param>
    /// <param name="damage"></param>
    private void DamageFunc3(EnemyBase targetEnemy, int damage)
    {
        targetEnemy.TakeDamage(damage + (addDamage+=50));
    }

    public void SkillUpdate() { }
}
