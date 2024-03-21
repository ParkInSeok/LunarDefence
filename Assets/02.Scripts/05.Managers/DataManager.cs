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


public class DataManager : Singleton<DataManager>
{


    List<AsyncOperationHandle> loadAssetList = new List<AsyncOperationHandle>();

    GameData data;

    public GameData GameData { get { return data; } }


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



    }
    //to do 바꿔야함 로직
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
        //에셋번들사용하지않으면 불가능
       // string url = "file://" + Application.dataPath + "/Prefabs/GameObjects/" + key;
       
        Addressables.LoadAssetAsync<GameObject>(key).Completed += (AsyncOperationHandle<GameObject> obj)=>{
            if(loadAssetList.Contains(obj) == false)
                loadAssetList.Add(obj);

            var loadObject = Instantiate(obj.Result, Vector3.zero, Quaternion.identity);

            endCallBack?.Invoke(loadObject);

            //Addressables.Release
        };


        // GameObject loadObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        // endCallBack?.Invoke(loadObject);

         yield return null;

    }

    public void ResetMemory()
    {
        for (int i = loadAssetList.Count - 1; i >= 0; i--)
        {
            Addressables.Release(loadAssetList[i]);
        }

        loadAssetList.Clear();
    }
 

    IEnumerator GetFileEvent(string key, Action<Sprite> endCallBack)
    {

        Addressables.LoadAssetAsync<Texture2D>(key).Completed += (AsyncOperationHandle<Texture2D> obj) => {
            if (loadAssetList.Contains(obj) == false)
                loadAssetList.Add(obj);
            var sprite = Sprite.Create(obj.Result, new Rect(0, 0, obj.Result.width, obj.Result.height), Vector2.one * 0.5f);
            sprite.name = key;

            endCallBack?.Invoke(sprite);

        };

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

}
