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
