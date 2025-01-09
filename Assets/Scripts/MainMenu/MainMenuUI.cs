using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.CustomTool;
using DG.Tweening;
using TMPro;

public class MainMenuUI 
{
    private MainMenuHandler _mainMenuHandler = null;

    private GameObject uiContainer = null;

    private RectTransform mainUIPanel = null;
    private TextMeshProUGUI welcomeText = null;
    private Button startGameButton = null;
    private Button settingButton = null;
    private Button exitGameButton = null;

    private RectTransform inputNamePanel = null;
    private TMP_InputField playerNameInput = null;
    private Button confirmButton = null;

    private string playerName = "";
    
    public MainMenuUI(MainMenuHandler mainMenuHandler)
    {
        _mainMenuHandler = mainMenuHandler;
        Initialize();
    }

    public void Initialize()
    {
        uiContainer = UnityTool.FindGameObject("MainCanvas");
        //main panel
        mainUIPanel = UITool.GetUIComponent<RectTransform>(uiContainer, "MainMenuPanel");
        welcomeText = UITool.GetUIComponent<TextMeshProUGUI>(mainUIPanel.gameObject, "WelcomeText");
        startGameButton = UITool.GetUIComponent<Button>(mainUIPanel.gameObject, "StartButton");
        settingButton = UITool.GetUIComponent<Button>(mainUIPanel.gameObject, "SettingButton");
        exitGameButton = UITool.GetUIComponent<Button>(mainUIPanel.gameObject, "ExitButton");

        //input name panel
        inputNamePanel = UITool.GetUIComponent<RectTransform>(uiContainer, "InputNameUIPanel");
        playerNameInput = UITool.GetUIComponent<TMP_InputField>(inputNamePanel.gameObject, "InputNickName");
        playerNameInput.onValueChanged.AddListener((s) => 
        { 
            CheckPlayerInputName(s); 
        });

        confirmButton = UITool.GetUIComponent<Button>(inputNamePanel.gameObject, "ButtonConfirm");

        mainUIPanel.anchoredPosition = new Vector3(0, 1100, 0);

        InitButton();
    }

    public void Update()
    {

    }

    private void InitButton()
    {
        confirmButton.onClick.AddListener(() =>
        {
            welcomeText.text = $"Åwªï¡A{playerName}";
            FirebaseManager.Instance.LoginAnonymous(playerName, ShowMainMenuPanel);
        });

        startGameButton.onClick.AddListener(() => 
        {
            _mainMenuHandler.ChangeSceneStateAction.Invoke();
        });

        settingButton.onClick.AddListener(() =>
        {

        });

        exitGameButton.onClick.AddListener(() =>
        {

        });
    }

    public void SetWelcomeText()
    {
        this.welcomeText.text = $"Åwªï¡A{UserData.PlayerName}";
    }

    private void CheckPlayerInputName(string name)
    {
        playerName = name;
    }


    public void ShowInputNamePanel()
    {
        //mainUIPanel.DOAnchorPosY(1100, 0.5f).OnComplete(() => inputNamePanel.DOAnchorPosY(0, 0.5f));
        inputNamePanel.DOAnchorPosY(0, 0.5f);
    }

    public void ShowMainMenuPanel()
    {
        inputNamePanel.DOAnchorPosY(-1080, 0.5f).OnStart(() => mainUIPanel.DOAnchorPosY(0, 0.5f));
    }
}
