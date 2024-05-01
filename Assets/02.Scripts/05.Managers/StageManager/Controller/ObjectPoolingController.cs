using System;
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
    //public PoolType poolType;
    //public bool collectionChecks = true;

    public int maxPoolSize = 10;

   // Hero currentHero;

    //public Hero CurrentHero { get { return currentHero; } }

    Queue<Enemy> enemyPool = new Queue<Enemy>();
    Queue<Tower> towerPool = new Queue<Tower>();

    List<Transform> unitTypeParent = new List<Transform>();






    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Init()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            unitTypeParent.Add(transform.GetChild(i));
        }
        // need enemy round kyes
        SubscribeEnemy("NormalMonster_Snake"); 

        SubscribeHero(DataManager.Instance.SelectedHero);

        var selectedTowers = DataManager.Instance.SelectedTowers;
        for (int i = 0; i < selectedTowers.Count; i++)
        {
            CreateTower(selectedTowers[i]);
        }

        //tower recycle init 만들어야함
       
    }

    public void BindEvents()
    {

    }

    #region Enemy

    void SubscribeEnemy(string key)
    {
        DataManager.Instance.GetGameObject("Enemy", (obj)=>SubscribeSetEnemy(obj,key));
    }

  
    void SubscribeSetEnemy(GameObject obj, string key)
    {
        var enemy = obj.GetComponent<Enemy>();
        enemy.dieEventHandler += DieEnemyEvent;
        enemy.Init(DataManager.Instance.GameData.GetEnemyData(key));
        enemy.setModelCompletedEventHandler += () =>
        {
            InitSubscribeCreateEnemy(enemy);
        };
        enemy.transform.SetParent(unitTypeParent[(int)enemy.Stat.CurrentEnemyStat.UnitType]);

    }

    private void InitSubscribeCreateEnemy(Enemy _enemy)
    {
        for (int i = 0; i < maxPoolSize - 1; i++)
        {
            CreateEnemy("NormalMonster_Snake");
        }
        _enemy.setModelCompletedEventHandler -= () =>
        {
            InitSubscribeCreateEnemy(_enemy);
        };
    }

    void CreateEnemy(string key)
    {
        DataManager.Instance.GetGameObject("Enemy", (obj) => SetEnemy(obj, key));
    }


    void SetEnemy(GameObject obj, string key)
    {
        var enemy = obj.GetComponent<Enemy>();
        enemy.dieEventHandler += DieEnemyEvent;
        enemy.Init(DataManager.Instance.GameData.GetEnemyData(key));
    
        enemy.transform.SetParent(unitTypeParent[(int)enemy.Stat.CurrentEnemyStat.UnitType]);

    }



    private void DieEnemyEvent(Enemy enemy)
    {
        enemyPool.Enqueue(enemy);
    }

    public void GetEnemyPool(string key)
    {
        Debug.Log("enemyPool count " + enemyPool.Count);
        if(enemyPool.Count > 0)
        {
            var enemy = enemyPool.Dequeue();
            enemy.ResetModel();
            enemy.RecycleInit(DataManager.Instance.GameData.GetEnemyData(key));
          
        }
        else
        {
            CreateEnemy(key);
        }

    }

    #endregion

    #region Hero

    void SubscribeHero(string key)
    {
        DataManager.Instance.GetGameObject("Hero", (obj) => SetHero(obj, key));

    }

    void SetHero(GameObject obj, string key)
    {
        var hero = obj.GetComponent<Hero>();

        hero.dieEventHandler += DieHeroEvent;
        hero.Init(DataManager.Instance.GameData.GetHeroData(key));

        hero.transform.SetParent(unitTypeParent[(int)hero.Stat.CurrentHeroStat.UnitType]);

        //currentHero = hero;
    }

    private void DieHeroEvent(Hero obj)
    {
        //게임 패배
        InfinityStageManager.Instance.GameOver();
    }

    #endregion

    #region Tower
    void CreateTower(string key)
    {
        DataManager.Instance.GetGameObject("Tower", (obj) => SetTower(obj, key));

    }

    void SetTower(GameObject obj, string key)
    {
        var tower = obj.GetComponent<Tower>();

        tower.dieEventHandler += DieTowerEvent;
        tower.Init(DataManager.Instance.GameData.GetTowerData(key));
        

        tower.transform.SetParent(unitTypeParent[(int)tower.Stat.CurrentTowerStat.UnitType]);

    }

    private void DieTowerEvent(Tower _tower)
    {
        towerPool.Enqueue(_tower);
    }


    #endregion


    /*

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
    */


}
