using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameData : MonoBehaviour
{

    public GameData data;


    public int getCount;
    public GameObject testObject;
    public Sprite testsprite;

    public Enemy currentEnemy;
    public Tower currentTower;
    public Hero currentHero;

    public float length;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StageManager.Instance.ObjectPoolingController.GetEnemyPool("BossMonster_Dragon Fire");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            StageManager.Instance.ObjectPoolingController.GetEnemyPool("NormalMonster_Snake");
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            //InfinityStageManager.Instance.ObjectPoolingController.GetTowerPool("Tower_Angel Mage");
            StageManager.Instance.ObjectPoolingController.GetTowerPool("Tower_Ice Mage");
        
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            currentTower.ChangeAnimateState(UnitAnimateState.Attack);
            //InfinityStageManager.Instance.PathController.ReFindPath();
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            //InfinityStageManager.Instance.ObjectPoolingController.GetBulletAroundPool("Flash 1",SetTowerAttackPoint);
            NetworkManager.Instance.GetRandomNumber(1, 100, (randomNumber)=>
            {
                Debug.Log("Random Number: " + randomNumber);
            });
            

        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {

        }

        if (Input.GetKeyDown(KeyCode.F8))
        {

        }

        if (Input.GetKeyDown(KeyCode.F9))
        {

        }

        if (Input.GetKeyDown(KeyCode.F10))
        {

        }



    }

    private void SetTowerAttackPoint(ReturnToPool_Particle obj)
    {
        obj.transform.position = currentTower.CurrentAnimatorController.AttackPoint.position;
        obj.transform.rotation = currentTower.CurrentAnimatorController.AttackPoint.rotation;
        obj.StartParticle();
    }

    public void SetSprite(Sprite set)
    {
        testsprite = set;
        
    }

    void SetGameObject(GameObject set)
    {
        testObject = set;

        var enemy = testObject.GetComponent<Enemy>();

        enemy.Init(DataManager.Instance.GameData.GetEnemyData("NormalMonster_Snake"));
       
        currentEnemy = enemy;
    }
    void SetTowerGameObject(GameObject set)
    {
        testObject = set;

        var tower = testObject.GetComponent<Tower>();

        tower.Init(DataManager.Instance.GameData.GetTowerData("Tower_SunfloraPixie"));

        currentTower = tower;
    }

    void SetHeroGameObject(GameObject set)
    {
        testObject = set;

        var hero = testObject.GetComponent<Hero>();

        hero.Init(DataManager.Instance.GameData.GetHeroData("BlastRobotBlueHeroTest"));

        currentHero = hero;
    }


    //private void OnDrawGizmos()
    //{
    //    RaycastHit hit;
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    Debug.DrawRay(ray.origin, ray.direction* length, Color.blue, 1f);
       
    //}


}
