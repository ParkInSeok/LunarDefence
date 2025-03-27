using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AdvantageType // uniquekey
{
    advatage1,
    advatage2,
    advatage3,
}



public class AdvantageController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {

    }

    public void BindEvents()
    {

    }

    public void SelectAdvantageEvent()
    {
        //UI활성화 어드벤티지 테이블에서 3개 뽑아오기 로직
    }

    public void SelectedAdvantageEvent(AdvantageData data)
    {
        AdvantageType advantageType = (AdvantageType)Enum.Parse(typeof(AdvantageType), data.uniqueKey);

        //선택한 어드벤티지 적용시키기
        switch (advantageType)
        {
            case AdvantageType.advatage1:
                break;
            case AdvantageType.advatage2:
                break;
            case AdvantageType.advatage3:
                break;
        }
    }




}
