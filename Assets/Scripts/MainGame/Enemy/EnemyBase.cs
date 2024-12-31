using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected SurvivorLikeGame2DFacade _survivorLikeGame = null;

    protected string enemyName = "Default";
    protected float speed = 0.0f;
    protected int damage = 0;
    protected int hp = 0;

    /// <summary>
    /// 玩家擊殺該敵人可獲得的分數
    /// </summary>
    protected int rewardScore = 0;

    protected Transform targetPlayer = null;

    public delegate void Recycle(EnemyBase enemy);
    public Recycle recycle;

    public string EnemyName { get => enemyName;}

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        FollowPlayer();
    }

    public virtual void Initialize() { }
    public virtual void FollowPlayer() 
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.targetPlayer.position, speed * Time.deltaTime);
        this.transform.rotation = Quaternion.FromToRotation(Vector2.up, targetPlayer.position - this.transform.position);
    }

    public void CollisionPlayer()
    {
        AttackPlayer();
        recycle(this);
    }

    protected abstract void AttackPlayer();
    public abstract void TakeDamage(int damage);

    //附加異常(疾病)狀態
    public abstract void TakeAilmentsDamage(System.Action stateAction);
}
