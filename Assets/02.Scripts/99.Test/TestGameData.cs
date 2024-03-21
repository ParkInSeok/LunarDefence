using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameData : MonoBehaviour
{

    public GameData data;


    public int getCount;
    public GameObject testObject;
    public Sprite testsprite;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            DataManager.Instance.GetGameObject("Cube",SetGameObject);

        }
        if (Input.GetKeyDown(KeyCode.F2))
        {

        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            DataManager.Instance.GetSprite("testsprite", SetSprite);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {

        }

        if (Input.GetKeyDown(KeyCode.F5))
        {

        }

        if (Input.GetKeyDown(KeyCode.F6))
        {

        }

        if (Input.GetKeyDown(KeyCode.F7))
        {

        }

        if (Input.GetKeyDown(KeyCode.F8))
        {

        }

        if (Input.GetKeyDown(KeyCode.F9))
        {

        }

        if (Input.GetKeyDown(KeyCode.F10))
        {

        }



    }




    public void SetSprite(Sprite set)
    {
        testsprite = set;
        
    }

    void SetGameObject(GameObject set)
    {
        testObject = set;
    }


}
