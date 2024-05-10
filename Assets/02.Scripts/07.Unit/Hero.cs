using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : UnitBase
{

    [SerializeField] HeroStat stat;

    public HeroStat Stat { get { return stat; } }

    public Action<Hero> dieEventHandler;

    #region Init
    public void Init(HeroData _stat)
    {
        stat.InitStat(_stat);

        setModelCompletedEventHandler += BindEvents;

        CreateModel(stat.CurrentHeroStat.uniqueKey);

        unitState = UnitState.alive;
    }

    public void RecycleInit(HeroData _stat)
    {
        this.gameObject.SetActive(true);
        stat.InitStat(_stat);
        unitState = UnitState.alive;
        animateState = UnitAnimateState.None;
        setModelCompletedEventHandler = null;
        setModelCompletedEventHandler += BindEvents;

        if (animatorControllerPool.ContainsKey(stat.CurrentHeroStat.uniqueKey))
        {
            Debug.Log("animatorControllerPool ContainsKey ");
            SetModel(animatorControllerPool[stat.CurrentHeroStat.uniqueKey].gameObject);
        }
        else
        {
            Debug.Log("animatorControllerPool not ContainsKey ");
            CreateModel(stat.CurrentHeroStat.uniqueKey);
        }

    }

    #endregion

    protected override void BindEvents()
    {
        stat.dieEventHandler += BindPlayDieAnimationEvent;
        animatorContoller.dieEventHandler += DieEvent;
        animatorContoller.spawnedEventHandler += BindSpawnedEvent;
        //delay function 3f => spawn time
        animatorContoller.attackEventHandler += BindAttackEvent;

        if (animatorControllerPool.ContainsKey(stat.CurrentHeroStat.uniqueKey) == false)
            animatorControllerPool.Add(stat.CurrentHeroStat.uniqueKey, animatorContoller);

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
        if (model != null)
        {
            model.SetActive(false);
            //Destroy(model);
        }

    }

    protected override void DieEvent()
    {
        Debug.Log("hero DieEvent");
        base.DieEvent();
        //object pool add
        dieEventHandler?.Invoke(this);
        this.gameObject.SetActive(false);
    }


    protected override float SetDamage()
    {

        bool isCritical = false;
        var randomIndex = UnityEngine.Random.Range(0, 100);
        var _damage = stat.CurrentHeroStat.atk;
        if (randomIndex <= stat.CurrentHeroStat.critical)
        {
            isCritical = true;
        }

        if (isCritical)
        {
            _damage = stat.CurrentHeroStat.atk * (200f + (float)stat.CurrentHeroStat.criticalDamage) / 100f;
            //加碍 单固瘤 贸府
        }
        else
        {
            _damage = stat.CurrentHeroStat.atk;
            //加碍 单固瘤 贸府
        }


        return _damage;
    }


}
