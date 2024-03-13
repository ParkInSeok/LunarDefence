using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Advantage 
{
    

    public string uniqueKey;                        //유니크 키
    public string iconUniqueKey;                        //icon 유니크 키
    public Sprite icon;

    public string title;                             //제목
    public string info;                             //정보

    public int level;                               //레벨

    
    public Advantage(){

    }


    public Advantage(string _uniqueKey, string _iconUniqueKey, string _title, string _info, int _level)
    {
        uniqueKey = _uniqueKey;
        iconUniqueKey = _iconUniqueKey;
        title = _title;
        info = _info;
        level = _level;

        //todo iconuniquekey convert to icon
        DataManager.Instance.GetSprite("testsprite.png",SetIconSprite);

    }

    public Advantage(AdvantageData advantageData)
    {
        uniqueKey = advantageData.uniqueKey;
        iconUniqueKey = advantageData.iconUniqueKey;
        title = advantageData.title;
        info = advantageData.info;
        level = advantageData.level;

        //todo iconuniquekey convert to icon


    }

    void SetIconSprite(Sprite sprite)
    {
        icon = sprite;
    }



}
