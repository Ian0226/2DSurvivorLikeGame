using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected float speed = 0.0f;
    protected int damage = 0;

    protected Transform targetPlayer = null;

    public delegate void Recycle(EnemyBase enemy);
    public Recycle recycle;

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
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, targetPlayer.position);
    }

    public void CollisionPlayer()
    {
        AttackPlayer();
        recycle(this);
    }

    protected abstract void AttackPlayer();
}
