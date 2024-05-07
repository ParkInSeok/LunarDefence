using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public enum LoadClassType
{
    GameObject,
    Sprite,


}

[System.Serializable]
public class LoadClass
{
    [SerializeField] string key;
    [SerializeField] LoadClassType type;
    AsyncOperationHandle loadedHandle;
    [SerializeField] GameObject gameObject;
    [SerializeField] Sprite sprite;

    public string Key { get { return key; } }

    public LoadClassType Type { get { return type; } }

    public AsyncOperationHandle LoadedHandle { get { return loadedHandle; } }

    public LoadClass(string _key, AsyncOperationHandle _loadedHandle, GameObject _gameObject)
    {
        key = _key;
        type = LoadClassType.GameObject;
        loadedHandle = _loadedHandle;
        gameObject = _gameObject;
    }


    public LoadClass(string _key, AsyncOperationHandle _loadedHandle, Sprite _sprite)
    {
        key = _key;
        type = LoadClassType.Sprite;
        loadedHandle = _loadedHandle;
        sprite = _sprite;
    }



    public bool IsExist(string _key)
    {
        if (key.Equals(_key))
        {
            return true;
        }

        return false;
    }

    public GameObject GetGameObject
    {
        get { return gameObject; }
    }

    public Sprite GetSprite
    {
        get { return sprite; }
    }



}
