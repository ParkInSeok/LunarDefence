using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    //사용법 : yield return CoroutineHelper.WaitForSeconds(시간);
    public class CoroutineHelper
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

        static WaitForSeconds WaitForSeconds(float seconds)
        {
            WaitForSeconds wfs;
            if (!_timeInterval.TryGetValue(seconds, out wfs))
                _timeInterval.Add(seconds, wfs = new WaitForSeconds(seconds));
            return wfs;
        }

        static WaitForSecondsRealtime WaitForSecondsRealTime(float seconds)
        {
            WaitForSecondsRealtime wfsReal;
            if (!_timeIntervalReal.TryGetValue(seconds, out wfsReal))
                _timeIntervalReal.Add(seconds, wfsReal = new WaitForSecondsRealtime(seconds));
            return wfsReal;
        }

        public static IEnumerator DelayFunction(Action action, float delayTime)
        {
            yield return CoroutineHelper.WaitForSeconds(delayTime);
            action?.Invoke();
        }

        public static IEnumerator DelayFunction_RealTime(Action action, float delayTime)
        {
            yield return CoroutineHelper.WaitForSecondsRealTime(delayTime);
            action?.Invoke();
        }

        public static IEnumerator DelayFunction_EndOfFrame(Action action)
        {
            yield return CoroutineHelper.WaitForEndOfFrame;
            action?.Invoke();
        }

        public static IEnumerator DelayFunction_FixedUpdate(Action action)
        {
            yield return CoroutineHelper.WaitForFixedUpdate;
            action?.Invoke();
        }

        public static IEnumerator DelayFunction_NextEndOfFrame(Action action)
        {
            yield return null;
            yield return CoroutineHelper.WaitForEndOfFrame;
            action?.Invoke();
        }

    }



}

public class UtilityManager : Singleton<UtilityManager>
{

}



