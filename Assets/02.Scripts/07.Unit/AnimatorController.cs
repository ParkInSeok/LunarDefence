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
    public Action noExistSpawnAnimEventHandler;

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
            if (stateinfo.IsTag("ExistSpawnAnimfalse") == true)
            {
                //spawnedEventHandler Action 실행 안함
                //move state change 
                //Debug.Log("current anim state " + _state);
                StartCoroutine(SpawnDissolveEvent(-0.1f, 2f, true, 1));

                //스폰 default 연출 이후 spawnedEventHandler Action 실행 
            }
            else
            {
               // Debug.Log("ExistSpawnAnimfalse " + _state);
            }
        }

       

    }


    public void DieEvent()
    {
        //Debug.Log("DieEvent");

        StartCoroutine(DieDissolveEvent(1f,2f));
    }

    IEnumerator DieDissolveEvent(float targetValue , float delayTime)
    {
        yield return null;

        float t = 0;
        float dissolveValue = meshRenderer.material.GetFloat(dissolveValueKeyWord);
        float currentDissolveValue = meshRenderer.material.GetFloat(dissolveValueKeyWord);

        while (t < delayTime)
        {
            t += Time.deltaTime;
            dissolveValue = Mathf.Lerp(currentDissolveValue, targetValue, t);
            meshRenderer.material.SetFloat(dissolveValueKeyWord, dissolveValue);
            yield return null;
        }

        meshRenderer.material.SetFloat(dissolveValueKeyWord, targetValue);
        dieEventHandler?.Invoke();

    }

    IEnumerator SpawnDissolveEvent(float targetValue, float delayTime, bool isCurrentValueSetting = false, float currentValue = 0)
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
        noExistSpawnAnimEventHandler?.Invoke();

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
