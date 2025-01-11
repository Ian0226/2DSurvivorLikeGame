using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System.Threading.Tasks;

public class FirebaseManager
{
    private Firebase.Auth.FirebaseAuth auth;
    private Firebase.Auth.FirebaseUser user;

    private DatabaseReference reference = null;

    private FirebaseManager() 
    { 
        Initailize(); 
    }

    private static FirebaseManager _instance = null;
    public static FirebaseManager Instance
    {
        get { if (_instance == null)
                _instance = new FirebaseManager();
            return _instance;
        }
    }

    public FirebaseAuth Auth { get => auth; set => auth = value; }
    public FirebaseUser User { get => user; set => user = value; }

    private void Initailize ()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;

        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public bool CheckCurrentUserIsNull()
    {
        if (auth.CurrentUser == null)
            return true;
        else
            return false;
    }

    public async void LoginAnonymous(string playerNickName,System.Action completeCallBack)
    {
        Debug.Log("匿名登入");
        await auth.SignInAnonymouslyAsync().ContinueWith( task => {
            //Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
            if (task.IsCanceled)
            {
                //Debug.Log($"Task is canceled, error code : {e.ErrorCode}");
                return;
            }
            if (task.IsFaulted)
            {
                //Debug.Log($"Task is faulted, error code : {e.ErrorCode}");
                return;
            }
            if (task.IsCompleted)
            {
                InitPlayerData(auth.CurrentUser.UserId, playerNickName);
                completeCallBack.Invoke();
            }
        });
    }

    //For test
    public void Logout()
    {
        Debug.Log("登出");
        auth.SignOut();
    }

    public void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if(auth.CurrentUser != user)
        {
            user = auth.CurrentUser;
            if(user != null)
            {
                //Debug.Log($"Login - {user.DisplayName}");
                Debug.Log($"Login - {user.UserId}");
            }
        }
    }

    public void InitPlayerData(string id,string nickName)
    {
        reference.Child(id).Child("PlayerNickName").SetValueAsync(nickName).ContinueWith(task => {
            if (task.IsCompletedSuccessfully)
            {
                Debug.Log($"Saved : {id}, {nickName}");
            }
        });
    }

    public void LoadPlayerNickName()
    {
        reference.Child(auth.CurrentUser.UserId).Child("PlayerNickName").GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot snapshot = task.Result;
            Debug.Log(snapshot);
            //name = snapshot.Value.ToString();
            if (task.IsCompleted)
            {
                Debug.Log(snapshot.Value.ToString());
                UserData.PlayerName = snapshot.Value.ToString();
            }
                
        });
    }

    /// <summary>
    /// 儲存玩家遊戲紀錄
    /// </summary>
    /// <param name="result"></param>
    public async Task SavePlayerGameResultData(string result)
    {
        await reference.Child(auth.CurrentUser.UserId).Child("PlayerLogs").Push().SetRawJsonValueAsync(result).ContinueWith(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                Debug.Log($"Saved : {result}");
            }
        });
        return;
    }

    /// <summary>
    /// 讀取玩家遊戲紀錄
    /// </summary>
    public async Task LoadPlayerLog()
    {
        var task = reference.Child(auth.CurrentUser.UserId).Child("PlayerLogs").GetValueAsync();
        await reference.Child(auth.CurrentUser.UserId).Child("PlayerLogs").GetValueAsync().ContinueWith(task =>
        {  
            if (task.IsFaulted)
            {
                Debug.Log("error");
            }
            if (task.IsCompletedSuccessfully)
            {
                DataSnapshot snapshot = task.Result;
                foreach (var data in snapshot.Children)
                {
                    var jsonData = data.GetRawJsonValue();
                    var resultData = JsonUtility.FromJson<GameResult>(jsonData);
                    GameResultData.GameResults.Add(resultData);
                    Debug.Log($"{resultData.Score} - {resultData.GameTime} - {resultData.RecordTime}");
                }
            }
        });
        return;
    }
}

struct PlayerData
{
    public string Name;
}