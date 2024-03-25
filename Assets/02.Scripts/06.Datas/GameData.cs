using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameDatas")]
public class GameData : ScriptableObject
{
    
    [SerializeField] protected string version;
    [SerializeField]protected List<TowerData> towerDatas = new List<TowerData>();
    [SerializeField]protected List<EnemyData> enemyDatas = new List<EnemyData>();
    [SerializeField]protected List<HeroData> heroDatas = new List<HeroData>();
    [SerializeField]protected List<AdvantageData> advantageDatas = new List<AdvantageData>();
    [SerializeField]protected List<SkillDataBase> skillDatas = new List<SkillDataBase>();




    [Header("Test")]
    public List<AdvantageData> datas = new List<AdvantageData>();


    //To Do 뒤끝서버 이용해서 제이슨 데이터가져와서 세팅하기    version 에 따라서 데이터  받아오는거 처리 


    public void ClearData()
    {
        towerDatas.Clear();
        enemyDatas.Clear();
        heroDatas.Clear();
        advantageDatas.Clear();
        skillDatas.Clear();
    }


    public List<AdvantageData> GetRandomAdvantageData(int count)
    {
  
        List<int> randomIndexs = new List<int>();

        for(int i = 0; i < count; i++)
        {
            GetRandomIndex(randomIndexs);
        }

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
    

    public Skill GetSkill(string _uniqueKey)
    {
        SkillDataBase _skillData = skillDatas.Find((x)=> x.skillUniqueKey.Equals(_uniqueKey) == true);
        Skill getSkill = new Skill(_skillData);
        return getSkill;
    }

    public EnemyData GetEnemyData(string _uniqueKey)
    {
        var data = enemyDatas.Find((x) => x.uniqueKey.Equals(_uniqueKey));
        if (data == null)
            return null;
        else
            return data;
    }

    public HeroData GetHeroData(string _uniqueKey)
    {
        var data = heroDatas.Find((x) => x.uniqueKey.Equals(_uniqueKey));
        if (data == null)
            return null;
        else
            return data;
    }

    public TowerData GetTowerData(string _uniqueKey)
    {
        var data = towerDatas.Find((x) => x.uniqueKey.Equals(_uniqueKey));
        if (data == null)
            return null;
        else
            return data;
    }

    public SkillDataBase GetSkillData(string _uniqueKey)
    {
        var data = skillDatas.Find((x) => x.skillUniqueKey.Equals(_uniqueKey));
        if (data == null)
            return null;
        else
            return data;
    }

    public AdvantageData GetAdvantageData(string _uniqueKey)
    {
        var data = advantageDatas.Find((x) => x.uniqueKey.Equals(_uniqueKey));
        if (data == null)
            return null;
        else
            return data;
    }
    public void SetData(EnemyData _data)
    {
        enemyDatas.Add(_data);
    }

    public void SetData(HeroData _data)
    {
        heroDatas.Add(_data);
    }

    public void SetData(TowerData _data)
    {
        towerDatas.Add(_data);
    }
    public void SetData(SkillDataBase _data)
    {
        skillDatas.Add(_data);
    }
    public void SetData(AdvantageData _data)
    {
        advantageDatas.Add(_data);
    }





}
