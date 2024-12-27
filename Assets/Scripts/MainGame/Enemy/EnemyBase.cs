using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected string enemyName = "Default";
    protected float speed = 0.0f;
    protected int damage = 0;
    protected int hp = 0;

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
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.targetPlayer.position, speed * Time.fixedDeltaTime);
        this.transform.rotation = Quaternion.FromToRotation(Vector2.up, targetPlayer.position - this.transform.position);
    }

    public void CollisionPlayer()
    {
        AttackPlayer();
        recycle(this);
    }

    protected abstract void AttackPlayer();
    public abstract void TakeDamage(int damage);
}
