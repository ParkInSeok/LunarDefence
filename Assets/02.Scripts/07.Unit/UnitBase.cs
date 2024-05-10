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
    Die,
    Idle,
    Skill_First, // need skill motion
    Skill_Second,
    Skill_Third,
    Skill_Fourth,


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

    public UnitAnimateState AnimateState { get { return animateState; } }
    public UnitState UnitState { get { return unitState; } }

    protected Dictionary<string, AnimatorController> animatorControllerPool = new Dictionary<string, AnimatorController>();



    public Action setModelCompletedEventHandler;
    public Action unitDieEventHandler;


    protected virtual void CreateModel(string modelUniqueKey)
    {
        DataManager.Instance.GetGameObject(modelUniqueKey, SetModel);
    }

    protected virtual void SetModel(GameObject loadedObject)
    {
        model = loadedObject;
        model.SetActive(true);
        model.transform.SetParent(modelParent);
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;

        animatorContoller = model.GetComponent<AnimatorController>();

        animatorContoller.Init();

        setModelCompletedEventHandler?.Invoke();

    
    }

    protected virtual void SetModel(AnimatorController controller)
    {
        model = controller.gameObject;
        model.SetActive(true);
        model.transform.SetParent(modelParent);
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;

        animatorContoller = controller;
        animatorContoller.Init();

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
        unitDieEventHandler?.Invoke();

    }

    protected virtual void BindSpawnedEvent()
    {
        ChangeAnimateState(UnitAnimateState.Move);
    }

    protected virtual void RecycleBindEvents()
    {
        //delay function 3f => spawn time
        ChangeAnimateState(UnitAnimateState.Spawn);
    }


    public abstract void ResetModel();

    public abstract void ChangeAnimateState(UnitAnimateState _state, float animSpeed = 1);
    public abstract void GetDamage(int _damage, DamageType _damageType);
    protected abstract float SetDamage();

    

}