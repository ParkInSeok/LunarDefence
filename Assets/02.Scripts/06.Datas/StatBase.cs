
using System;

public enum UnitState
{
    alive,
    die,
}



[Serializable]
public abstract class StatBase 
{

    protected UnitState unitState;

    public Action dieEventHandler;

    public abstract void DieEvent();
    public abstract void GetDamage(int _damage, DamageType _damageType);





}
