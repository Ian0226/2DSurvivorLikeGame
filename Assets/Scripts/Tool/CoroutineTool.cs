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

    public static void ExecuteCDCount(PlayerController player, float cd, float currentCD)
    {
        Instance.StartCoroutine(CDCountCoroutine(player, cd, currentCD));
        Debug.Log("StartCoroutine");
    }

    private static IEnumerator CDCountCoroutine(PlayerController player,float cd,float currentCD)
    {
        while(currentCD < cd)
        {
            currentCD += 1 * Time.deltaTime;
            Debug.Log(currentCD);
        }
        yield return new WaitUntil(() => currentCD >= cd);
        player.CanAttack = true;
    }
}
