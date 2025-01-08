using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �x�s�����ޯ�
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

    private List<SkillBase> allSkillContainerList = new List<SkillBase>();
    public List<SkillBase> AllSkillContainerList { get => allSkillContainerList;}

    private void Initialize()
    {
        PlayerController _playerController = SurvivorLikeGame2DFacade.Instance.GetPlayerController();
        //�ثe�ϥ�Add�[�J�ޯ��List��
        allSkillContainerList.Add(new SkillAddDamage(_playerController));
        allSkillContainerList.Add(new SkillAddHp(_playerController));
        allSkillContainerList.Add(new SkillAddSpeed(_playerController));
        allSkillContainerList.Add(new SkillAddAtkSpeed(_playerController));
    }
}
