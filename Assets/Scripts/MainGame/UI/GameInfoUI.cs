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
    private TextMeshProUGUI playerScoreText = null;
    public GameInfoUI(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        containerCancas = UnityTool.FindGameObject("GameInfoUI");

        testGameInfoText = UITool.FindGameObjectInSpecificCanvas("GameInfoUI", "GameTime").GetComponent<TextMeshProUGUI>();
        playerHPText = UITool.FindGameObjectInSpecificCanvas("GameInfoUI", "HPText").GetComponent<TextMeshProUGUI>();
        playerScoreText = UITool.FindGameObjectInSpecificCanvas("GameInfoUI", "PlayerScoreText").GetComponent<TextMeshProUGUI>();
    }

    public override void Update()
    {
        testGameInfoText.text = "Wave : " + survivorLikeGame.GetCurrentWave() + " "
            + (survivorLikeGame.GetGameTime() / 60).ToString("d2") + " : " + (survivorLikeGame.GetGameTime() % 60).ToString("d2");

        playerHPText.text = survivorLikeGame.GetPlayerHP().ToString();

        playerScoreText.text = survivorLikeGame.GetPlayerScore().ToString();
    }
}
