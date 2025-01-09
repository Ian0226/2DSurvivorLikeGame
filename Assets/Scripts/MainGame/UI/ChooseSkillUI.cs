using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.CustomTool;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class ChooseSkillUI : UserInterface
{
    private GameObject chooseSkillCanvas = null;
    private RectTransform chooseSkillPanel = null;

    private SkillManager _skillManager = null;

    private Button skillSelectionBtn01 = null;
    private Button skillSelectionBtn02 = null;
    private Button skillSelectionBtn03 = null;

    private Button confiromBtn = null;

    private Vector2 chooseSkillPanelOriginPos;

    public SkillManager SkillManager { get => _skillManager; set => _skillManager = value; }

    public ChooseSkillUI(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        chooseSkillCanvas = UnityTool.FindGameObject("ChooseSkillUICanvas");
        chooseSkillPanel = UITool.GetUIComponent<RectTransform>(chooseSkillCanvas, "ChooseSkillPanel");

        skillSelectionBtn01 = UITool.GetUIComponent<Button>(chooseSkillPanel.gameObject, "Selection_1");
        skillSelectionBtn02 = UITool.GetUIComponent<Button>(chooseSkillPanel.gameObject, "Selection_2");
        skillSelectionBtn03 = UITool.GetUIComponent<Button>(chooseSkillPanel.gameObject, "Selection_3");

        //confiromBtn = UITool.GetUIComponent<Button>(chooseSkillPanel.gameObject, "ConfiromBtn");
        //confiromBtn.onClick.AddListener(Release);

        chooseSkillPanelOriginPos = chooseSkillPanel.anchoredPosition;

        chooseSkillCanvas.SetActive(false);
    }

    public override void Update()
    {

    }

    public override void Show()
    {
        chooseSkillCanvas.SetActive(true);
        HandleUIAnimation();
    }
    
    private void HandleUIAnimation()
    {
        RandomSkills();
        if (chooseSkillCanvas.gameObject.activeInHierarchy)
            chooseSkillPanel.DOAnchorPosY(0, 0.45f).SetUpdate(true);
    }

    /// <summary>
    /// 隨機選取三項技能，並呼叫設定UI的方法將技能顯示於UI上
    /// </summary>
    private void RandomSkills()
    {
        if (_skillManager.AllSkillContainerList.Count < 3) return;

        List<int> randomNum = new List<int>();
        for(int i = 0; i < 3; i++)//產生三個隨機數
        {
            int r = Random.Range(0, _skillManager.AllSkillContainerList.Count);
            while(randomNum.Contains(r))
            {
                r = Random.Range(0, _skillManager.AllSkillContainerList.Count);
            }
            randomNum.Add(r);
        }
        //skillSelectionBtn01.onClick.AddListener(skillManager.AllSkillContainerList[0].UseSkill);//Test
        SetButtonUI(skillSelectionBtn01, _skillManager.AllSkillContainerList[randomNum[0]].UseSkill,
            _skillManager.AllSkillContainerList[randomNum[0]].SkillDisplayName, 
            _skillManager.AllSkillContainerList[randomNum[0]].SkillIcon,
            _skillManager.AllSkillContainerList[randomNum[0]].SkillInfo
            );
        SetButtonUI(skillSelectionBtn02, _skillManager.AllSkillContainerList[randomNum[1]].UseSkill,
            _skillManager.AllSkillContainerList[randomNum[1]].SkillDisplayName,
            _skillManager.AllSkillContainerList[randomNum[1]].SkillIcon,
            _skillManager.AllSkillContainerList[randomNum[1]].SkillInfo
            );
        SetButtonUI(skillSelectionBtn03, _skillManager.AllSkillContainerList[randomNum[2]].UseSkill,
            _skillManager.AllSkillContainerList[randomNum[2]].SkillDisplayName,
            _skillManager.AllSkillContainerList[randomNum[2]].SkillIcon,
            _skillManager.AllSkillContainerList[randomNum[2]].SkillInfo
            );
    }

    private void SetButtonUI(Button btn,System.Action onClickAction,string showText,Sprite icon,string SkillInfo)
    {
        btn.onClick.RemoveAllListeners();//清除上一次的Action紀錄
        btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = showText;
        btn.transform.GetChild(1).GetComponent<Image>().sprite = icon;
        if (showText.Contains("_Max"))//技能等級已達最大值
            return;

        btn.onClick.AddListener( () => { 
            onClickAction.Invoke();
            Release();
        });
        
    }

    public override void Release()
    {
        chooseSkillPanel.DOAnchorPosY(chooseSkillPanelOriginPos.y, 0.45f).SetUpdate(true).
            OnComplete(() => {
                chooseSkillCanvas.SetActive(false);
                survivorLikeGame.GameContinue();
            });
    }
}
