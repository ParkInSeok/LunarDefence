using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public enum PoolType
{
    Stack,
    LinkedList

}



public class ObjectPoolingController : MonoBehaviour 
{
    public PoolType poolType;

    public bool collectionChecks = true;
    public int maxPoolSize = 10;
    IObjectPool<ParticleSystem> m_Pool;

    public IObjectPool<ParticleSystem> Pool
    {
        get
        {
            if (m_Pool == null)
            {
                if (poolType == PoolType.Stack)
                    m_Pool = new ObjectPool<ParticleSystem>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
                else
                    m_Pool = new LinkedPool<ParticleSystem>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
            }
            return m_Pool;
        }
    }




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Init()
    {

    }

    public void BindEvents()
    {

    }

  


    ParticleSystem CreatePooledItem()
    {
        var go = new GameObject("Pooled Particle System");
        var ps = go.AddComponent<ParticleSystem>();
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        var main = ps.main;
        main.duration = 1;
        main.startLifetime = 1;
        main.loop = false;

        // This is used to return ParticleSystems to the pool when they have stopped.
        var returnToPool = go.AddComponent<ReturnToPool_Particle>();
        returnToPool.pool = Pool;

        return ps;
    }


    void OnReturnedToPool(ParticleSystem system)
    {
        system.gameObject.SetActive(false);
    }

    // Called when an item is taken from the pool using Get
    void OnTakeFromPool(ParticleSystem system)
    {
        system.gameObject.SetActive(true);
    }

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    void OnDestroyPoolObject(ParticleSystem system)
    {
        Destroy(system.gameObject);
    }

    void OnGUI()
    {
        GUILayout.Label("Pool size: " + Pool.CountInactive);
        if (GUILayout.Button("Create Particles"))
        {
            var amount = Random.Range(1, 10);
            for (int i = 0; i < amount; ++i)
            {
                var ps = Pool.Get();
                ps.transform.position = Random.insideUnitSphere * 10;
                ps.Play();
            }
        }
    }

}
