using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{

    Animator animator;

    public Action<UnitAnimateState, float> changeAnimationEventHandler;

    public Action dieEventHandler;
    public Action spawnedEventHandler;


    public void ChangeAnimateState(UnitAnimateState _state, float animSpeed)
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        animator.speed = animSpeed;

        animator.SetTrigger(_state.ToString());

        changeAnimationEventHandler?.Invoke(_state, animSpeed);

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





}
