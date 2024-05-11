using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using BansheeGz.BGDatabase;

public class DataManager : Singleton<DataManager>
{

    [SerializeField] List<LoadClass> loadAssetList = new List<LoadClass>();

    //List<AsyncOperationHandle> loadAssetList = new List<AsyncOperationHandle>();

    [SerializeField] GameData gameData;

    public GameData GameData { get { return gameData; } }

    [SerializeField] string selectedHeroUniqueKey;
    [SerializeField] List<string> selectedTowerUniqueKeys = new List<string>();

    public string SelectedHero { get { return selectedHeroUniqueKey; } }
    public List<string> SelectedTowers { get { return selectedTowerUniqueKeys; } }

  
    void Start()
    {
     

    }

  

    // Update is called once per frame
    void Update()
    {

    }
    protected override void Init()
    {
        gameData.ClearData();

        SetEnemyData();
        SetHeroData();
        SetTowerData();
        SetSkillData();
        SetAdvantageData();



    }


    #region SetDatas


    private void SetEnemyData()
    {
        var enemyData = BGRepo.I["EnemyData"];

        if (enemyData == null)
            Debug.Log("meta null");

        for (int i = 0; i < enemyData.CountEntities; i++)
        {
            int index = i;

            string _uniqueKey = enemyData[index].Get<string>("uniqueKey");
            var foundData = gameData.GetEnemyData(_uniqueKey);

            if (foundData != null)
                continue;

            int _propertyState = enemyData[index].Get<int>("propertyState");
            int _damageType = enemyData[index].Get<int>("damageType");
            var _unitType = (int)UnitType.enemyData;
            int _enemyType = enemyData[index].Get<int>("enemyType");

            EnemyData data = new EnemyData(_propertyState, _damageType, _unitType, _enemyType);
            data.uniqueKey = _uniqueKey;
            data.atk = enemyData[index].Get<float>("atk");
            data.hp = enemyData[index].Get<int>("hp");
            data.def = enemyData[index].Get<float>("def");
            data.spdef = enemyData[index].Get<float>("spdef");
            data.attackSpeed = enemyData[index].Get<float>("attackSpeed");
            data.propertyReinforcePower = enemyData[index].Get<float>("propertyReinforcePower");
            data.propertyResistPower = enemyData[index].Get<float>("propertyResistPower");
            data.moveSpeed = enemyData[index].Get<float>("moveSpeed");
            data.attackMotionLength = enemyData[index].Get<int>("attackMotionLength");
            gameData.SetData(data);


        }
    }

    void SetHeroData()
    {
        var heroData = BGRepo.I["HeroData"];

        if (heroData == null)
            Debug.Log("meta null");

        for (int i = 0; i < heroData.CountEntities; i++)
        {
            int index = i;

            string _uniqueKey = heroData[index].Get<string>("uniqueKey");
            var foundData = gameData.GetHeroData(_uniqueKey);

            if (foundData != null)
                continue;

            int _propertyState = heroData[index].Get<int>("propertyState");
            int _damageType = heroData[index].Get<int>("damageType");
            var _unitType = (int)UnitType.heroData;

            HeroData data = new HeroData(_propertyState, _damageType, _unitType);
            data.uniqueKey = _uniqueKey;
            data.atk = heroData[index].Get<float>("atk");
            data.hp = heroData[index].Get<int>("hp");
            data.def = heroData[index].Get<float>("def");
            data.spdef = heroData[index].Get<float>("spdef");
            data.attackSpeed = heroData[index].Get<float>("attackSpeed");
            data.propertyReinforcePower = heroData[index].Get<float>("propertyReinforcePower");
            data.propertyResistPower = heroData[index].Get<float>("propertyResistPower");
            data.critical = heroData[index].Get<int>("critical");
            data.criticalDamage = heroData[index].Get<int>("criticalDamage");
            data.lifeBloodAbsorption = heroData[index].Get<int>("lifeBloodAbsorption");
            data.skillUniqueKeys = heroData[index].Get<string>("skillUniqueKeys");
            data.attackMotionLength = heroData[index].Get<int>("attackMotionLength");

            gameData.SetData(data);


        }
    }


    void SetTowerData()
    {
        var towerData = BGRepo.I["TowerData"];

        if (towerData == null)
            Debug.Log("meta null");

        for (int i = 0; i < towerData.CountEntities; i++)
        {
            int index = i;

            string _uniqueKey = towerData[index].Get<string>("uniqueKey");
            var foundData = gameData.GetTowerData(_uniqueKey);

            if (foundData != null)
                continue;

            int _propertyState = towerData[index].Get<int>("propertyState");
            int _damageType = towerData[index].Get<int>("damageType");
            var _unitType = (int)UnitType.towerData;

            TowerData data = new TowerData(_propertyState, _damageType, _unitType);
            data.uniqueKey = _uniqueKey;
            data.atk = towerData[index].Get<float>("atk");
            data.hp = towerData[index].Get<int>("hp");
            data.def = towerData[index].Get<float>("def");
            data.spdef = towerData[index].Get<float>("spdef");
            data.attackSpeed = towerData[index].Get<float>("attackSpeed");
            data.propertyReinforcePower = towerData[index].Get<float>("propertyReinforcePower");
            data.propertyResistPower = towerData[index].Get<float>("propertyResistPower");
            data.critical = towerData[index].Get<int>("critical");
            data.criticalDamage = towerData[index].Get<int>("criticalDamage");
            data.lifeBloodAbsorption = towerData[index].Get<int>("lifeBloodAbsorption");
            data.skillUniqueKey = towerData[index].Get<string>("skillUniqueKey");
            data.attackMotionLength = towerData[index].Get<int>("attackMotionLength");

            gameData.SetData(data);


        }
    }

    void SetSkillData()
    {
        var dbData = BGRepo.I["SkillData"];

        if (dbData == null)
            Debug.Log("meta null");

        for (int i = 0; i < dbData.CountEntities; i++)
        {
            int index = i;

            string _uniqueKey = dbData[index].Get<string>("skillUniqueKey");
            var foundData = gameData.GetSkillData(_uniqueKey);

            if (foundData != null)
                continue;

            var skillType = dbData[index].Get<int>("skillType");
            var buffStatType = dbData[index].Get<int>("buffStatType");
            var activationType = dbData[index].Get<int>("skillActivationType");
            SkillDataBase data = new SkillDataBase(skillType, buffStatType, activationType);
            data.skillUniqueKey = _uniqueKey;
            data.skillIconUniqueKey = dbData[index].Get<string>("skillIconUniqueKey");
            data.damageCoefficient = dbData[index].Get<int>("damageCoefficient");
            data.activatePercent = dbData[index].Get<int>("activatePercent");

            gameData.SetData(data);


        }



    }

    void SetAdvantageData()
    {
        var dbData = BGRepo.I["AdvantageData"];

        if (dbData == null)
            Debug.Log("meta null");

        for (int i = 0; i < dbData.CountEntities; i++)
        {
            int index = i;

            string _uniqueKey = dbData[index].Get<string>("uniqueKey");
            var foundData = gameData.GetAdvantageData(_uniqueKey);

            if (foundData != null)
                continue;
         
            AdvantageData data = new AdvantageData();
            data.uniqueKey = _uniqueKey;
            data.iconUniqueKey = dbData[index].Get<string>("iconUniqueKey");
            data.title = dbData[index].Get<string>("title");
            data.info = dbData[index].Get<string>("info");
            data.level = dbData[index].Get<int>("level");

            gameData.SetData(data);

        }



    }


    #endregion

    #region Create Addressable Function

    public void GetSprite(string key, Action<Sprite> endCallBack)
    {
        StartCoroutine(GetFileEvent(key, endCallBack));
    }


    public void GetGameObject(string key, Action<GameObject> endCallBack)
    {
        StartCoroutine(GetFileEvent(key, endCallBack));
    }

    IEnumerator GetFileEvent(string key, Action<GameObject> endCallBack)
    {
        // TODO 
        // 처음 for문으로 만들떄 여러개가 추가됨 확인해서 처리할것 
        //Debug.Log("GetFileEvent loadAssetList count " + loadAssetList.Count);
        var existLoadedAsset = loadAssetList.Find((x) => x.IsExist(key) == true);

        if(existLoadedAsset != null)
        {
           // Debug.Log("existLoadedAsset != null");
            var loadObject = Instantiate(existLoadedAsset.GetGameObject, Vector3.zero, Quaternion.identity);
            endCallBack?.Invoke(loadObject);
        }
        else
        {
            //Debug.Log("existLoadedAsset == null");
            Addressables.LoadAssetAsync<GameObject>(key).Completed += (AsyncOperationHandle<GameObject> obj) =>
            {
                var newData = new LoadClass(key, obj, obj.Result);
                //Debug.Log("add loadclass data : " + key);
                loadAssetList.Add(newData);

                var loadObject = Instantiate(obj.Result, Vector3.zero, Quaternion.identity);

                endCallBack?.Invoke(loadObject);

            };

        }

        


         yield return null;

    }

    public void ResetMemory()
    {
        for (int i = loadAssetList.Count - 1; i >= 0; i--)
        {
            Addressables.Release(loadAssetList[i].LoadedHandle.Result);
        }

        loadAssetList.Clear();
    }
 
    public void ResetMemory(string key)
    {
        var findedAsset = loadAssetList.Find((x) => x.Key.Equals(key));

        if (findedAsset == null)
            return;

        Addressables.Release(findedAsset.GetGameObject);

        if (loadAssetList.Contains(findedAsset))
            loadAssetList.Remove(findedAsset);
    }

    IEnumerator GetFileEvent(string key, Action<Sprite> endCallBack)
    {

        var existLoadedAsset = loadAssetList.Find((x) =>
            x.Type == LoadClassType.Sprite && x.IsExist(key) == true
      );

        if (existLoadedAsset != null)
        {
            var loadSprite = existLoadedAsset.GetSprite;
            endCallBack?.Invoke(loadSprite);
        }
        else
        {
            Addressables.LoadAssetAsync<Texture2D>(key).Completed += (AsyncOperationHandle<Texture2D> obj) =>
            {
                var sprite = Sprite.Create(obj.Result, new Rect(0, 0, obj.Result.width, obj.Result.height), Vector2.one * 0.5f);
                var newData = new LoadClass(key, obj, sprite);
                loadAssetList.Add(newData);

                sprite.name = key;

                endCallBack?.Invoke(sprite);

            };
        }


        yield return null;

        #region GetLocal Sprite using www
        /*
        string url = "file://" + Application.dataPath + "/Prefabs/Images/" + key;

        WWW www = new WWW(url);

        yield return www;

        if (string.IsNullOrEmpty(www.error) == false)
        {
            Debug.LogError("file file error " + www.error);
        }
        else
        {
            var sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.one * 0.5f);
            sprite.name = key;
            endCallBack?.Invoke(sprite);
        }

        yield return null;
        */
        #endregion

    }

    #endregion

    #region Set Hero Tower Keys

    public void SetHeroKey(string key)
    {
        selectedHeroUniqueKey = key;
    }

    public void SetTowerKeys(List<string> keys)
    {
        selectedTowerUniqueKeys = keys;
    }

    public void ClearKeys()
    {
        selectedHeroUniqueKey = "";
        selectedTowerUniqueKeys.Clear();
    }


    #endregion

}
