using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class HUtil : HSingleTon<HUtil> {

    public static T NewGameObject<T>(GameObject cloneOrigin, Transform parent, Vector3 localPos, Vector3 localDir, bool defaultActive = true) where T : MonoBehaviour
    {
        cloneOrigin.SetActive(defaultActive);
        GameObject gameObject = GameObject.Instantiate(cloneOrigin, parent) ;
        gameObject.SetActive(defaultActive);
        gameObject.transform.localPosition = localPos;
        gameObject.transform.localEulerAngles = localDir;
        T t = gameObject.GetComponent<T>();
        return t;
    }

    public static T NewGameObject<T>(GameObject cloneOrigin, Transform parent,bool defaultActive = true) where T : MonoBehaviour
    {
        cloneOrigin.SetActive(defaultActive);
        GameObject gameObject = GameObject.Instantiate(cloneOrigin, parent);
        gameObject.SetActive(defaultActive);
        T t = gameObject.GetComponent<T>();
        return t;
    }

    public static GameObject NewGameObject(GameObject cloneOrigin, Transform parent, bool defaultActive = true)
    {
        cloneOrigin.SetActive(false);
        GameObject gameObject = GameObject.Instantiate(cloneOrigin, parent);
        gameObject.SetActive(defaultActive);
        return gameObject;
    }
    public static T NewGameObject<T>(Transform parent,Vector3 localPos, Vector3 localDir, bool defaultActive = true) where T : MonoBehaviour
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.parent = parent;
        gameObject.SetActive(defaultActive);
        gameObject.transform.localPosition = localPos;
        gameObject.transform.localEulerAngles = localDir;
        T t = gameObject.AddComponent<T>();
        return t;
    }

    /// <summary>  
    /// Base64编码  
    /// </summary>  
    public string Base64Encode(string message)
    {
        byte[] bytes = System.Text.Encoding.GetEncoding("utf-8").GetBytes(message);
        return System.Convert.ToBase64String(bytes);
    }

    /// <summary>  
    /// Base64解码  
    /// </summary>  
    public string Base64Decode(string message)
    {
        byte[] bytes = System.Convert.FromBase64String(message);
        return System.Text.Encoding.GetEncoding("utf-8").GetString(bytes);
    }


    public string GetTime(string timeStamp)
    {
        //处理字符串,截取括号内的数字
        var strStamp = Regex.Matches(timeStamp, @"(?<=\()((?<gp>\()|(?<-gp>\))|[^()]+)*(?(gp)(?!))").Cast<Match>().Select(t => t.Value).ToArray()[0].ToString();
        //处理字符串获取+号前面的数字
        var str = Convert.ToInt64(strStamp.Substring(0, strStamp.IndexOf("+")));
        long timeTricks = new DateTime(1970, 1, 1).Ticks + str * 10000 + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours * 3600 * (long)10000000;
        return new DateTime(timeTricks).ToString("yyyy-MM-dd");

    }

    public string GetDigest(string content)
    {
        var newStr = Regex.Replace(content, @"[^0-9]+", "");
        return newStr;
    }

    public class HFile
    {
        public static void Save(string content, string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
            {
              
                Directory.CreateDirectory(fileInfo.Directory.FullName);
            }
            File.WriteAllText(path, content);
        }

        public static void SaveJsonObject(object content, string path)
        {
            string s=  JsonUtility.ToJson(content);
            Save(s, path);
        }

        public static void OpenDirectory(string path, bool isFile = false)
        {
            if (string.IsNullOrEmpty(path)) return;
            path = path.Replace("/", "\\");
            if (isFile)
            {
                if (!File.Exists(path))
                {
                    Debug.LogError("No File: " + path);
                    return;
                }
                path = string.Format("/Select, {0}", path);
            }
            else
            {
                if (!Directory.Exists(path))
                {
                    Debug.LogError("No Directory: " + path);
                    return;
                }
            }
            //可能360不信任
            System.Diagnostics.Process.Start("explorer.exe", path);
        }

    }


}
