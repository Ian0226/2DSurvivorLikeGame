using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefualtProjectile : ProjectileBase
{
    public override void Intialize()
    {
        base.Intialize();
    }

    public override void InitProperties()
    {
        this._projectileProp = (ProjectileProp)Resources.Load("ScriptableObject/Projectiles/Projectile_Default");
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
                damageAction(_survivorLikeGame.GetCurrentInGameEnemies(System.Int32.Parse(collision.gameObject.name.Split("_")[1])),
                    this.damage);
                existTime = 0;//銷毀子彈
                break;
        }
    }

    /// <summary>
    /// 預設攻擊模式
    /// </summary>
    /// <param name="tagetEnemy"></param>
    /// <param name="damage"></param>
    public override void DamageFunc(EnemyBase tagetEnemy, int damage)
    {
        tagetEnemy.TakeDamage(this.damage);
    }
}
