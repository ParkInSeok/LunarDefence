using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EnemyStat
{
    protected EnemyData origineEnemyStat;
    protected EnemyData currentEnemyStat;

    public EnemyData CurrentTowerStat { get { return currentEnemyStat; } }

    protected UnitState unitState;

    public Action dieEventHandler;




    public virtual void InitStat(EnemyData stat)
    {
        unitState = UnitState.alive;

        origineEnemyStat = stat;

        currentEnemyStat = origineEnemyStat;
    }

    public virtual void SlowEvent(int percent)
    {
        currentEnemyStat.moveSpeed -= currentEnemyStat.moveSpeed * (float)percent / 100;
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
                calculationDamage -= (int)((float)calculationDamage * 100f / (100f + currentEnemyStat.def)); /* *(currentEnemyStat.propertyResistPower))*/
                break;
            case DamageType.masicDamageType:
                calculationDamage -= (int)((float)calculationDamage * 100f / (100f + currentEnemyStat.spdef)); /* *(currentEnemyStat.propertyResistPower))*/
                break;
        }

        Debug.Log("GetDamage : " + calculationDamage);

        currentEnemyStat.hp -= calculationDamage;

        if (currentEnemyStat.hp <= 0)
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
