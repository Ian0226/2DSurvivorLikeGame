using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.CustomTool;

public class GameInfoUI : UserInterface
{
    private GameObject containerCancas = null;
    private TextMeshProUGUI testGameInfoText = null;
    private TextMeshProUGUI playerHPText = null;
    public GameInfoUI(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        containerCancas = UnityTool.FindGameObject("GameInfoUI");

        testGameInfoText = UITool.FindGameObjectInSpecificCanvas("GameInfoUI", "GameTime").GetComponent<TextMeshProUGUI>();
        playerHPText = UITool.FindGameObjectInSpecificCanvas("GameInfoUI", "HPText").GetComponent<TextMeshProUGUI>();
    }

    public override void Update()
    {
        testGameInfoText.text = "Wave : " + survivorLikeGame.GetCurrentWave() + " "
            + (CoroutineTool.TimeSecond / 60).ToString("d2") + " : " + (CoroutineTool.TimeSecond % 60).ToString("d2");

        playerHPText.text = survivorLikeGame.GetPlayerHP().ToString();
    }
}
