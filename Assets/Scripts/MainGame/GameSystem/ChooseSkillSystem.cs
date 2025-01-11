using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseSkillSystem : GameSystemBase
{
    private SkillManager _skillManager = null;
    private ChooseSkillUI _chooseSkillUI = null;

    //存放三個隨機技能的陣列
    private List<SkillBase> randomSkills = new List<SkillBase>() { null,null,null};
    public ChooseSkillSystem(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        _skillManager = SkillManager.Instance;
        _chooseSkillUI = new ChooseSkillUI(survivorLikeGame);
        _chooseSkillUI.SkillManager = this._skillManager;
    }

    public override void Update()
    {
        _chooseSkillUI.Update();
    }

    /// <summary>
    /// 進行三選一技能選擇
    /// </summary>
    public void ChooseSkill()
    {
        GetRandomSkill();
        _chooseSkillUI.SetRandomSkillOnUI(randomSkills);
        _chooseSkillUI.Show();
    }

    public void GetRandomSkill()
    {
        int totalWeight = 0;

        // 計算權重總和
        foreach (SkillOption option in _skillManager.AllSkillContainerList)
        {
            totalWeight += option.Weight;
        }

        // 在權重範圍內生成三個隨機技能
        for (int i = 0; i < randomSkills.Count; i++)
        {
            randomSkills[i] = CreateRandomNumber(totalWeight);
            Debug.Log(randomSkills[i]);
        }
    }

    /// <summary>
    /// 生成隨機技能
    /// </summary>
    private SkillBase CreateRandomNumber(int totalWeight)
    {
        //生成一個隨機數
        int randomValue = Random.Range(0, totalWeight);

        //根據隨機數確定選項
        int cumulativeWeight = 0;
        foreach (SkillOption option in _skillManager.AllSkillContainerList)
        {
            cumulativeWeight += option.Weight;
            if (randomValue < cumulativeWeight)
            {
                if (randomSkills.Contains(option.Skill))
                {
                    return CreateRandomNumber(totalWeight);
                }
                else
                {
                    //找到技能，回傳
                    return option.Skill;
                }
            }
        }
        return null;
    }
}
