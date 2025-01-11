using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// 生成敵人，控制生成位置
/// </summary>
public class EnemyInsSystem : GameSystemBase
{
    private List<GameObject> insEnemies = new List<GameObject>();

    /// <summary>
    /// 當前遊戲場景中存在的敵人
    /// </summary>
    private Hashtable currentInGameEnemies = new Hashtable();

    /// <summary>
    /// 當前場中敵人編號
    /// </summary>
    protected int enemyNumber = 0;

    //private List<Vector2> insPosition = new List<Vector2>();

    //private int[] currentInsIndex;
    //private float currentInsSpeed;

    private int insPosOffsetMax = 0;
    private int insPosOffsetMin = 0;

    private int insIndex;
    private Vector2 insPos;

    private Vector2 enemyDeadPos;

    /// <summary>
    /// 敵人物件池
    /// </summary>
    private ObjectPool<EnemyBase> enemyPoolL1 = null;
    private ObjectPool<EnemyBase> enemyPoolL2 = null;
    private ObjectPool<EnemyBase> enemyPoolL3 = null;

    public EnemyInsSystem(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        insEnemies.Add((GameObject)Resources.Load("Prefabs/Enemy/DefaultEnemy"));
        insEnemies.Add((GameObject)Resources.Load("Prefabs/Enemy/DefaultEnemyL2"));

        //敵人生成位置跟玩家的距離
        insPosOffsetMax = 25;
        insPosOffsetMin = 20;

        insIndex = 0;
        enemyPoolL1 = InitObjectPool(enemyPoolL1, 0);
        enemyPoolL2 = InitObjectPool(enemyPoolL2, 1);

    }

    private ObjectPool<EnemyBase> InitObjectPool(ObjectPool<EnemyBase> objectPool,int insIndex)
    {
        objectPool = new ObjectPool<EnemyBase>(
            () =>
            {
                EnemyBase enemy = GameObject.Instantiate(insEnemies[insIndex], insPos, Quaternion.identity).GetComponent<EnemyBase>();
                enemy.recycle = (e) =>
                {
                    if (e != null)
                        objectPool.Release(e);
                };
                return enemy;
            },
            (enemy) =>
            {
                enemy.gameObject.SetActive(true);
                enemy.transform.position = insPos;
                enemy.Initialize();
            },
            (enemy) =>
            {
                enemy.gameObject.SetActive(false);
            },
            (enemy) =>
            {
                GameObject.Destroy(enemy.gameObject);
            }, true, 100, 10000
        );
        return objectPool;
    }

    public override void Update()
    {

    }

    /// <summary>
    /// 物件池預熱(測試)
    /// </summary>
    private void ObjectPoolPreheat()
    {
        for(int i = 0;i < 50; i++)
        {
            EnemyBase enemy = enemyPoolL1.Get();
            enemy.recycle(enemy);
        }
    }

    /// <summary>
    /// 生成敵人
    /// </summary>
    /// <param name="index">生成敵人類型Index，對應存放各個種類敵人的insEnemies list</param>
    public void InsEnemy(int insIndex)
    {
        this.insIndex = insIndex;
        float theta = Random.Range(0, 360);
        int insPosOffset = Random.Range(insPosOffsetMin, insPosOffsetMax);
        int random = Random.Range(0, 2);

        Vector2 playerPos = survivorLikeGame.GetPlayerPos();
        Vector2 playerDir = survivorLikeGame.GetPlayerMoveDirection();
        if(random < 1 && playerDir.magnitude != 0)
        {
            insPos = playerPos + playerDir * 20;
            //insPos = new Vector2(playerPos.x + Mathf.Cos(theta) * insPosOffset, playerPos.y + Mathf.Sin(theta) * insPosOffset);
        }
        else
        {
            insPos = new Vector2(playerPos.x + Mathf.Cos(theta) * insPosOffset, playerPos.y + Mathf.Sin(theta) * insPosOffset);
        }
        //Vector2 playerPos = survivorLikeGame.GetPlayerPos();
        //Vector2 insPos = new Vector2(playerPos.x + Random.Range(insPosOffsetMin, insPosOffsetMax), playerPos.y + Random.Range(insPosOffsetMin, insPosOffsetMax));
        EnemyBase enemy = GetEnemy(insIndex);
        enemy.gameObject.name = enemy.EnemyName + $"({enemyNumber}";
        currentInGameEnemies.Add(enemyNumber, enemy);
        enemyNumber++;
    }

    private EnemyBase GetEnemy(int index)
    {
        switch (index)
        {
            case 0:
                return enemyPoolL1.Get();
            case 1:
                return enemyPoolL2.Get();
            default:
                return null;
        }
    }

    /// <summary>
    /// 使用index獲取遊戲場景中存在的敵人
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public EnemyBase GetEnemyInEnemies(int index)
    {
        //Debug.Log(index);
        return (EnemyBase)currentInGameEnemies[index];
    }
}
