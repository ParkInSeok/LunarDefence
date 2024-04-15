using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : UnitBase
{
    [SerializeField] TowerStat stat;

    public TowerStat Stat { get { return stat; } }



    public void Init(TowerData _stat)
    {
        stat.InitStat(_stat);

        setModelCompletedEventHandler += BindEvents;

        CreateModel(stat.CurrentTowerStat.modelUniqueKey);

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


}
