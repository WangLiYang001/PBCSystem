using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HDrop_select : MonoBehaviour
{
  
        Dropdown dpn;

        void Start()
        {
            Dropdown.OptionData data1 = new Dropdown.OptionData();
            data1.text = "男";
            Dropdown.OptionData data2 = new Dropdown.OptionData();
            data2.text = "女";
            dpn = transform.GetComponent<Dropdown>();
            dpn.options.Add(data1);
            dpn.options.Add(data2);
        }
}

