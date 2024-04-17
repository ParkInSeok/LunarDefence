using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
{
    [Header("Enemy")]
    [SerializeField] EnemyStat stat;

    public EnemyStat Stat { get { return stat; } }


    public Action<Enemy> dieEventHandler;



    public void Init(EnemyData _stat)
    {
        stat = new EnemyStat();
        stat.InitStat(_stat);
        unitState = UnitState.alive;
        animateState = UnitAnimateState.None;
        setModelCompletedEventHandler += BindEvents;

        CreateModel(stat.CurrentEnemyStat.uniqueKey);

      
    }
    public void RecycleInit(EnemyData _stat)
    {
        this.gameObject.SetActive(true);
        stat.InitStat(_stat);
        unitState = UnitState.alive;
        animateState = UnitAnimateState.None;
        setModelCompletedEventHandler = null;
        setModelCompletedEventHandler += BindEvents;

        CreateModel(stat.CurrentEnemyStat.uniqueKey);

    }



    protected override void BindEvents()
    {
        Debug.Log("Enemy BindEvents");
        stat.dieEventHandler += BindPlayDieAnimationEvent;
        animatorContoller.dieEventHandler += DieEvent;
        animatorContoller.spawnedEventHandler += BindSpawnedEvent;
        RecycleBindEvents();

    }

    protected override void BindSpawnedEvent()
    {
        ChangeAnimateState(UnitAnimateState.Move);

        //Move(herotransform);
    }

    protected override void DieEvent()
    {
        Debug.Log("enemy component DieEvent");
        base.DieEvent();
        //object pool add
        dieEventHandler?.Invoke(this);
        this.gameObject.SetActive(false);
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

    public override void ResetModel()
    {
        DataManager.Instance.ResetMemory(stat.CurrentEnemyStat.uniqueKey);


        if (model != null)
            Destroy(model);

    }



}
