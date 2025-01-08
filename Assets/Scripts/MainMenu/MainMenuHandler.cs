using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler
{
    private MainMenuUI _mainMenuUI = null;
    private MainMenuHandler(){ }

    private static MainMenuHandler _instance = null;
    public static MainMenuHandler Instance
    {
        get { if (_instance == null)
                _instance = new MainMenuHandler();
            return _instance;
        }
    }

    public void Initailize()
    {
        _mainMenuUI = new MainMenuUI();

        LoginHandler();
    }

    public void Update()
    {
        _mainMenuUI.Update();

        if (Input.GetKeyDown(KeyCode.O))//Test
        {
            FirebaseManager.Instance.Logout();
        }
    }

    private void LoginHandler()
    {
        if (FirebaseManager.Instance.CheckCurrentUserIsNull())
        {
            Debug.Log("創建新帳號");
            _mainMenuUI.ShowInputNamePanel();
        }
        else
        {
            Debug.Log($"登入使用者 : {FirebaseManager.Instance.Auth.CurrentUser}");
            FirebaseManager.Instance.LoadPlayerNickName();
            CoroutineTool.Instance.DelayExcuteAction(() => { 
                _mainMenuUI.SetWelcomeText();
                _mainMenuUI.ShowMainMenuPanel();
            }, 2f);
            //FirebaseManager.Instance.LoginAnonymous();
        }
    }

    //methods
    /// <summary>
    /// 
    /// </summary>
    public void ShowInputNamePanel()
    {
        _mainMenuUI.ShowInputNamePanel();
    }
}
