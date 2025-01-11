using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 儲存全部技能
/// </summary>
public class SkillManager
{
    private SkillManager() 
    {
        Initialize();
    }
    private static SkillManager _instance;
    public static SkillManager Instance
    {
        get { if (_instance == null) 
                _instance = new SkillManager();
            return _instance;
        }
    }

    private List<SkillOption> allSkillContainerList = new List<SkillOption>();
    public List<SkillOption> AllSkillContainerList { get => allSkillContainerList;}

    private void Initialize()
    {
        PlayerController _playerController = SurvivorLikeGame2DFacade.Instance.GetPlayerController();

        //目前使用Add加入技能至List中
        allSkillContainerList.Add(new SkillOption(new SkillAddDamage(_playerController),5));
        allSkillContainerList.Add(new SkillOption(new SkillAddHp(_playerController),5));
        allSkillContainerList.Add(new SkillOption(new SkillAddSpeed(_playerController),5));
        allSkillContainerList.Add(new SkillOption(new SkillAddAtkSpeed(_playerController),5));
        allSkillContainerList.Add(new SkillOption(new SkillRecoverHp(_playerController),1));

        //更換投射物技能
        allSkillContainerList.Add(new SkillOption(new SkillChangeProjectile(_playerController), 5,
            (GameObject)Resources.Load("Prefabs/Projectiles/EnergyProjectile")));
    }
    
    public SkillOption SearchSkillOption(string skillName)
    {
        foreach(SkillOption option in allSkillContainerList)
        {
            if (option.SkillName == skillName)
                return option;
        }
        return null;
    }
}

public class SkillOption
{
    public SkillOption(SkillBase skill,int weight)
    {
        this.Skill = skill;
        this.Weight = weight;
        this.SkillName = skill.SkillName;
    }
    public SkillOption(SkillActiveAttackBase skill,int weight,GameObject projectileObj)
    {
        skill.SkillAction(projectileObj);
        this.Skill = skill;
        this.Weight = weight;
        this.SkillName = skill.SkillName;
    }
    public SkillBase Skill = null;
    public int Weight = 0;
    public string SkillName = "";
}
