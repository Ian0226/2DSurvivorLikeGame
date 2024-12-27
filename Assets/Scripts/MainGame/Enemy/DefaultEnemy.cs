using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.CustomTool;

public class DefaultEnemy : EnemyBase
{
    public override void Initialize()
    {
        this.enemyName = "DefaultEnemy";
        this.speed = 0.7f;
        this.damage = 1;
        this.hp = 1;

        targetPlayer = UnityTool.FindGameObject("Player").transform;
    }
    public override void FollowPlayer()
    {
        base.FollowPlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                base.CollisionPlayer();
                break;
        }
    }

    protected override void AttackPlayer()
    {
        SurvivorLikeGame2DFacade.Instance.PlayerTakeDamage(this.damage);
        //Debug.Log("攻擊到玩家，消除此敵人物件 : " + this.gameObject.name);
    }

    public override void TakeDamage(int damage)
    {
        if(hp <= damage)
        {
            hp = 0;
            recycle(this);
            return;
        }
        hp -= damage;
    }
}
