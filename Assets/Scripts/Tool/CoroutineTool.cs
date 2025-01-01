using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTool : MonoBehaviour
{
    private static CoroutineTool _instance;
    public static CoroutineTool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("MethodDelay").AddComponent<CoroutineTool>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public static void ExcuteInvokeRepeatInsEnemy(System.Action repeatAction, float intervalTime)
    {
        Instance.StartCoroutine(InvokeRepeatInsEnemy(repeatAction,intervalTime));
    }

    public static void ExcuteTimer(System.Action repeatAction)
    {
        Instance.StartCoroutine(Timer(repeatAction));
    }

    private static IEnumerator InvokeRepeatInsEnemy(System.Action repeatAction,float intervalTime)
    {
        while (true)
        {
            repeatAction.Invoke();
            yield return new WaitForSeconds(intervalTime);
        }
    }

    private static IEnumerator Timer(System.Action repeatAction)
    {
        Debug.Log("計時開始");
        while (true)
        {
            repeatAction.Invoke();
            yield return new WaitForSeconds(1);
        }
    }

    /// <summary>
    /// 延遲執行一個Action
    /// </summary>
    /// <param name="action"></param>
    /// <param name="delayTime"></param>
    public void DelayExcuteAction(System.Action action, float delayTime)
    {
        StartCoroutine(ExcuteAction(action, delayTime));
    }

    private IEnumerator ExcuteAction(System.Action action, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        action.Invoke();
    }
}
