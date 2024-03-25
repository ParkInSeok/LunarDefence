
using System;
using UnityEngine;




[Serializable]
public abstract class StatBase 
{



    public Action dieEventHandler;

    public abstract void DieEvent();
    public abstract void GetDamage(int _damage, DamageType _damageType);





}
