using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultProjectile : ProjectileBase
{
    public override void Intialize()
    {
        base.Intialize();
    }

    public override void InitProperties()
    {
        this._projectileProp = (ProjectileProp)Resources.Load($"ScriptableObject/Projectiles/{this.gameObject.name.Split("(")[0]}");
        Debug.Log(this.gameObject.name.Split("(")[0]);
        this.projectileName = _projectileProp.projectileName;
        this.damage = _projectileProp.damage;
        this.speed = _projectileProp.speed;
        this.existTime = _projectileProp.existTime;

        damageAction = DamageFunc;
        
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Move()
    {
        base.Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                damageAction(_survivorLikeGame.GetCurrentInGameEnemies(System.Int32.Parse(collision.gameObject.name.Split("(")[1])),
                    this.damage);
                existTime = 0;//�P���l�u
                break;
        }
    }

    /// <summary>
    /// �w�]�����Ҧ�
    /// </summary>
    /// <param name="tagetEnemy"></param>
    /// <param name="damage"></param>
    public override void DamageFunc(EnemyBase tagetEnemy, int damage)
    {
        tagetEnemy.TakeDamage(this.damage);
    }
}
