using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameDatas")]
public class GameData : ScriptableObject
{
    
    [SerializeField]protected List<TowerData> towerDatas = new List<TowerData>();
    [SerializeField]protected List<EnemyData> enemyDatas = new List<EnemyData>();
    [SerializeField]protected List<TowerData> heroDatas = new List<TowerData>();
    [SerializeField]protected List<AdvantageData> advantageDatas = new List<AdvantageData>();

    [Header("Test")]
    public List<AdvantageData> datas = new List<AdvantageData>();

    public void ClearData()
    {
        towerDatas.Clear();
        enemyDatas.Clear();
        heroDatas.Clear();
        advantageDatas.Clear();
    }

    public TowerData GetTowerData(string _uniqueKey)
    {
        var data = towerDatas.Find((x)=>x.uniqueKey.Equals(_uniqueKey) == true);
        return data;
    }


    public TowerData GetHeroData(string _uniqueKey)
    {
        var data = heroDatas.Find((x)=>x.uniqueKey.Equals(_uniqueKey) == true);
        return data;
    }



    public List<AdvantageData> GetRandomAdvantageData(int count)
    {
  
        List<int> randomIndexs = new List<int>();

        for(int i = 0; i < count; i++)
        {
            GetRandomIndex(randomIndexs);

         
        }
        Debug.Log(randomIndexs.Count);
        List<AdvantageData> randomDatas = new List<AdvantageData>();

        for (int i = 0; i < randomIndexs.Count; i++)
        {
            randomDatas.Add(advantageDatas[randomIndexs[i]]);
        }

        return randomDatas;

    }

    List<int> GetRandomIndex(List<int> list)
    {
        int minValue = 0;
        int maxValue = advantageDatas.Count;

        var randomIndex = Random.Range(minValue, maxValue);

        if(list.Contains(randomIndex) == false)
        {
            list.Add(randomIndex);
            return list;
        }
        else
        {
            return GetRandomIndex(list);
        }

    }



}
