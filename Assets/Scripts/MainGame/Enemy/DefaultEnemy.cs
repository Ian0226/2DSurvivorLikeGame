using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.CustomTool;

public class DefaultEnemy : EnemyBase
{
    public override void Initialize()
    {
        _survivorLikeGame = SurvivorLikeGame2DFacade.Instance;

        this.enemyName = "DefaultEnemy";
        this.speed = 4.9f;
        this.damage = 1;
        this.hp = 1;

        this.deadEffectObj = (GameObject)Resources.Load("Prefabs/Enemy/Particles/DefaultEnemyDeadParticle");

        this.rewardScore = 1;

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
        //Debug.Log("�����쪱�a�A�������ĤH���� : " + this.gameObject.name);
    }

    public override void TakeDamage(int damage)
    {
        if(hp <= damage)//���`
        {
            hp = 0;
            HandleDeath();
            return;
        }
        hp -= damage;
    }

    /// <summary>
    /// �B�z���`
    /// </summary>
    public override void HandleDeath()
    {
        recycle(this);
        _survivorLikeGame.SetPlayerScore(this.rewardScore);
        _survivorLikeGame.CreateParticle(this.deadEffectObj, this.transform.position);
    }

    /// <summary>
    /// ���[���`���A
    /// </summary>
    public override void TakeAilmentsDamage(System.Action stateAction)
    {
        
    }
}
