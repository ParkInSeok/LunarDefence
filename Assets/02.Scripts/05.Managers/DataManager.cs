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

    Dictionary<string, GameObject> loadAssetPrefabs = new Dictionary<string, GameObject>();
    Dictionary<string, Sprite> loadAssetSprite = new Dictionary<string, Sprite>();


    List<AsyncOperationHandle> loadAssetList = new List<AsyncOperationHandle>();

    GameData data;

    public GameData GameData { get { return data; } }

    
    [SerializeField] List<EnemyData> enemyDatas = new List<EnemyData>();
    [SerializeField] List<TowerData> towerDatas = new List<TowerData>();
    [SerializeField] List<HeroData> heroDatas = new List<HeroData>();


  

    // Start is called before the first frame update
    void Start()
    {
     

    }

  

    // Update is called once per frame
    void Update()
    {

    }
    protected override void Init()
    {

        SetEnemyData();
        SetHeroData();
        SetTowerData();

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
            var foundData = enemyDatas.Find((x) => x.uniqueKey.Equals(_uniqueKey));

            if (foundData != null)
                continue;

            int _propertyState = enemyData[index].Get<int>("propertyState");
            int _damageType = enemyData[index].Get<int>("damageType");
            int _unitType = enemyData[index].Get<int>("unitType");
            EnemyData data = new EnemyData(_propertyState, _damageType, _unitType);
            data.uniqueKey = _uniqueKey;
            data.modelUniqueKey = enemyData[index].Get<string>("modelUniqueKey");
            data.atk = enemyData[index].Get<float>("atk");
            data.hp = enemyData[index].Get<int>("hp");
            data.def = enemyData[index].Get<float>("def");
            data.spdef = enemyData[index].Get<float>("spdef");
            data.attackSpeed = enemyData[index].Get<float>("attackSpeed");
            data.propertyReinforcePower = enemyData[index].Get<float>("propertyReinforcePower");
            data.propertyResistPower = enemyData[index].Get<float>("propertyResistPower");
            data.moveSpeed = enemyData[index].Get<float>("moveSpeed");

            enemyDatas.Add(data);


        }
    }

    void SetHeroData()
    {
        var towerData = BGRepo.I["HeroData"];

        if (towerData == null)
            Debug.Log("meta null");

        for (int i = 0; i < towerData.CountEntities; i++)
        {
            int index = i;

            string _uniqueKey = towerData[index].Get<string>("uniqueKey");
            var foundData = heroDatas.Find((x) => x.uniqueKey.Equals(_uniqueKey));

            if (foundData != null)
                continue;

            int _propertyState = towerData[index].Get<int>("propertyState");
            int _damageType = towerData[index].Get<int>("damageType");
            int _unitType = towerData[index].Get<int>("unitType");
            HeroData data = new HeroData(_propertyState, _damageType, _unitType);
            data.uniqueKey = _uniqueKey;
            data.modelUniqueKey = towerData[index].Get<string>("modelUniqueKey");
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
            data.skillUniqueKeys = towerData[index].Get<string>("skillUniqueKeys");

            heroDatas.Add(data);


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
            var foundData = towerDatas.Find((x) => x.uniqueKey.Equals(_uniqueKey));

            if (foundData != null)
                continue;

            int _propertyState = towerData[index].Get<int>("propertyState");
            int _damageType = towerData[index].Get<int>("damageType");
            int _unitType = towerData[index].Get<int>("unitType");
            TowerData data = new TowerData(_propertyState, _damageType, _unitType);
            data.uniqueKey = _uniqueKey;
            data.modelUniqueKey = towerData[index].Get<string>("modelUniqueKey");
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

            towerDatas.Add(data);


        }
    }

    #endregion



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

        if (loadAssetPrefabs.ContainsKey(key))
        {
            var loadObject = Instantiate(loadAssetPrefabs[key], Vector3.zero, Quaternion.identity);
            endCallBack?.Invoke(loadObject);
        }
        else
        {
            Addressables.LoadAssetAsync<GameObject>(key).Completed += (AsyncOperationHandle<GameObject> obj) =>
            {
                if (loadAssetList.Contains(obj) == false)
                    loadAssetList.Add(obj);

                loadAssetPrefabs.Add(key, obj.Result);

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
            Addressables.Release(loadAssetList[i]);
        }

        loadAssetList.Clear();
        loadAssetPrefabs.Clear();
    }
 

    IEnumerator GetFileEvent(string key, Action<Sprite> endCallBack)
    {
        if (loadAssetSprite.ContainsKey(key))
        {
            var loadSprite = loadAssetSprite[key];
            endCallBack?.Invoke(loadSprite);
        }
        else
        {
            Addressables.LoadAssetAsync<Texture2D>(key).Completed += (AsyncOperationHandle<Texture2D> obj) =>
            {
                if (loadAssetList.Contains(obj) == false)
                    loadAssetList.Add(obj);


                var sprite = Sprite.Create(obj.Result, new Rect(0, 0, obj.Result.width, obj.Result.height), Vector2.one * 0.5f);
                loadAssetSprite.Add(key, sprite);
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



}
