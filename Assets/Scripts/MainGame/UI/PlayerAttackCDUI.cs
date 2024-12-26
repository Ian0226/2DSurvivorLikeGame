using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.CustomTool;

public class PlayerAttackCDUI : UserInterface
{
    private GameObject container = null;
    private Image attackCDHintImg = null;
    public PlayerAttackCDUI(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public Image AttackCDHintImg { get => attackCDHintImg; set => attackCDHintImg = value; }

    public override void Initialize()
    {
        container = UnityTool.FindGameObject("PlayerHintCanvas");
        AttackCDHintImg = UITool.GetUIComponent<Image>(container, "AttackCDImage");
    }

    public void SetAttackCDImgFillAmount(float fillAmount)
    {
        AttackCDHintImg.fillAmount = fillAmount;
    }
}
