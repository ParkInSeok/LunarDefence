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

[Serializable]
public class ActiveTowers
{
    public int row;
    public int column;
    public BaseUnit unit;
    public bool isHero = false;
    public ActiveTowers(int row, int column, BaseUnit unit, bool isHero = false)
    {
        this.row = row;
        this.column = column;
        this.unit = unit;
        this.isHero = isHero;
    }
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

    public List<Transform> UnitTypeParent { get { return unitTypeParent; } }

    PathNode startNode;
    PathNode targetNode;

    private List<ActiveTowers> activeTowers = new List<ActiveTowers>();

    public List<ActiveTowers> ActiveTowerList { get { return activeTowers; } }

    public void Init(PathNode _startNode, PathNode _targetNode)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            unitTypeParent.Add(transform.GetChild(i));
        }
        // need enemy round kyes

        startNode = _startNode;
        targetNode = _targetNode;

        SubscribeEnemy("NormalMonster_Snake"); 
        SubscribeHero(DataManager.Instance.SelectedHero);

        var selectedTowers = DataManager.Instance.SelectedTowers;
        for (int i = 0; i < selectedTowers.Count; i++)
        {
            CreateTower(selectedTowers[i], startNode, false);
        }

        //tower recycle init 만들어야함
        //CreateBulletAround("Flash 1");
        //CreateBulletAround("Hit 1");



    }

    public void BindEvents()
    {

    }

    #region Enemy

    void SubscribeEnemy(string key)
    {
        DataManager.Instance.GetGameObject("Enemy", (obj)=>SubscribeSetEnemy(obj,key));
    }

  
    void SubscribeSetEnemy(GameObject obj, string key, bool isActive = false)
    {
        var enemy = obj.GetComponent<Enemy>();
        enemy.dieEventHandler += DieEnemyEvent;
        enemy.Init(DataManager.Instance.GameData.GetEnemyData(key), isActive);

        enemy.transform.position = startNode.position;
        var path = InfinityStageManager.Instance.PathController.TargetPath;
        enemy.transform.LookAt(path[0].position);


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
            CreateEnemy("NormalMonster_Snake",false);
        }
        _enemy.setModelCompletedEventHandler -= () =>
        {
            InitSubscribeCreateEnemy(_enemy);
        };
    }

    void CreateEnemy(string key, bool isActive)
    {
        DataManager.Instance.GetGameObject("Enemy", (obj) => SetEnemy(obj, key, isActive));
    }


    void SetEnemy(GameObject obj, string key, bool isActive)
    {
        var enemy = obj.GetComponent<Enemy>();
        enemy.dieEventHandler += DieEnemyEvent;
        enemy.Init(DataManager.Instance.GameData.GetEnemyData(key), isActive);
    
        enemy.transform.SetParent(unitTypeParent[(int)enemy.Stat.CurrentEnemyStat.UnitType]);
        enemy.transform.position = startNode.position;

        var path = InfinityStageManager.Instance.PathController.TargetPath;
        enemy.transform.LookAt(path[0].position);

        enemy.gameObject.SetActive(isActive);

        if (isActive)
        {
            enemy.RecycleInit(DataManager.Instance.GameData.GetEnemyData(key));
        }
    }



    private void DieEnemyEvent(Enemy enemy)
    {
        enemyPool.Enqueue(enemy);
    }

    public void GetEnemyPool(string key)
    {
        Debug.Log("enemyPool count " + enemyPool.Count);
        if (enemyPool.Count > 0)
        {
            var enemy = enemyPool.Dequeue();
            enemy.ResetModel();
            enemy.RecycleInit(DataManager.Instance.GameData.GetEnemyData(key));

        }
        else
        {
            CreateEnemy(key,true);
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
        hero.transform.position = targetNode.position;
        hero.transform.localRotation = Quaternion.Euler(0, 180, 0);
        ActiveTowers data = new ActiveTowers(targetNode.row, targetNode.column, hero, true);
        activeTowers.Add(data);
        targetNode.SetUnit(TileUnitState.hero);
        //currentHero = hero;
    }

    private void DieHeroEvent(Hero obj)
    {
        //게임 패배
        StageManager.Instance.GameOver();
    }

    #endregion

    #region Tower
    void CreateTower(string key, PathNode node, bool isActive)
    {
        DataManager.Instance.GetGameObject("Tower", (obj) => SetTower(obj, key, node, isActive));

    }

    void SetTower(GameObject obj, string key, PathNode node, bool isActive)
    {
        var tower = obj.GetComponent<Tower>();

        tower.dieEventHandler += DieTowerEvent;
        tower.Init(DataManager.Instance.GameData.GetTowerData(key), isActive);

        tower.transform.SetParent(unitTypeParent[(int)tower.Stat.CurrentTowerStat.UnitType]);
        tower.transform.localRotation = Quaternion.Euler(0, 180, 0);
        tower.transform.position = node.position;
        tower.gameObject.SetActive(isActive);

        if(isActive)
        {
            tower.RecycleInit(DataManager.Instance.GameData.GetTowerData(key));

            var existData = activeTowers.Find((x) => x.row == node.row && x.column == node.column);
            if(existData != null)
            {
                activeTowers.Remove(existData);
            }
            ActiveTowers data = new ActiveTowers(node.row, node.column, tower);
            activeTowers.Add(data);
            node.SetUnit(TileUnitState.tower);
        }

    }

    private void DieTowerEvent(Tower _tower)
    {
        towerPool.Enqueue(_tower);
    }
    public void GetTowerPool(string key, PathNode node = null)
    {
        if (towerPool.Count > 0)
        {
            var tower = towerPool.Dequeue();
            tower.ResetModel();
            tower.RecycleInit(DataManager.Instance.GameData.GetTowerData(key));
            if (node == null)
                node = startNode;
            tower.transform.position = node.position;
            tower.gameObject.SetActive(true);

            var existData = activeTowers.Find((x) => x.row == node.row && x.column == node.column);
            if (existData != null)
            {
                activeTowers.Remove(existData);
            }
            ActiveTowers data = new ActiveTowers(node.row, node.column, tower);
            activeTowers.Add(data);

            node.SetUnit(TileUnitState.tower);
        }
        else
        {
            CreateTower(key,node,true);
        }

    }

    #endregion

    #region GetTowerHero
    public Hero GetCurrentHero()
    {
        var hero = activeTowers.Find((x) => x.row == targetNode.row && x.column == targetNode.column);

        if (hero == null)
            return null;

        var currentHero = (Hero)hero.unit;

        return currentHero;

    }

    public BaseUnit GetTargetTower(int row, int column)
    {
        var activeTower = activeTowers.Find((x) => x.row == row && x.column == column);

        if (activeTower == null)
            return null;

        var targetTower = activeTower.unit;

        return targetTower;

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
