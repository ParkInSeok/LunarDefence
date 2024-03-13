using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AdvantageData
{
  
    public string uniqueKey;                        //유니크 키
    public string iconUniqueKey;                        //icon 유니크 키

    public string title;                             //제목
    public string info;                             //정보

    public int level;                               //레벨

    public AdvantageData()
    {
        
    }


    public Advantage AdvantageDataConvertToAdvantage()
    {
        Advantage advantage = new Advantage(uniqueKey , iconUniqueKey, title, info , level);

        return advantage;
    }



}
