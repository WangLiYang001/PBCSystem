using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;
public class HVeySerApi : HSingleTonMono<HVeySerApi>
{
    //常量命名规则:首字母大写
    public const string IP = "123.57.34.244";
    public const string Port = "8090";

# region API 接口
    //
    //查询所有已授权用户信息
    public void QueryAllInfo(Action<string,string> resultCallback)
    {
        string method = "QueryAllKey";
        string url = "http://" + IP + ":" + Port + "/?method" + "="+ method;
        StartCoroutine(SendRequest(url, resultCallback));
    }

    //添加序列号授权信息
    public void AddSN(string sn,string tel,string company,string deviceTpe,Action<string, string> resultCallback)
    {
        string method = "AddSN";
        string url = "http://" + IP + ":" + Port + "/?method" + "=" + method+"&sn="+sn+"&tel=" + tel + "&company=" + company + "&deviceType=" + deviceTpe;
        StartCoroutine(SendRequest(url, resultCallback));
    }
    //添加随机授权信息
    public void AddKey(string tel, string company, Action<string, string> resultCallback)
    {
        string method = "AddKey";
        string url = "http://" + IP + ":" + Port + "/?method" + "=" + method + "&tel=" + tel + "&company=" + company + "&deviceType=" + "HTC";
        StartCoroutine(SendRequest(url, resultCallback));
    }   
    //删除授权信息
    public void DelKey(string key, Action<string, string> resultCallback)
    {
        string method = "DelKey";
        string url = "http://" + IP + ":" + Port + "/?method" + "=" + method + "&key=" + key;
        StartCoroutine(SendRequest(url, resultCallback));
    }


    IEnumerator SendRequest(string url, Action<string,string> requestCallback)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Content-Type", "text/html;charset=utf-8");
        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            string error = "网络错误";
            requestCallback?.Invoke(null,error);
        }
        else {
            if (string.IsNullOrEmpty(request.error))
            {
                string result = request.downloadHandler.text;
                requestCallback?.Invoke(result, null);
            }
            else
            {
                string error = "Unknown Error";
                requestCallback?.Invoke(null,error);
            }
        }
    }
#endregion
}
