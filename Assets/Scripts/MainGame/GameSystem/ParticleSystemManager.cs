using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ParticleSystemManager : GameSystemBase
{
    private ObjectPool<ParticleHandler> inGameParticlesPool = null;

    private GameObject createdParticleObj = null;
    private Vector2 insPos;
    public ParticleSystemManager(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        inGameParticlesPool = new ObjectPool<ParticleHandler>(() =>
        {
            ParticleHandler pObj = GameObject.Instantiate(createdParticleObj, insPos, Quaternion.identity).GetComponent<ParticleHandler>();
            pObj.recycle = (p) =>
            {
                inGameParticlesPool.Release(p);
            };
            return pObj;
        },
        (pObj) =>
        {
            pObj.gameObject.SetActive(true);
            pObj.transform.position = insPos;
            pObj.Initialize();
        },
        (pObj) =>
        {
            pObj.gameObject.SetActive(false);
        },
        (pObj) =>
        {
            GameObject.Destroy(pObj.gameObject);
        }, true, 10, 1000
        );
    }

    public void CreateParticle(GameObject particleObj,Vector2 insPos)
    {
        this.createdParticleObj = particleObj;
        this.insPos = insPos;
        inGameParticlesPool.Get();
    }
}
