using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

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
        Debug.Log("°Î¦Wµn¤J");
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
        Debug.Log("µn¥X");
        auth.SignOut();
    }

    public void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if(auth.CurrentUser != user)
        {
            user = auth.CurrentUser;
            if(user != null)
            {
                Debug.Log($"Login - {user.DisplayName}");
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
        //string name = "";
        //Debug.Log(user.UserId);
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
}

struct PlayerData
{
    public string Name;
}