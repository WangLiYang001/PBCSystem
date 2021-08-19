using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUIWindowShizheng : HUIBase
{
    Texture2D tempImage;
    RawImage image;
    public List<Button> buttons = new List<Button>();
    public override void InitData(bool forceInit = false)
    {
        LoadImage();
    }
    void LoadImage()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            image = buttons[i].GetComponent<RawImage>();
            string name = buttons[i].name.ToString();
            string texturePath = Application.streamingAssetsPath + "/时政新闻图片/" + name + ".png";
            string filePath = "file://" + texturePath;
            WWW www = new WWW(filePath);
            if (www.error != null)
            {
                Debug.LogError(filePath + www.error);
                image.gameObject.SetActive(false);
            }
            else
            {
                image.gameObject.SetActive(true);
                tempImage = www.texture;
                image.texture = tempImage;
            }
        }
    }
    public void Xinwen()
    {
        HUITipManager.Instance.PlayQue();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Xinwen, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void Renmin()
    {
        HUITipManager.Instance.PlayQue();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Renmin, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void Qiangguo()
    {
        HUITipManager.Instance.PlayQue();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Qiangguo, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void Qiushi()
    {
        HUITipManager.Instance.PlayQue();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Qiushi, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }

    public void Exit()
    {
        HUITipManager.Instance.PlayExit();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Main, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
}
