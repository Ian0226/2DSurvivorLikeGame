using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    protected SurvivorLikeGame2DFacade _survivorLikeGame = null;

    protected int damage = 0;
    protected float speed = 0.0f;
    protected float existTime = 0.0f;

    private Vector2 moveDir;
    private Rigidbody2D rb = null;

    public delegate void Recycle(ProjectileBase projectile);
    public Recycle recycle;

    public float ExistTime { get => existTime; set => existTime = value; }

    private void Start()
    {
        Intialize();
    }

    public virtual void Update()
    {
        existTime -= Time.deltaTime;
        if (ExistTime <= 0)
        {
            recycle(this);
        }

        Move();
    }

    public virtual void Intialize() 
    {
        _survivorLikeGame = SurvivorLikeGame2DFacade.Instance;

        rb = this.GetComponent<Rigidbody2D>();

        damage = 1;
    }

    public virtual void Move() 
    {
        //moveDir = SurvivorLikeGame2DFacade.Instance.GetPlayerAimDir();
        rb.AddForce(moveDir.normalized * speed,ForceMode2D.Impulse);
    }

    public void SetMoveDir(Vector2 dir) 
    {
        moveDir = dir;
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }
}
