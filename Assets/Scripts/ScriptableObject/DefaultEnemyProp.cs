using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Create enemyProp ")]
public class DefaultEnemyProp : ScriptableObject
{
    public string EnemyName = "";
    public float Speed = 0.0f;
    public int Damage = 0;
    public int KillReward = 0;
    public int Hp = 0;

    public GameObject DeadEffectObj = null;
    public AudioClip enemyTakeDamageAudio = null;
    public AudioClip enemyDeadAudio = null;
}
