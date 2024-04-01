using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{

    Animator animator;

    public Action<UnitAnimateState, float> changeAnimationEventHandler;

    public Action dieEventHandler;
    public Action spawnedEventHandler;
    public Action attackEventHandler;
    public Action noExistSpawnAnimEventHandler;

    public void ChangeAnimateState(UnitAnimateState _state, float animSpeed)
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        animator.speed = animSpeed;

        animator.SetTrigger(_state.ToString());

        changeAnimationEventHandler?.Invoke(_state, animSpeed);

        var stateinfo = animator.GetCurrentAnimatorStateInfo(0);

        if (_state == UnitAnimateState.Spawn)
        {
            if (stateinfo.IsTag("ExistSpawnAnimfalse") == false)
            {
                //spawnedEventHandler Action 실행 안함
                //move state change 
                Debug.Log("current anim state " + _state);
                noExistSpawnAnimEventHandler?.Invoke();
            }
            else
            {
                //spawnedEventHandler Action 실행
            }
        }

       

    }


    public void DieEvent()
    {
        //Debug.Log("DieEvent");
        dieEventHandler?.Invoke();
    }

    public void SpawnedEvent()
    {
        //Debug.Log("SpawnedEvent");
        spawnedEventHandler?.Invoke();
    }

    public void AttackEvent()
    {
        attackEventHandler?.Invoke();
    }



}
