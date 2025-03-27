using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class BackEndLogin : MonoBehaviour
{
    private static BackEndLogin _instance = null;

    public static BackEndLogin Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackEndLogin();
            }

            return _instance;
        }
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void CustomSignUp(string id, string pw)
    //{
    //    // Step 2. 회원가입 구현하기 로직
    //    Debug.Log("회원가입을 요청합니다.");

    //    var bro = Backend.BMember.CustomSignUp(id, pw);

    //    if (bro.IsSuccess())
    //    {
    //        Debug.Log("회원가입에 성공했습니다. : " + bro);
    //    }
    //    else
    //    {
    //        Debug.LogError("회원가입에 실패했습니다. : " + bro);
    //    }
    //}

    //public void CustomLogin(string id, string pw)
    //{
    //    // Step 3. 로그인 구현하기 로직
    //}

    //public void UpdateNickname(string nickname)
    //{
    //    // Step 4. 닉네임 변경 구현하기 로직
    //}

    //public async void Test()
    //{
    //    await Task.Run(() =>
    //    {
    //        BackEndLogin.Instance.CustomSignUp("user1", "1234"); // [추가] 뒤끝 회원가입 함수
    //        Debug.Log("테스트를 종료합니다.");
    //    });
    //}




}
