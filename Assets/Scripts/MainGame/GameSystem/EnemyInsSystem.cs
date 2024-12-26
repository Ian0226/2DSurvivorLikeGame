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

    //private List<Vector2> insPosition = new List<Vector2>();

    //private int[] currentInsIndex;
    //private float currentInsSpeed;

    private float insPosOffsetMax = 0.0f;
    private float insPosOffsetMin = 0.0f;

    private int insIndex;
    private Vector2 insPos;

    /// <summary>
    /// 敵人物件池
    /// </summary>
    private ObjectPool<EnemyBase> enemyPool = null;

    public EnemyInsSystem(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        insEnemies.Add((GameObject)Resources.Load("Prefabs/Enemy/DefaultEnemy"));

        insPosOffsetMax = 15f;
        insPosOffsetMin = 10f;

        enemyPool = new ObjectPool<EnemyBase>(
            () => 
            {
                EnemyBase enemy = GameObject.Instantiate(insEnemies[insIndex], insPos, Quaternion.identity).GetComponent<EnemyBase>();
                enemy.recycle = (e) =>
                {
                    enemyPool.Release(e);
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
            },true,100,10000
            );
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))//For test
        {
            InsEnemy(0);
        }
    }

    private void InsEnemy(int index)
    {
        this.insIndex = index;
        float theta = Random.Range(0, 360);
        float insPosOffset = Random.Range(insPosOffsetMin, insPosOffsetMax);
        Vector2 playerPos = survivorLikeGame.GetPlayerPos();
        insPos = new Vector2(playerPos.x + Mathf.Cos(theta) * insPosOffset, playerPos.y + Mathf.Sin(theta) * insPosOffset);
        //Vector2 playerPos = survivorLikeGame.GetPlayerPos();
        //Vector2 insPos = new Vector2(playerPos.x + Random.Range(insPosOffsetMin, insPosOffsetMax), playerPos.y + Random.Range(insPosOffsetMin, insPosOffsetMax));
        EnemyBase enemy = enemyPool.Get();
    }
}
