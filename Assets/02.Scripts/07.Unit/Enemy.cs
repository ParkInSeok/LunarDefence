using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
{
    [Header("Enemy")]
    [SerializeField] EnemyStat stat;

    public EnemyStat Stat { get { return stat; } }

    [Header("Debugging")]
    public UnitBase target;


    public Action<Enemy> dieEventHandler;

    #region Init

    public void Init(EnemyData _stat)
    {
        stat = new EnemyStat();
        stat.InitStat(_stat);
        unitState = UnitState.die;
        animateState = UnitAnimateState.Die;
        setModelCompletedEventHandler += BindEvents;

        CreateModel(stat.CurrentEnemyStat.uniqueKey);

        DieEvent();
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

    #endregion

    #region BindEvents

    protected override void BindEvents()
    {
        stat.dieEventHandler += BindPlayDieAnimationEvent;
        animatorContoller.dieEventHandler += DieEvent;
        animatorContoller.spawnedEventHandler += BindSpawnedEvent;
        animatorContoller.attackEventHandler += BindAttackEvent;
        RecycleBindEvents();

    }
    protected override void BindSpawnedEvent()
    {
        ChangeAnimateState(UnitAnimateState.Move);

        //Move(herotransform);
    }

    private void BindAttackEvent()
    {
        if (target != null)
        {
            if (target.AnimateState != UnitAnimateState.Die)
            {
                float _damage = SetDamage();
                target.GetDamage((int)_damage, stat.CurrentEnemyStat.DamageType);
            }
            else
            {
                target = null;
                ChangeAnimateState(UnitAnimateState.Move);
                // �����ϰ��ִ� ������ ���� -> �������� �̵� ���� �����׿������ �Ŵ������� ó��
            }
        }
       
    }

  

    protected override void DieEvent()
    {
       // Debug.Log("enemy component DieEvent");
        base.DieEvent();
        //object pool add
        dieEventHandler?.Invoke(this);
        this.gameObject.SetActive(false);
    }

    #endregion

    #region CreateModel


    protected override void CreateModel(string modelUniqueKey)
    {
        base.CreateModel(modelUniqueKey);
    }

    protected override void SetModel(GameObject loadedObject)
    {
        base.SetModel(loadedObject);

    }

    public override void ResetModel()
    {
        if (model != null)
            Destroy(model);

        //���� �ϳ��� ���������� �޸� ���� ���� -> �������ִٰ� ���� ������ �ʱ�ȭ ū���̾���

        //DataManager.Instance.ResetMemory(stat.CurrentEnemyStat.uniqueKey);
    }

    #endregion


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

    #region Damage

    public override void GetDamage(int _damage, DamageType _damageType)
    {
        if (unitState == UnitState.die)
            return;


        stat.GetDamage(_damage, _damageType);

    }

   

    protected override float SetDamage()
    {
        return stat.CurrentEnemyStat.atk;
    }

    #endregion

}
