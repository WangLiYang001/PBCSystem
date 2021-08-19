using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HQueryAllInfoCtrller : MonoBehaviour
{
    private GameObject DataManager;
    private GameObject UIWindowChakan;
    
    void Start()
    {
        UIWindowChakan = GameObject.Find("DataManager").GetComponent<GameObject >();


    }
}
