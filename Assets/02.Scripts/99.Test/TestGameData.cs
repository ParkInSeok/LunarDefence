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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            InfinityStageManager.Instance.ObjectPoolingController.GetEnemyPool("BossMonster_Dragon Fire");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            InfinityStageManager.Instance.PathController.ReFindPath();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {

        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
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


    private void OnDrawGizmos()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction* 70, Color.blue, 1f);
        if (Physics.Raycast(ray, out hit, 100f))
        {

            print("raycast hit!");

            hit.transform.TryGetComponent<TileEventTrigger>(out var tile);
            if (tile == null)
                return;

            Debug.Log("tile row " + tile.pathNode.row + " tile column " + tile.pathNode.column);

        }
    }


}
