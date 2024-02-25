using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityManager : Singleton<UtilityManager>
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Init()
    {
        
    }



    public Coroutine DelayFunction(Action action, float delayTime)
    {
        return StartCoroutine(_DelayFunction(action, delayTime));
    }


    IEnumerator _DelayFunction(Action action, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        action?.Invoke();
    }







}
