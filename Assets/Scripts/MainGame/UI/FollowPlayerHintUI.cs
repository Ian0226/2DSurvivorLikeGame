using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.CustomTool;

public class FollowPlayerHintUI : UserInterface
{
    private GameObject container = null;
    private Image attackCDHintImg = null;
    private Image playerScoreHintImg = null;
    public FollowPlayerHintUI(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public Image AttackCDHintImg { get => attackCDHintImg; set => attackCDHintImg = value; }

    public override void Initialize()
    {
        container = UnityTool.FindGameObject("PlayerHintCanvas");
        AttackCDHintImg = UITool.GetUIComponent<Image>(container, "AttackCDImage");
        playerScoreHintImg = UITool.GetUIComponent<Image>(container, "PlayerScoreImage");

        playerScoreHintImg.fillAmount = 0;
    }

    public void SetAttackCDImgFillAmount(float fillAmount)
    {
        AttackCDHintImg.fillAmount = fillAmount;
    }

    public void SetPlayerScoreHintImgFillAmount(float fillAmount)
    {
        playerScoreHintImg.fillAmount = fillAmount;
    }
}
