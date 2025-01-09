using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    protected SurvivorLikeGame2DFacade _survivorLikeGame = null;

    protected ProjectileProp _projectileProp = null;

    protected string projectileName = "";
    protected int damage = 0;
    protected float speed = 0.0f;
    protected float existTime = 0.0f;

    private Vector2 moveDir;
    
    public delegate void Recycle(ProjectileBase projectile);
    public Recycle recycle;

    //Components
    private Rigidbody2D rb = null;
    protected Sprite projectileSprite = null;
    protected Material projectileMat = null;
    protected TrailRenderer trailRenderer = null;

    protected System.Action<EnemyBase, int> damageAction = null;

    public float ExistTime { get => existTime; set => existTime = value; }
    public Action<EnemyBase, int> DamageAction { get => damageAction; set => damageAction = value; }
    public string ProjectileName { get => projectileName; }

    private void Start()
    {
        //Intialize();
    }

    public virtual void Update()
    {
        existTime -= Time.deltaTime;
        if (ExistTime <= 0)
        {
            trailRenderer.enabled = false;
            recycle(this);
        }
    }

    public void FixedUpdate()
    {
        Move();
    }

    public virtual void Intialize() 
    {
        _survivorLikeGame = SurvivorLikeGame2DFacade.Instance;

        rb = this.GetComponent<Rigidbody2D>();

        SpriteRenderer pSpriteRenderer = this.GetComponent<SpriteRenderer>();

        projectileSprite = pSpriteRenderer.sprite;
        projectileMat = pSpriteRenderer.material;
    }

    public virtual void InitProperties() { }

    public virtual void Move() 
    {
        //moveDir = SurvivorLikeGame2DFacade.Instance.GetPlayerAimDir();
        rb.velocity = moveDir.normalized * speed;
        //rb.AddForce(moveDir.normalized * speed,ForceMode2D.Impulse);
    }

    /// <summary>
    /// 初始化子彈
    /// </summary>
    /// <param name="dir">子彈移動方向</param>
    public void InitProjectile(Vector2 dir)
    {
        SetMoveDir(dir);
        InitTrail();
    }

    public void SetMoveDir(Vector2 dir) 
    {
        moveDir = dir;
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }

    private void InitTrail()
    {
        trailRenderer.enabled = true;
    }

    public virtual void DamageFunc(EnemyBase tagetEnemy, int damage) { }
}
