using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.CustomTool;

public class DefaultEnemy : EnemyBase
{
    public override void Initialize()
    {
        base.Initialize();
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
        HandleOnDamageEffect();
    }

    protected override void HandleOnDamageEffect()
    {
        //this.enemyAudioSource.PlayOneShot(this.takeDamageAudio);
        AudioManager.Instance.PlayerAudioOneShot(AudioManager.GameAudioSource.EnemyAudio, this.takeDamageAudio);
    }

    /// <summary>
    /// �B�z���`
    /// </summary>
    public override void HandleDeath()
    {
        AudioManager.Instance.PlayerAudioOneShot(AudioManager.GameAudioSource.EnemyAudio, this.deadAudio);
        recycle(this);
        _survivorLikeGame.SetPlayerScore(this.rewardScore);
        _survivorLikeGame.CreateParticle(this.deadEffectObj, this.transform.position, null);
    }

    /// <summary>
    /// ���[���`���A
    /// </summary>
    public override void TakeAilmentsDamage(System.Action stateAction)
    {
        
    }
}
