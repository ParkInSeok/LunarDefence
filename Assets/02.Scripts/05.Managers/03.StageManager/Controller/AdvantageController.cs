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
        //UIȰ��ȭ ��庥Ƽ�� ���̺��� 3�� �̾ƿ��� ����
    }

    public void SelectedAdvantageEvent(AdvantageData data)
    {
        AdvantageType advantageType = (AdvantageType)Enum.Parse(typeof(AdvantageType), data.uniqueKey);

        //������ ��庥Ƽ�� �����Ű��
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
