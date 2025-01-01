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

    private float insPosOffsetMax = 0.0f;
    private float insPosOffsetMin = 0.0f;

    private int insIndex;
    private Vector2 insPos;

    private Vector2 enemyDeadPos;

    /// <summary>
    /// �ĤH�����
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

        insIndex = 0;

        enemyPool = new ObjectPool<EnemyBase>(
            () => 
            {
                EnemyBase enemy = GameObject.Instantiate(insEnemies[insIndex], insPos, Quaternion.identity).GetComponent<EnemyBase>();
                enemy.recycle = (e) =>
                {
                    if(e != null)
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
            },true,50,1000
        );
    }

    public override void Update()
    {

    }

    /// <summary>
    /// �ͦ��ĤH
    /// </summary>
    /// <param name="index">�ͦ��ĤH����Index�A�����s��U�Ӻ����ĤH��insEnemies list</param>
    public void InsEnemy()
    {
        float theta = Random.Range(0, 360);
        float insPosOffset = Random.Range(insPosOffsetMin, insPosOffsetMax);
        Vector2 playerPos = survivorLikeGame.GetPlayerPos();
        insPos = new Vector2(playerPos.x + Mathf.Cos(theta) * insPosOffset, playerPos.y + Mathf.Sin(theta) * insPosOffset);
        //Vector2 playerPos = survivorLikeGame.GetPlayerPos();
        //Vector2 insPos = new Vector2(playerPos.x + Random.Range(insPosOffsetMin, insPosOffsetMax), playerPos.y + Random.Range(insPosOffsetMin, insPosOffsetMax));
        EnemyBase enemy = enemyPool.Get();
        enemy.gameObject.name = enemy.EnemyName + $"_{enemyNumber}";
        currentInGameEnemies.Add(enemyNumber, enemy);
        enemyNumber++;
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
