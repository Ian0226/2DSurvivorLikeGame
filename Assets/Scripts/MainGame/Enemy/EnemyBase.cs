using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.CustomTool;

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

    /// <summary>
    /// 死亡特效
    /// </summary>
    protected GameObject deadEffectObj = null;

    public delegate void Recycle(EnemyBase enemy);
    public Recycle recycle;

    public string EnemyName { get => enemyName;}
    public GameObject DeadEffectObj { get => deadEffectObj;}

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        FollowPlayer();
    }

    public virtual void Initialize() 
    {
        _survivorLikeGame = SurvivorLikeGame2DFacade.Instance;

        InitProps();

        targetPlayer = UnityTool.FindGameObject("Player").transform;
    }
    /// <summary>
    /// 參數初始化
    /// </summary>
    public void InitProps()
    {
        DefaultEnemyProp enemyProp = (DefaultEnemyProp)Resources.Load($"ScriptableObject/Enemies/{this.gameObject.name.Split("(")[0]}");
        this.enemyName = enemyProp.EnemyName;
        this.speed = enemyProp.Speed;
        this.hp = enemyProp.Hp;
        this.damage = enemyProp.Damage;
        this.rewardScore = enemyProp.KillReward;

        this.deadEffectObj = enemyProp.DeadEffectObj;
    }

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
    protected abstract void HandleOnDamageEffect();

    public abstract void HandleDeath();

    //附加異常(疾病)狀態
    public abstract void TakeAilmentsDamage(System.Action stateAction);
}
