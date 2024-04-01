using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
{
    [Header("Enemy")]
    [SerializeField] EnemyStat stat;

    public EnemyStat Stat { get { return stat; } }





    public void Init(EnemyData _stat)
    {
        stat.InitStat(_stat);

        CreateModel(stat.CurrentEnemyStat.modelUniqueKey);

        unitState = UnitState.alive;

        setModelCompletedEventHandler += BindEvents;

    }

    protected override void BindEvents()
    {
        stat.dieEventHandler += BindPlayDieAnimationEvent;
        animatorContoller.dieEventHandler += DieEvent;
        animatorContoller.spawnedEventHandler += BindSpawnedEvent;
        //delay function 3f => spawn time
        animatorContoller.noExistSpawnAnimEventHandler += () => { UtilityManager.Instance.DelayFunction(BindSpawnedEvent, 3f); };
        ChangeAnimateState(UnitAnimateState.Spawn);

    }

    private void BindSpawnedEvent()
    {
        ChangeAnimateState(UnitAnimateState.Move);

        //Move(herotransform);
    }

    protected override void DieEvent()
    {
        base.DieEvent();
        //object pool add

    }

  

    protected override void CreateModel(string modelUniqueKey)
    {
        base.CreateModel(modelUniqueKey);
    }

    protected override void SetModel(GameObject loadedObject)
    {
        base.SetModel(loadedObject);

    }

    public void Move(Transform targetHero)
    {

    }


    public override void ChangeAnimateState(UnitAnimateState _state, float animSpeed = 1)
    {

        if (animateState == UnitAnimateState.Die)
            return;

        if (animateState == _state)
            return;

        animateState = _state;

        animatorContoller.ChangeAnimateState(_state, animSpeed);

    }

    public override void GetDamage(int _damage, DamageType _damageType)
    {
        if (unitState == UnitState.die)
            return;


        stat.GetDamage(_damage, _damageType);

    }

 



}
