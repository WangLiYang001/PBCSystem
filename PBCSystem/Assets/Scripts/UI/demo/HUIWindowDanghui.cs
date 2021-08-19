using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Paroxe.PdfRenderer;
using System.IO;

public class HUIWindowDanghui : HUIBase
{
    Texture2D tempImage;
    RawImage image;
    public List<Button> buttons = new List<Button>();
    public List<GameObject> _objects = new List<GameObject>();
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
            string texturePath = Application.streamingAssetsPath + "/党规党章图片/" + name + ".png";
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
    public void Exit()
    {
        HUITipManager.Instance.PlayExit();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Main, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void Zhangcheng()
    {
        HUITipManager.Instance.PlayQue();
        _objects[0].gameObject.SetActive(true);
    }
    public void Jiandu()
    {
        HUITipManager.Instance.PlayQue();
        _objects[1].gameObject.SetActive(true);
    }
    public void Xunshi()
    {
        HUITipManager.Instance.PlayQue();
        _objects[2].gameObject.SetActive(true);
    }
    public void Chufen()
    {
        HUITipManager.Instance.PlayQue();
        _objects[3].gameObject.SetActive(true);
    }
    public void Jilv()
    {
        HUITipManager.Instance.PlayQue();
        _objects[4].gameObject.SetActive(true);
    }
    public void Shenli()
    {
        HUITipManager.Instance.PlayQue();
        _objects[5].gameObject.SetActive(true);
    }
}
