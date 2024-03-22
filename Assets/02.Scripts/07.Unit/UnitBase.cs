using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [Header("UnitBase")]
    [SerializeField] protected Transform modelParent;
    protected GameObject model;
    protected Animator animator;


    protected virtual void CreateModel(string modelUniqueKey)
    {
        DataManager.Instance.GetGameObject(modelUniqueKey, SetModel);
    }

    protected virtual void SetModel(GameObject loadedObject)
    {
        model = loadedObject;

        model.transform.SetParent(modelParent);
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;

        animator = model.GetComponent<Animator>();
    }


}
