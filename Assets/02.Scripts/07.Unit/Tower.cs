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

        if (animatorControllerPool.ContainsKey(stat.CurrentTowerStat.uniqueKey))
        {
            SetModel(animatorControllerPool[stat.CurrentTowerStat.uniqueKey].gameObject);
        }
        else
        {
            CreateModel(stat.CurrentTowerStat.uniqueKey);
        }

    }


    #endregion

    #region BindEvents

    protected override void BindEvents()
    {
        stat.dieEventHandler += BindPlayDieAnimationEvent;
        animatorContoller.dieEventHandler += DieEvent;
        animatorContoller.spawnedEventHandler += BindSpawnedEvent;
        animatorContoller.attackEventHandler += BindAttackEvent;
        animatorContoller.skillEventHandler += BindSkillEvent;

        if (animatorControllerPool.ContainsKey(stat.CurrentTowerStat.uniqueKey) == false)
            animatorControllerPool.Add(stat.CurrentTowerStat.uniqueKey, animatorContoller);

        ChangeAnimateState(UnitAnimateState.Spawn);



    }

    

    private void BindAttackEvent()
    {
        //źȯ�߻� ����Ʈ���� �ִϸ��̼� ��ų ��� �� ó�� ��ų�� ���� �ٸ���ų ó��
        //���� Ÿ���� ���ݿ� ���� ����


        //next attack Type setting
        SetAttackType();
    }

    

    private void BindSkillEvent(float obj)
    {
        //���� Ÿ���� ��ų�� ���� ����

        //next attack Type setting
        SetAttackType();
    }
    protected override void SetAttackType()
    {
        if (IsSkillAttack())
        {
            //next attack use skill 
            int value = UnityEngine.Random.Range(0, stat.CurrentTowerStat.attackMotionLength);
            animatorContoller.SetAttackValue((float)value);
        }
        else
        {
            animatorContoller.SetAttackValue(0);
        }
    }
    protected override bool IsSkillAttack()
    {
        if(stat.Skill.activationType == SkillActivationType.mana)
        {
            //mana �� max mana�ϋ� return true
        }
        else
        {
            int random = UnityEngine.Random.Range(0, 100);
            if (random < stat.Skill.activatePercent)
            {
                return true;
            }
        }
        return false;
    }




    private void BindStunEndEvent(UnitAnimateState arg1, float arg2)
    {
        //if (target.AnimateState != UnitAnimateState.Die)
        //{
        //    if (target != null)
        //    {
        //        Attack();
        //    }
        //    else
        //    {
        //        ChangeAnimateState(UnitAnimateState.Move);
        //    }
        //}
        //else
        //{
        //    ChangeAnimateState(UnitAnimateState.Move);
        //}

        animatorContoller.changeAnimationEventHandler -= BindStunEndEvent;

    }

    protected override void BindSpawnedEvent()
    {
        ChangeAnimateState(UnitAnimateState.Idle);



    }

    protected override void DieEvent()
    {
        //Debug.Log("tower DieEvent");
        base.DieEvent();
        //object pool add
        dieEventHandler?.Invoke(this);
        this.gameObject.SetActive(false);

        //���� ��ġ���ִ� tileeventTrigger.DieUnit 

    }


    #endregion

    public override void ChangeAnimateState(UnitAnimateState _state, float animSpeed = 1, float attackValue = 0 )
    {
        if (animateState == UnitAnimateState.Die)
            return;

        if (animateState == _state)
            return;

        if (animateState == UnitAnimateState.Stun)
        {
            animatorContoller.changeAnimationEventHandler += BindStunEndEvent;
        }
        animateState = _state;

        animatorContoller.ChangeAnimateState(_state, animSpeed, attackValue);
    }

    public override void GetDamage(int _damage, DamageType _damageType)
    {
        if (unitState == UnitState.die)
            return;

        stat.GetDamage(_damage, _damageType);


    }

  


    public override void ResetModel()
    {
        if (model != null)
        {
            model.SetActive(false);
            //Destroy(model);
        }

    }

   
    protected override void Attack()
    {
        //��ų Ȯ���� ���� attackvalue set �ٵ� ���� �ִϸ��̼��� �������� value ���������
        SetAttackType();

        ChangeAnimateState(UnitAnimateState.Attack,stat.CurrentTowerStat.attackSpeed);
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
            //�Ӱ� ������ ó��
        }
        else
        {
            _damage = stat.CurrentTowerStat.atk;
            //�Ӱ� ������ ó��
        }


        return _damage;
    }

}
