
using System;
using UnityEngine;

public enum UnitState
{
    alive,
    die,
}



[Serializable]
public abstract class StatBase 
{

    [SerializeField] protected UnitState unitState;

    public Action dieEventHandler;

    public abstract void DieEvent();
    public abstract void GetDamage(int _damage, DamageType _damageType);





}
