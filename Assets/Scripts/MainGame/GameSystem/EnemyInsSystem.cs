using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// �ͦ��ĤH�A����ͦ���m
/// </summary>
public class EnemyInsSystem : GameSystemBase
{
    private List<GameObject> insEnemies = new List<GameObject>();

    /// <summary>
    /// ��e�C���������s�b���ĤH
    /// </summary>
    private Hashtable currentInGameEnemies = new Hashtable();

    /// <summary>
    /// ��e�����ĤH�s��
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
    /// �ĤH�����
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

        //�ĤH�ͦ���m�򪱮a���Z��
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
    /// ������w��(����)
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
    /// �ͦ��ĤH
    /// </summary>
    /// <param name="index">�ͦ��ĤH����Index�A�����s��U�Ӻ����ĤH��insEnemies list</param>
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
    /// �ϥ�index����C���������s�b���ĤH
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public EnemyBase GetEnemyInEnemies(int index)
    {
        //Debug.Log(index);
        return (EnemyBase)currentInGameEnemies[index];
    }
}
