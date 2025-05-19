using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FusionController
{

    public void Init()
    {
        BindEvents();

    }

    void BindEvents()
    {

    }

    public bool IsCanFusion(Tower tower1, Tower tower2)
    {
        return IsEqualTowerType(tower1, tower2) && IsEqualTowerStar(tower1, tower2);

    }

    public bool IsEqualTowerStar(Tower tower1, Tower tower2)
    {
        return tower1.Stat.CurrentTowerStat.Star == tower2.Stat.CurrentTowerStat.Star;
    }


    public bool IsEqualTowerType(Tower tower1, Tower tower2)
    {
        if (tower1.Stat.CurrentTowerStat.uniqueKey.Equals(tower2.Stat.CurrentTowerStat.uniqueKey))
            return true;

        return false;
    }


    public void TryFusion(Tower tower1, Tower tower2)
    {
        tower1.ConsumeTower();
        tower2.LevelUpStar();
    }

    public bool isCanLevelUp(Tower tower1)
    {
        if (tower1.Stat.CurrentTowerStat.Star == StarState.max - 1)
            return false;

        return true;
    }




}
