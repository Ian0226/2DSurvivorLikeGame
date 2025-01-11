using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseSkillSystem : GameSystemBase
{
    private SkillManager _skillManager = null;
    private ChooseSkillUI _chooseSkillUI = null;

    //�s��T���H���ޯ઺�}�C
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
    /// �i��T��@�ޯ���
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

        // �p���v���`�M
        foreach (SkillOption option in _skillManager.AllSkillContainerList)
        {
            totalWeight += option.Weight;
        }

        // �b�v���d�򤺥ͦ��T���H���ޯ�
        for (int i = 0; i < randomSkills.Count; i++)
        {
            randomSkills[i] = CreateRandomNumber(totalWeight);
            Debug.Log(randomSkills[i]);
        }
    }

    /// <summary>
    /// �ͦ��H���ޯ�
    /// </summary>
    private SkillBase CreateRandomNumber(int totalWeight)
    {
        //�ͦ��@���H����
        int randomValue = Random.Range(0, totalWeight);

        //�ھ��H���ƽT�w�ﶵ
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
                    //���ޯ�A�^��
                    return option.Skill;
                }
            }
        }
        return null;
    }
}
