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

    public AdvantageData(string _uniqueKey , string _iconUniqueKey, string _title, string _info, int _level)
    {
        uniqueKey = _uniqueKey;
        iconUniqueKey = _iconUniqueKey;
        title = _title;
        info = _info;
        level = _level;

    }





    public Advantage AdvantageDataConvertToAdvantage()
    {
        Advantage advantage = new Advantage(uniqueKey , iconUniqueKey, title, info , level);

        return advantage;
    }


    public void LevelUpAdvantage()
    {
        level++;

    }



}
