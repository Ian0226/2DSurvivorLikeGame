using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Name", menuName = "Create skillProp ")]
public class SkillProp : ScriptableObject
{
    public string skillName = "";

    public int skillLevel = 0;
    public int skillMaxLevel = 0;
}
