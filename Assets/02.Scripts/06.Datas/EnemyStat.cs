using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EnemyStat : StatBase
{
    protected EnemyData origineEnemyStat;
    public EnemyData OrigineEnemyStat { get { return origineEnemyStat; } }
    [SerializeField] protected EnemyData currentEnemyStat;

    public EnemyData CurrentEnemyStat { get { return currentEnemyStat; } }





    public virtual void InitStat(EnemyData stat)
    {
        origineEnemyStat = new EnemyData(stat);
        currentEnemyStat = new EnemyData(stat);
    }

    public virtual void SlowEvent(int percent)
    {
        currentEnemyStat.moveSpeed -= currentEnemyStat.moveSpeed * (float)percent / 100;
    }

    public override void GetDamage(int _damage, DamageType _damageType)
    {
        //TO DO property type , enemy type not exist 0225 
        var calculationDamage = _damage;
        var currentDamage = _damage;
        switch (_damageType)
        {
            case DamageType.physicalDamageType:
                calculationDamage = (int)((float)currentDamage * 100f / (100f + currentEnemyStat.def)); /* *(currentEnemyStat.propertyResistPower))*/
                break;
            case DamageType.masicDamageType:
                calculationDamage = (int)((float)currentDamage * 100f / (100f + currentEnemyStat.spdef)); /* *(currentEnemyStat.propertyResistPower))*/
                break;
        }


        Debug.Log("GetDamage : " + calculationDamage + "current Unit : " + currentEnemyStat.uniqueKey);

        currentEnemyStat.hp -= calculationDamage;

        if (currentEnemyStat.hp <= 0)
        {
            DieEvent();
        }



    }

    public override void DieEvent()
    {

        dieEventHandler?.Invoke();
    }


}
