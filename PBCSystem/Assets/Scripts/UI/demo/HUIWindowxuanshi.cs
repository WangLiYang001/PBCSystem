using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUIWindowxuanshi : HUIBase 
{
    public  RawImage Image;
    Texture2D tempImage;
    public override void InitData(bool forceInit = false)
    {
        string texturePath = Application.streamingAssetsPath + "/入党宣誓/" + "入党誓词.png";
        string filePath = "file://" + texturePath;
        WWW www = new WWW(filePath);
        if (www.error != null)
        {
            Debug.LogError(filePath + www.error);
            Image.gameObject.SetActive(false);
        }
        else
        {
            Image.gameObject.SetActive(true);
            tempImage = www.texture;
            Image.texture = tempImage;
        }
    }
    public void Return()
    {
        HUITipManager.Instance.PlayExit();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Main, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
}
