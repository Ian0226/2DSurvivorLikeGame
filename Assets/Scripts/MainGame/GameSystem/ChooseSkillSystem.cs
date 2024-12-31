using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseSkillSystem : GameSystemBase
{
    private SkillManager _skillManager = null;
    private ChooseSkillUI _chooseSkillUI = null;
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
        _chooseSkillUI.Show();
    }
}
