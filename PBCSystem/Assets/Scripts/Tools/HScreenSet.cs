using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HScreenSet : MonoBehaviour {

    private void OnEnable()
    {
        Resolution[] resolutions = Screen.resolutions;//获取设置当前屏幕分辩率
        Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);//设置当前分辨率
        Screen.fullScreen = true;  //设置成全屏
    }

}
