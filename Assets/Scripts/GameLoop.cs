using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    SceneStateController m_SceneStateController = new SceneStateController();
    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        m_SceneStateController.SetState(new StartState(m_SceneStateController), "");
    }

    private void Update()
    {
        m_SceneStateController.StateUpdate();
    }
}
