using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : UnitBase
{

    [SerializeField] HeroStat stat;

    public HeroStat Stat { get { return stat; } }


    public void Init(HeroData _stat)
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
        ChangeAnimateState(UnitAnimateState.Idle);
    }

    public override void GetDamage(int _damage, DamageType _damageType)
    {
        if (unitState == UnitState.die)
            return;

        stat.GetDamage(_damage, _damageType);

    }
}
