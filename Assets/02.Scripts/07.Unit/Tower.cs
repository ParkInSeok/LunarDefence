using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : UnitBase
{
    [SerializeField] TowerStat stat;

    public TowerStat Stat { get { return stat; } }

    public Action<Tower> dieEventHandler;

    #region Init

    public void Init(TowerData _stat)
    {
        stat.InitStat(_stat);
        unitState = UnitState.die;
        animateState = UnitAnimateState.Die;

        setModelCompletedEventHandler += BindEvents;

        CreateModel(stat.CurrentTowerStat.uniqueKey);

        DieEvent();
    }
    public void RecycleInit(TowerData _stat)
    {
        this.gameObject.SetActive(true);
        stat.InitStat(_stat);
        unitState = UnitState.alive;
        animateState = UnitAnimateState.None;
        setModelCompletedEventHandler = null;
        setModelCompletedEventHandler += BindEvents;

        CreateModel(stat.CurrentTowerStat.uniqueKey);

    }


    #endregion

    protected override void BindEvents()
    {
        stat.dieEventHandler += BindPlayDieAnimationEvent;
        animatorContoller.dieEventHandler += DieEvent;
        animatorContoller.spawnedEventHandler += BindSpawnedEvent;
        animatorContoller.noExistSpawnAnimEventHandler += BindSpawnedEvent;
        animatorContoller.attackEventHandler += BindAttackEvent;
        ChangeAnimateState(UnitAnimateState.Spawn);

    }

    private void BindAttackEvent()
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

    protected override void BindSpawnedEvent()
    {
        ChangeAnimateState(UnitAnimateState.Idle);



    }


    public override void ResetModel()
    {
        DataManager.Instance.ResetMemory(stat.CurrentTowerStat.uniqueKey);
        if (model != null)
            Destroy(model);

    }

    protected override void DieEvent()
    {
        //Debug.Log("tower DieEvent");
        base.DieEvent();
        //object pool add
        dieEventHandler?.Invoke(this);
        this.gameObject.SetActive(false);

        //현재 위치해있는 tileeventTrigger.DieUnit 

    }

    protected override float SetDamage()
    {
        bool isCritical = false;
        var randomIndex = UnityEngine.Random.Range(0, 100);
        var _damage = stat.CurrentTowerStat.atk;
        if (randomIndex <= stat.CurrentTowerStat.critical)
        {
            isCritical = true;
        }

        if (isCritical)
        {
            _damage = stat.CurrentTowerStat.atk * (200f + (float)stat.CurrentTowerStat.criticalDamage) / 100f;
            //속강 데미지 처리
        }
        else
        {
            _damage = stat.CurrentTowerStat.atk;
            //속강 데미지 처리
        }


        return _damage;
    }

}
