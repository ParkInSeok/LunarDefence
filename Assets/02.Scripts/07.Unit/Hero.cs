using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : UnitBase
{

    [SerializeField] HeroStat stat;

    public HeroStat Stat { get { return stat; } }

    public Action<Hero> dieEventHandler;


    public void Init(HeroData _stat)
    {
        stat.InitStat(_stat);

        setModelCompletedEventHandler += BindEvents;

        CreateModel(stat.CurrentHeroStat.uniqueKey);

        unitState = UnitState.alive;
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

    protected override void BindSpawnedEvent()
    {
        ChangeAnimateState(UnitAnimateState.Idle);
    }

    public override void ResetModel()
    {
        DataManager.Instance.ResetMemory(stat.CurrentHeroStat.uniqueKey);
        if (model != null)
            Destroy(model);

    }

    protected override void DieEvent()
    {
        Debug.Log("hero DieEvent");
        base.DieEvent();
        //object pool add
        dieEventHandler?.Invoke(this);
        this.gameObject.SetActive(false);
    }



}
