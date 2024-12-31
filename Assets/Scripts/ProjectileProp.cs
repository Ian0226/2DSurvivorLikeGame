using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile",menuName = "Create projectileProp ")]
public class ProjectileProp : ScriptableObject
{
    public string projectileName = "";
    public int damage = 0;
    public float speed = 0.0f;
    public float existTime = 0.0f;
}
