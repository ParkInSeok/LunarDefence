using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ReturnToPool_Particle : MonoBehaviour
{
    protected ParticleSystem system;
    protected List<ParticleSystem> systems = new List<ParticleSystem>();
    public Action particleStoppedEventHandler;

    

    public virtual void Init()
    {
        if(system == null)
        {
            system = GetComponent<ParticleSystem>();
        }

        var main = system.main;
        main.stopAction = ParticleSystemStopAction.Callback;

        if (transform.childCount <= 0)
            return;

        var _systems = GetComponentsInChildren<ParticleSystem>();

        if (_systems == null)
            return;

        for (int i = 0; i < _systems.Length; i++)
        {
            systems.Add(_systems[i]);
        }

       
    }

  


    public void StartParticle()
    {
        for (int i = 0; i < systems.Count; i++)
        {
            systems[i].Play();

        }
    }

    protected void StopParticle(bool withChildren = true, ParticleSystemStopBehavior particleSystemStopBehavior = ParticleSystemStopBehavior.StopEmitting)
    {
        system.Stop(withChildren, particleSystemStopBehavior);
    }


    protected virtual void OnParticleSystemStopped()
    {

        particleStoppedEventHandler?.Invoke();
    }



}
