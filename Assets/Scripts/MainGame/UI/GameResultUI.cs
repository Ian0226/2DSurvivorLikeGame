using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.CustomTool;
using DG.Tweening;
using TMPro;

public class GameResultUI : UserInterface
{
    private GameObject gameResultCanvas = null;
    private RectTransform gameResultPanel = null;

    private TextMeshProUGUI gameResultText = null;
    private TextMeshProUGUI gameScoreText = null;
    private TextMeshProUGUI gameHighestTimeText = null;
    public GameResultUI(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        gameResultCanvas = UnityTool.FindGameObject("GameResultCanvas");
        gameResultPanel = UITool.GetUIComponent<RectTransform>(gameResultCanvas, "GameResultPanel");

        gameResultText = UITool.GetUIComponent<TextMeshProUGUI>(gameResultPanel.gameObject, "GameResultText");
        gameScoreText = UITool.GetUIComponent<TextMeshProUGUI>(gameResultPanel.gameObject, "GameScoreText");
        gameHighestTimeText = UITool.GetUIComponent<TextMeshProUGUI>(gameResultPanel.gameObject, "HighestRecordText");
    }

    public override void Show()
    {
        HandleUIAnimation();
    }

    public void SetGameResult(GameResult gameResult,int heighstRecordTime)
    {
        gameResultText.text = "�C������";
        //gameScoreText.text = $"��o���� : {gameResult.Score} " +
            //$"�s���ɶ� : {gameResult.GameTime / 60} �� {gameResult.GameTime%60} ��";
        gameScoreText.text = $"�s���ɶ� : {gameResult.GameTime / 60} �� {gameResult.GameTime % 60} ��";
        gameHighestTimeText.text = $"�̰��s���ɶ����� : {heighstRecordTime / 60} �� {heighstRecordTime % 60} ��";
    }

    public void SetHighestGameTimeText(int time)
    {
        
    }

    private void HandleUIAnimation()
    {
        gameResultPanel.DOAnchorPosY(0, 0.45f).SetUpdate(true);
    }
}
