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



    public Coroutine DelayFunction_FixedUpdate(Action action)
    {
        return StartCoroutine(_DelayFunction_FixedUpdate(action));
    }
    public Coroutine DelayFunction_EndOfFrame(Action action)
    {
        return StartCoroutine(_DelayFunction_EndOfFrame(action));
    }
    public Coroutine DelayFunction_NextEndOfFrame(Action action)
    {
        return StartCoroutine(_DelayFunction_NextEndOfFrame(action));
    }
    public Coroutine DelayFunction(Action action, float delayTime)
    {
        return StartCoroutine(_DelayFunction(action, delayTime));
    }
    public Coroutine DelayFunction_RealTime(Action action, float delayTime)
    {
        return StartCoroutine(_DelayFunction_RealTime(action, delayTime));
    }

    IEnumerator _DelayFunction(Action action, float delayTime)
    {
        yield return CoroutineHelper.WaitForSeconds(delayTime);
        action?.Invoke();
    }

    IEnumerator _DelayFunction_RealTime(Action action, float delayTime)
    {
        yield return CoroutineHelper.WaitForSecondsRealTime(delayTime);
        action?.Invoke();
    }

    IEnumerator _DelayFunction_EndOfFrame(Action action)
    {
        yield return CoroutineHelper.WaitForEndOfFrame;
        action?.Invoke();
    }

    IEnumerator _DelayFunction_FixedUpdate(Action action)
    {
        yield return CoroutineHelper.WaitForFixedUpdate;
        action?.Invoke();
    }

    IEnumerator _DelayFunction_NextEndOfFrame(Action action)
    {
        yield return null;
        yield return CoroutineHelper.WaitForEndOfFrame;
        action?.Invoke();
    }

}



//사용법 : yield return CoroutineHelper.WaitForSeconds(시간);
static class CoroutineHelper
{
    class FloatComparer : IEqualityComparer<float>
    {
        bool IEqualityComparer<float>.Equals(float x, float y)
        {
            return x == y;
        }

        int IEqualityComparer<float>.GetHashCode(float obj)
        {
            return obj.GetHashCode();
        }
    }

    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();

    private static readonly Dictionary<float, WaitForSeconds> _timeInterval =
        new Dictionary<float, WaitForSeconds>(new FloatComparer());

    private static readonly Dictionary<float, WaitForSecondsRealtime> _timeIntervalReal =
        new Dictionary<float, WaitForSecondsRealtime>(new FloatComparer());

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        WaitForSeconds wfs;
        if (!_timeInterval.TryGetValue(seconds, out wfs))
            _timeInterval.Add(seconds, wfs = new WaitForSeconds(seconds));
        return wfs;
    }

    public static WaitForSecondsRealtime WaitForSecondsRealTime(float seconds)
    {
        WaitForSecondsRealtime wfsReal;
        if (!_timeIntervalReal.TryGetValue(seconds, out wfsReal))
            _timeIntervalReal.Add(seconds, wfsReal = new WaitForSecondsRealtime(seconds));
        return wfsReal;
    }




}
