using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{

    [SerializeField] SkinnedMeshRenderer meshRenderer; 

    Animator animator;

    public Action<UnitAnimateState, float> changeAnimationEventHandler;

    public Action dieEventHandler;
    public Action spawnedEventHandler;
    public Action attackEventHandler;

    string dissolveValueKeyWord = "_DissolveValue";



    public void Init()
    {
        Material dummy = new Material(meshRenderer.material);
        meshRenderer.material = dummy;
    }


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
            StartCoroutine(DissolveEvent(-0.1f, 2f, true, 1));
        }




    }


    public void DieEvent()
    {
        //Debug.Log("DieEvent");

        StartCoroutine(DissolveEvent(1, 2f, false, 0, dieEventHandler));
    }

    IEnumerator DissolveEvent(float targetValue, float delayTime, bool isCurrentValueSetting = false, float currentValue = 0, Action action = null)
    {
        yield return null;

        float t = 0;
        float dissolveValue = meshRenderer.material.GetFloat(dissolveValueKeyWord);
        float currentDissolveValue = 0;
        if (isCurrentValueSetting)
        {
            currentDissolveValue = currentValue;
        }
        else
        {
            currentDissolveValue = meshRenderer.material.GetFloat(dissolveValueKeyWord);
        }


        while (t < delayTime)
        {
            t += Time.deltaTime;
            dissolveValue = Mathf.Lerp(currentDissolveValue, targetValue, t);
            meshRenderer.material.SetFloat(dissolveValueKeyWord, dissolveValue);
            yield return null;
        }

        meshRenderer.material.SetFloat(dissolveValueKeyWord, targetValue);
        action?.Invoke();

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
