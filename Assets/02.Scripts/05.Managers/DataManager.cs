using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;



public class DataManager : Singleton<DataManager>
{






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
        //string url = "Assets/Prefabs/GameObjects/" + key;
        //string url = "Assets/Prefabs/GameObjects";
        string url =  Application.dataPath + "/Prefabs/GameObjects/";

        // var prefab = AssetDatabase.LoadAssetAtPath(url, typeof(GameObject));

        // yield return new WaitUntil(() => AssetDatabase.IsMainAssetAtPathLoaded(url) == true);
        
    //    AssetBundleCreateRequest createRequest = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(url + key));
    //     yield return createRequest;
    //     AssetBundle bundle = createRequest.assetBundle;
    //     var prefab = bundle.LoadAsset<GameObject>("Cube");
        //Instantiate(prefab);

        // string uri = "file:///" + Application.dataPath +"/Prefabs/GameObjects/" + key;        
        // UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri, 0);
        // yield return request.Send();
        // AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        // GameObject cube = bundle.LoadAsset<GameObject>("Cube");
        // GameObject sprite = bundle.LoadAsset<GameObject>("Sprite");
        // Instantiate(cube);
        // Instantiate(sprite);

        // GameObject loadObject = Instantiate(cube, Vector3.zero, Quaternion.identity) as GameObject;

        // string uri = "file:///" + Application.dataPath +"/Prefabs/GameObjects/" + key;        

        // WWW bundleRequest = new WWW(uri);
        // while (!bundleRequest.isDone)
        // {
        //     Debug.Log("downloading....");
        //     //onDLProgress?.Invoke(modelName, bundleRequest.progress);
        //     yield return null;
        // }

        // AssetBundle bundle = null;
        // GameObject loadObject = null;
        // if (bundleRequest.bytesDownloaded > 0)
        // {
        //     AssetBundleCreateRequest myRequest = AssetBundle.LoadFromMemoryAsync(bundleRequest.bytes);
        //     while(!myRequest.isDone)
        //     {
        //         Debug.Log("loading....");
        //         yield return null;
        //     }
        //     if(myRequest.assetBundle != null)
        //     {
        //         bundle = myRequest.assetBundle;
        //         GameObject model = null;
        //         if (bundle != null)
        //         {
        //             AssetBundleRequest newRequest = bundle.LoadAssetAsync<GameObject>("Cube");
        //             while (!newRequest.isDone)
        //             {
        //                 Debug.Log("loading ASSET....");
        //                 yield return null;
        //             }
        //             model = (GameObject)newRequest.asset;
        //             loadObject = Instantiate(model, Vector3.zero, Quaternion.identity);

                    
        //         }
        //     }
        //     else
        //     {
        //         Debug.LogError("COULDN'T DOWNLOAD ASSET BUNDLE FROM URL: " + uri);
        //     }
        // }
        // else
        // {
        //     Debug.LogError("COULDN'T DOWNLOAD ASSET BUNDLE FROM URL: " + uri);
        // }



        //bundle.Unload(false);

      


        //GameObject loadObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
       // endCallBack?.Invoke(loadObject);

        yield return null;

    }

    IEnumerator GetFileEvent(string key, Action<Sprite> endCallBack)
    {
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

    }




}
