using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    public delegate void Recycle(ParticleHandler dEParticleHandler);
    public Recycle recycle;

    private AudioSource deadAudio = null;

    public void Initialize()
    {
        deadAudio = this.GetComponent<AudioSource>();
        deadAudio.clip = (AudioClip)Resources.Load("AudioClips/Enemy/enemyDead");

        deadAudio.PlayOneShot(deadAudio.clip);

        this.GetComponent<ParticleSystem>().Play();
        
        CoroutineTool.Instance.DelayExcuteAction(() => { recycle(this); }, 1.5f);
    }
}
