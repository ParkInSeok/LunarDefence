using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitAnimateState { 
    None,
    Spawn,
    Move,
    Stun,
    Attack,
    Die

}
public enum UnitState
{
    alive,
    die,
}



public abstract class UnitBase : MonoBehaviour
{
    [Header("UnitBase")]
    [SerializeField] protected Transform modelParent;
    protected GameObject model;
    protected AnimatorController animatorContoller;
    [SerializeField] protected UnitAnimateState animateState;
    [SerializeField] protected UnitState unitState;
    public Action setModelCompletedEventHandler;



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

        animatorContoller = model.GetComponent<AnimatorController>();

        setModelCompletedEventHandler?.Invoke();
    }



    protected virtual void BindPlayDieAnimationEvent()
    {
        ChangeAnimateState(UnitAnimateState.Die);
    }

    protected virtual void BindEvents()
    {
        ChangeAnimateState(UnitAnimateState.Spawn);

    }

    protected virtual void DieEvent()
    {
        unitState = UnitState.die;

        //object pool add

    }
    public abstract void ChangeAnimateState(UnitAnimateState _state, float animSpeed = 1);
    public abstract void GetDamage(int _damage, DamageType _damageType);

}