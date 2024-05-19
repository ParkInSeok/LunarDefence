
using System;
using UnityEngine;




[Serializable]
public abstract class BaseStat 
{



    public Action dieEventHandler;

    public abstract void DieEvent();
    public abstract void GetDamage(int _damage, DamageType _damageType);





}
