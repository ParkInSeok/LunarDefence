using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ReturnToPool_Particle : MonoBehaviour
{
    public ParticleSystem system;
    public IObjectPool<ParticleSystem> pool;



    // Start is called before the first frame update
    void Start()
    {
        system = GetComponent<ParticleSystem>();
        var main = system.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        // Return to the pool
        pool.Release(system);
    }



}
