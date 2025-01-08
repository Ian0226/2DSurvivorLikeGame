using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    public delegate void Recycle(ParticleHandler dEParticleHandler);
    public Recycle recycle;

    public void Initialize()
    {
        this.GetComponent<ParticleSystem>().Play();
        CoroutineTool.Instance.DelayExcuteAction(() => { recycle(this); }, 1.5f);
    }
}
