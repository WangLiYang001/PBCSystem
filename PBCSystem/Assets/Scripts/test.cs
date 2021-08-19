using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void Start()
    {
        //System.Diagnostics.Process.Start("https://www.baidu.com/");
        Application.OpenURL("http://www.baidu.com");
    }
}
