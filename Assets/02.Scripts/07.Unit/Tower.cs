using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : BaseUnit
{
    [Header("Tower")]
    [SerializeField] TowerStat stat;

    public TowerStat Stat { get { return stat; } }

    public Action<Tower> dieEventHandler;

 

    #region Init

    public void Init(TowerData _stat, bool isActive = false)
    {
        stat.InitStat(_stat);
        
        SetBulletSpawner();
        bulletSpawner.CreateBulletAround(stat.CurrentTowerStat.flashUniqueKey);
        bulletSpawner.CreateBulletAround(stat.CurrentTowerStat.hitUniqueKey);
        bulletSpawner.CreateBullet(stat.CurrentTowerStat.bulletUniqueKey, SetHitBulletAround);


        unitState = UnitState.die;
        animateState = UnitAnimateState.Die;

        setModelCompletedEventHandler += BindEvents;

        CreateModel(stat.CurrentTowerStat.uniqueKey);
        if(!isActive)
            DieEvent();
    }

  

    public void RecycleInit(TowerData _stat)
    {
        this.gameObject.SetActive(true);
        stat.InitStat(_stat);

        bulletSpawner.CreateBulletAround(stat.CurrentTowerStat.flashUniqueKey);
        bulletSpawner.CreateBullet(stat.CurrentTowerStat.bulletUniqueKey);

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
        bulletSpawner.GetBulletAroundPool(stat.CurrentTowerStat.flashUniqueKey, SetAttackPoint);
        if (IsSkillAttack())
        {
            //next attack use skill 
            int value = UnityEngine.Random.Range(0, stat.CurrentTowerStat.attackMotionLength);
            animatorContoller.SetAttackValue((float)value);

            bulletSpawner.GetBulletPool(stat.CurrentTowerStat.bulletUniqueKey, 
                (bullet)=>SetAttackPoint(bullet,true,stat.Skill));
        }
        else
        {
            animatorContoller.SetAttackValue(0);

            bulletSpawner.GetBulletPool(stat.CurrentTowerStat.bulletUniqueKey,
                (bullet)=> SetAttackPoint(bullet,false));
        }
    }
    protected override bool IsSkillAttack()
    {
        if (stat.Skill == null)
            return false;
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


    #region Bullet

    private void SetAttackPoint(ReturnToPool_Particle obj)
    {
        obj.transform.position = CurrentAnimatorController.AttackPoint.position;
        obj.transform.rotation = CurrentAnimatorController.AttackPoint.rotation;
        obj.StartParticle();
    }

    private void SetAttackPoint(Bullet bullet, bool isSkill = false, Skill skill = null)
    {
        bullet.transform.position = CurrentAnimatorController.AttackPoint.position;
        bullet.transform.rotation = CurrentAnimatorController.AttackPoint.rotation;
        bullet.StartParticle();
        bullet.SetData(stat.CurrentTowerStat, isSkill,skill);
    }

    private void SetHitBulletAround(Bullet obj)
    {
        obj.hitEventHandler += BindHitEvent;
    }

    private void BindHitEvent(Vector3 pos, Quaternion rot, ContactPoint contact, 
        bool UseFirePointRotation, Vector3 rotationOffset)
    {
        bulletSpawner.GetBulletAroundPool(stat.CurrentTowerStat.hitUniqueKey,
            (particle)=> SetHitPoint(particle,pos,rot, contact, UseFirePointRotation , rotationOffset));

    }

    private void SetHitPoint(ReturnToPool_Particle obj, Vector3 pos, Quaternion rot, ContactPoint contact, 
        bool UseFirePointRotation, Vector3 rotationOffset)
    {
        if (obj != null)
        {
           
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            if (UseFirePointRotation) { obj.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
            else if (rotationOffset != Vector3.zero) { obj.transform.rotation = Quaternion.Euler(rotationOffset); }
            else { obj.transform.LookAt(contact.point + contact.normal); }

            //Destroy hit effects depending on particle Duration time
            obj.StartParticle();
        }
    }


    #endregion




}
