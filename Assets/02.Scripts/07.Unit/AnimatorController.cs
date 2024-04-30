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
                Debug.Log("current anim state " + _state);
                noExistSpawnAnimEventHandler?.Invoke();
                //스폰 default 연출 이후 spawnedEventHandler Action 실행 
            }
            else
            {
                Debug.Log("ExistSpawnAnimfalse " + _state);
            }
        }

       

    }


    public void DieEvent()
    {
        //Debug.Log("DieEvent");

        StartCoroutine(DissolveEvent());
    }

    IEnumerator DissolveEvent()
    {
        yield return null;

        float t = 0;
        float dissolveValue = meshRenderer.material.GetFloat("_DissolveValue");
        float currentDissolveValue = meshRenderer.material.GetFloat("_DissolveValue");
        while (t < 2f)
        {
            t += Time.deltaTime;
            dissolveValue = Mathf.Lerp(currentDissolveValue, 1, t);
            meshRenderer.material.SetFloat("_DissolveValue", dissolveValue);
            yield return null;
        }

        meshRenderer.material.SetFloat("_DissolveValue", 1f);
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
