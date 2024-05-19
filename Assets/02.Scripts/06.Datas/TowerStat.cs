using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  System;




[Serializable]
public class TowerStat : BaseStat
{

    protected TowerData origineTowerStat;
    [SerializeField] protected TowerData currentTowerStat;

    

    public TowerData CurrentTowerStat { get { return currentTowerStat; } }


    protected Skill skill;

    public Skill Skill { get { return skill; } }

    public virtual void InitStat(TowerData stat)
    {

        origineTowerStat = stat;

        currentTowerStat = origineTowerStat;
        if (string.IsNullOrEmpty(stat.skillUniqueKey) == false)
        {
            skill = DataManager.Instance.GameData.GetSkill(stat.skillUniqueKey);
        }
    }

    public override void GetDamage(int _damage, DamageType _damageType)
    {
        //TO DO property type , enemy type not exist 0225 


        var calculationDamage = _damage;
        var currentDamage = _damage;
        switch (_damageType)
        {
            case DamageType.physicalDamageType:
                calculationDamage = (int)((float)currentDamage * 100f / (100f + currentTowerStat.def)); /* *(currentEnemyStat.propertyResistPower))*/
                break;
            case DamageType.masicDamageType:
                calculationDamage = (int)((float)currentDamage * 100f / (100f + currentTowerStat.spdef)); /* *(currentEnemyStat.propertyResistPower))*/
                break;
        }

        Debug.Log("GetDamage : " + calculationDamage + "current Unit : " + currentTowerStat.uniqueKey);

        currentTowerStat.hp -= calculationDamage;

        if(currentTowerStat.hp <= 0)
        {
            DieEvent();
        }


    }

    public override void DieEvent()
    {

        dieEventHandler?.Invoke();
    }

}
