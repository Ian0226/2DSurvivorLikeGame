using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.CustomTool;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    private GameObject testPanel = null;
    private Text testText = null;
    private void Start()
    {
        testPanel = UnityTool.FindGameObject("TestPanel");
        testText = testPanel.transform.GetChild(0).GetComponent<Text>();
    }

    private void Update()
    {
        testText.text = "HP " + SurvivorLikeGame2DFacade.Instance.GetPlayerHP() + "\n" 
            + "MoveSpeed " + SurvivorLikeGame2DFacade.Instance.GetPlayerController().MoveSpeed + "\n" 
            + "AttackCDTime " + SurvivorLikeGame2DFacade.Instance.GetPlayerController().AttackCDTime;
    }
}
