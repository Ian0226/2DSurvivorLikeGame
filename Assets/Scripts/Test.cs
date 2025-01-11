using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        SurvivorLikeGame2DFacade.Instance.Initialize();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SurvivorLikeGame2DFacade.Instance.Update();
        TestFunc();
    }

    private void FixedUpdate()
    {
        //SurvivorLikeGame2DFacade.Instance.FixedUpdate();
    }

    private void LateUpdate()
    {
        SurvivorLikeGame2DFacade.Instance.LateUpdate();
    }

    private void TestFunc()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            FirebaseManager.Instance.LoadPlayerLog();
        }
    }
}
