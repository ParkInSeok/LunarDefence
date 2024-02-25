using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  System;

public enum UnitState{
    alive,
    die,
}



[Serializable]
public class TowerStat
{

    protected TowerData origineTowerStat;
    protected TowerData currentTowerStat;

    public TowerData CurrentTowerStat { get { return currentTowerStat; } }

    protected UnitState unitState;

    public Action dieEventHandler;

    public virtual void InitStat(TowerData stat)
    {
        unitState = UnitState.alive;

        origineTowerStat = stat;

        currentTowerStat = origineTowerStat;
    }

    public virtual void GetDamage(int _damage, DamageType _damageType)
    {
        //TO DO property type , enemy type not exist 0225 
        if (unitState == UnitState.die)
            return;

        var calculationDamage = _damage;
        switch (_damageType)
        {
            case DamageType.physicalDamageType:
                calculationDamage -= (int)((float)calculationDamage * 100f / (100f + currentTowerStat.def)); /* *(currentEnemyStat.propertyResistPower))*/
                break;
            case DamageType.masicDamageType:
                calculationDamage -= (int)((float)calculationDamage * 100f / (100f+currentTowerStat.spdef)); /* *(currentEnemyStat.propertyResistPower))*/
                break;
        }

        Debug.Log("GetDamage : " + calculationDamage);

        currentTowerStat.hp -= calculationDamage;

        if(currentTowerStat.hp <= 0)
        {
            DieEvent();
        }


    }

    public void DieEvent()
    {
        unitState = UnitState.die;

        dieEventHandler?.Invoke();
    }

}
