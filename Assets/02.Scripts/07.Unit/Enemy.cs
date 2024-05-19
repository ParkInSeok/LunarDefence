using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseUnit
{
    [Header("Enemy")]
    [SerializeField] EnemyStat stat;

    public EnemyStat Stat { get { return stat; } }

    int targetIndex = 0;
    
    Coroutine moveCoroutine;
    
    public Action<Enemy> dieEventHandler;
    
    List<PathNode> path;
   


    [Header("Debugging")]
    public BaseUnit target;





    #region Init

    public void Init(EnemyData _stat)
    {
        stat = new EnemyStat();
        stat.InitStat(_stat);
        SetBulletSpawner();
        unitState = UnitState.die;
        animateState = UnitAnimateState.Die;
        setModelCompletedEventHandler += BindEvents;

        CreateModel(stat.CurrentEnemyStat.uniqueKey);

        targetIndex = 0;

        DieEvent();
    }
    public void RecycleInit(EnemyData _stat)
    {
        this.gameObject.SetActive(true);
        var regacyKey = stat.CurrentEnemyStat.uniqueKey;
        stat.InitStat(_stat);
        unitState = UnitState.alive;
        animateState = UnitAnimateState.None;
        setModelCompletedEventHandler = null;
        setModelCompletedEventHandler += BindEvents;

        if (animatorControllerPool.ContainsKey(stat.CurrentEnemyStat.uniqueKey))
        {
            Debug.Log("animatorControllerPool ContainsKey ");
            SetModel(animatorControllerPool[stat.CurrentEnemyStat.uniqueKey]);
        }
        else
        {
            Debug.Log("animatorControllerPool not ContainsKey ");
            CreateModel(stat.CurrentEnemyStat.uniqueKey);
        }

        targetIndex = 0;
        path = InfinityStageManager.Instance.PathController.TargetPath;

    }

    #endregion

    #region BindEvents

    protected override void BindEvents()
    {
        stat.dieEventHandler += BindPlayDieAnimationEvent;
        animatorContoller.dieEventHandler += DieEvent;
        animatorContoller.spawnedEventHandler += BindSpawnedEvent;
        animatorContoller.attackEventHandler += BindAttackEvent;

        if (animatorControllerPool.ContainsKey(stat.CurrentEnemyStat.uniqueKey) == false)
            animatorControllerPool.Add(stat.CurrentEnemyStat.uniqueKey, animatorContoller);

        RecycleBindEvents();

    }
    protected override void BindSpawnedEvent()
    {
        ChangeAnimateState(UnitAnimateState.Move);

        Move();


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
                // 공격하고있던 유닛이 죽음 -> 다음으로 이동 만약 영웅죽였을경우 매니저에서 처리
            }
        }
       
    }

    private void BindStunEndEvent(UnitAnimateState arg1, float arg2)
    {
        if(target.AnimateState !=  UnitAnimateState.Die)
        {
            if (target != null)
            {
                Attack();
            }
            else
            {
                ChangeAnimateState(UnitAnimateState.Move);
            }
        }
        else
        {
            ChangeAnimateState(UnitAnimateState.Move);
        }

        animatorContoller.changeAnimationEventHandler -= BindStunEndEvent;

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
        {
            model.SetActive(false);
            //Destroy(model);
        }

        //씬에 하나라도 남아있으면 메모리 리셋 안함 -> 가지고있다가 게임 끝나면 초기화 큰차이없음

        //DataManager.Instance.ResetMemory(stat.CurrentEnemyStat.uniqueKey);
    }

    #endregion


    public void Move()
    {
        if(moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(_Move());
    }

    IEnumerator _Move()
    {
        if (path == null || path.Count == 0)
        {
            Debug.LogError("Path is null or empty.");
            yield break;
        }

        Vector3 currentWaypoint = path[0].position;
        Vector3 direction = currentWaypoint - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        while (targetIndex < path.Count - 1)
        {
            float distance = Vector3.Distance(transform.position, currentWaypoint);

            if (distance <= 0.1f)
            {
                targetIndex++;
                currentWaypoint = path[targetIndex].position;

                if (path[targetIndex].isTower)
                {
                    yield return StartCoroutine(RotateAttackUnit(direction, currentWaypoint, targetRotation));
                    target = path[targetIndex].Unit;
                    Attack();
                    yield break;
                }

                if (targetIndex >= path.Count - 1)
                {
                    break; // End of path reached
                }

            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint,
                stat.CurrentEnemyStat.moveSpeed * Time.deltaTime);
            LootAtRotation(direction, currentWaypoint, targetRotation);
            yield return null;
        }

        yield return StartCoroutine(RotateAttackUnit(direction, currentWaypoint, targetRotation));
        // 앞에 타워가 있다면? 멈추고 공격


    }


    public override void ChangeAnimateState(UnitAnimateState _state, float animSpeed = 1, float attackValue = 0)
    {

        if (animateState == UnitAnimateState.Die)
            return;

        if (animateState == _state)
            return;

        if(animateState == UnitAnimateState.Stun)
        {
            animatorContoller.changeAnimationEventHandler += BindStunEndEvent;
        }


        animateState = _state;

        animatorContoller.ChangeAnimateState(_state, animSpeed, attackValue);

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

    protected override bool IsSkillAttack()
    {
        return false;
    }

    protected override void SetAttackType()
    {

    }

    #endregion

}
