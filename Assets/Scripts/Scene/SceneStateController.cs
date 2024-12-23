using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateController
{
    private SceneState m_State;
    private bool runBegin = false;

    private AsyncOperation loadSceneAsyncOperation = null;

    public SceneStateController() { }

    public void SetState(SceneState state,string loadSceneName)
    {
        runBegin = false;

        LoadScene(loadSceneName);

        if (m_State != null)
            m_State.StateEnd();

        m_State = state;
    }

    private void LoadScene(string loadSceneName)
    {
        if (loadSceneName == null || loadSceneName.Length == 0) return;
        loadSceneAsyncOperation = SceneManager.LoadSceneAsync(loadSceneName);
        Debug.Log("Scene Load");
    }

    public void StateUpdate()
    {
        if(loadSceneAsyncOperation != null && !loadSceneAsyncOperation.isDone)
        {
            Debug.Log((loadSceneAsyncOperation.progress * 100) + "%");
            return;
        }

        if (m_State != null && runBegin == false)
        {
            m_State.StateBegin();
            runBegin = true;
        }

        if (m_State != null)
            m_State.StateUpdate();
    }
}
