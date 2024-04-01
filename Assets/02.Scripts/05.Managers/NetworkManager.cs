using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;


public class NetworkManager : Singleton<NetworkManager>
{



    // Start is called before the first frame update
    void Start()
    {
        //var bro = Backend.Initialize(true); // 뒤끝 초기화

        //if (bro.IsSuccess())
        //{
        //    Debug.Log("초기화 성공 : " + bro); // 성공일 경우 statusCode 204 Success
        //}
        //else
        //{
        //    Debug.LogError("초기화 실패 : " + bro); // 실패일 경우 statusCode 400대 에러 발생
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Init()
    {


    }


}
