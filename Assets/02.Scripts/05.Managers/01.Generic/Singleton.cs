using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    [SerializeField]
    protected bool dontDestroy = true;
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = InitSingleton<T>();
            }
            return instance;
        }
    }

    protected static U InitSingleton<U>() where U : MonoBehaviour
    {
        GameObject go = null;
        U obj = FindObjectOfType<U>();
        if (obj == null)
        {
            go = new GameObject(typeof(U).Name);
            go.AddComponent<U>();
        }
        else
        {
            go = obj.gameObject;
        }

        DontDestroyOnLoad(go);
        return go.GetComponent<U>();
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            if (dontDestroy)
            {
                Instance.Init();
            }
            else
            {
                instance = GetComponent<T>();
                Init();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected abstract void Init();
}
