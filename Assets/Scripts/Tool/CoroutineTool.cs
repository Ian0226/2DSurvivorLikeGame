using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTool : MonoBehaviour
{
    private static CoroutineTool _instance;
    private static CoroutineTool Instance
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

    private static long timeSecond = 0;
    public static long TimeSecond { get => timeSecond; }

    public static void ExcuteInvokeRepeatInsEnemy(System.Action repeatAction, int intervalTime)
    {
        Instance.StartCoroutine(InvokeRepeatInsEnemy(repeatAction,intervalTime));
    }

    public static void ExcuteTimer(System.Action repeatAction)
    {
        Instance.StartCoroutine(Timer(repeatAction));
    }

    private static IEnumerator InvokeRepeatInsEnemy(System.Action repeatAction,int intervalTime)
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
            timeSecond++;
            yield return new WaitForSeconds(1);
        }
    }
}
