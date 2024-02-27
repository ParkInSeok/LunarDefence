using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameData : MonoBehaviour
{
   
    public GameData data;


    public int getCount;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            data.datas = data.GetRandomAdvantageData(getCount);
        }
    }




}
