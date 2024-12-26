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
    public GameInfoUI(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        containerCancas = UnityTool.FindGameObject("GameInfoUI");

        testGameInfoText = UITool.FindGameObjectInSpecificCanvas("GameInfoUI", "GameTime").GetComponent<TextMeshProUGUI>();
    }

    public override void Update()
    {
        testGameInfoText.text = (CoroutineTool.TimeSecond / 60).ToString("d2") + " : " + (CoroutineTool.TimeSecond % 60).ToString("d2");
    }
}
