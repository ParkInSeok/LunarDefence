using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System;

public class NetworkManager : Singleton<NetworkManager>
{
    private string apiKey = "283af007-324b-43b0-8d8d-18a0951877e0";
    //private string apiKey = "MROERxM2NuHm/BizgS6zc/yEU6MLqUryMdj+5j6DZEBRYTuh2JqtFRD2HcrT3AHD2FPpmnkt4U9eG8lwXUXcDw==";
    private string url = "https://api.random.org/json-rpc/4/invoke";


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

    public void GetRandomNumber(int min, int max, Action<int> endCall)
    {
        Task.Run(()=> _GetRandomNumberAsync(min, max, endCall));

        StartCoroutine(_GetRandomNumber(min, max, endCall));
    }

    async Task _GetRandomNumberAsync(int min, int max , Action<int> endCall)
    {
        int random = await _GetRandomNumberIntAsync(min, max);

        endCall?.Invoke(random);
    }

    private async Task<int> _GetRandomNumberIntAsync(int min, int max)
    {
        string jsonData = "{\"jsonrpc\":\"2.0\",\"method\":\"generateIntegers\",\"params\":{\"apiKey\":\"" + apiKey + "\",\"n\":1,\"min\":1,\"max\":100},\"id\":1}";
        //string jsonData = "{\"jsonrpc\":\"2.0\",\"method\":\"generateIntegers\",\"params\":{\"apiKey\":\"" + apiKey + "\",\"n\":1,\"min\":\""+min+ "\",\"max\":\"" + max+ "\"},\"id\":1}";
        Debug.Log("task jsonData " + jsonData);


        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
                return -1; // 오류 발생 시 -1 반환
            }
            else
            {
                Debug.Log("task Response: " + request.downloadHandler.text);

                // JSON 응답 파싱
                JObject response = JObject.Parse(request.downloadHandler.text);
                int randomNumber = response["result"]["random"]["data"][0].Value<int>();
                return randomNumber;
            }
        }
    




    }

    IEnumerator _GetRandomNumber(int min, int max , Action<int> endCall)
    {
       // string jsonData = "{\"jsonrpc\":\"2.0\",\"method\":\"generateIntegers\",\"params\":{\"apiKey\":\"" + apiKey + "\",\"n\":1,\"min\":" + min + ",\"max\":" + max + "},\"id\":1}";
       // string jsonData = "{\"jsonrpc\":\"2.0\",\"method\":\"generateIntegers\",\"params\":{\"apiKey\":\"" + apiKey + "\",\"n\":1,\"min\":" + min + ",\"max\":" + max + "},\"id\":1}";
        //                   { "jsonrpc":"2.0","method":"generateIntegers","params":{ "apiKey":"283af007-324b-43b0-8d8d-18a0951877e0","n":1,"min":1,"max":100},"id":1}
        //string jsonData = "{\"jsonrpc\":\"2.0\",\"method\":\"generateIntegers\",\"params\":{\"apiKey\":\"" + apiKey + "\",\"n\":1,\"min\":1,\"max\":100},\"id\":1}";
        string jsonData = "{\"jsonrpc\":\"2.0\",\"method\":\"generateIntegers\",\"params\":{\"apiKey\":\"" + apiKey + "\",\"n\":1,\"min\":\""+min+ "\",\"max\":\"" + max+ "\"},\"id\":1}";
        Debug.Log("coroutine jsonData " + jsonData);
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
                endCall?.Invoke(-1);
            }
            else
            {
                Debug.Log("Response: " + request.downloadHandler.text);

                // JSON 응답 파싱
                JObject response = JObject.Parse(request.downloadHandler.text);
                int randomNumber = response["result"]["random"]["data"][0].Value<int>();
                Debug.Log("coroutine Random Number: " + randomNumber);
                endCall?.Invoke(randomNumber);
            }
        }
    }


}
