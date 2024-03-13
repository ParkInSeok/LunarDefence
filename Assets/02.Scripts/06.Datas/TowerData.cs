using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TowerData : DataBase
{
    public int critical;            //크확
    public int criticalDamage;      //크증뎀
    public int lifeBloodAbsorption; // 흡혈
   
    public TowerData(){
        
    }
}
