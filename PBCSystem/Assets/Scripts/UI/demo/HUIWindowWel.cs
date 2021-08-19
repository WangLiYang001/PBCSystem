using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class HUIWindowWel : HUIBase
{
    public Text _text;
    public Button _bianji;
    public InputField _input;
    public Button _sure;
    string path;
    public override void InitData(bool forceInit = false)
    {
        _bianji.gameObject.SetActive(false);
        _input.gameObject.SetActive(false);
        _sure.gameObject.SetActive(false);
        if (HLoginDataManager.Instance.IsAdmin)
        {
            _bianji.gameObject.SetActive(true);
        }
        path = Application.streamingAssetsPath + "/欢迎界面/欢迎界面内容.txt";
        WWW www = new WWW(path);
        _text.text = www.text;

    }
    public void Return()
    {
        HUITipManager.Instance.PlayExit();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Main, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void Click()
    {
        _input.gameObject.SetActive(true);
        _sure.gameObject.SetActive(true);
    }
    public void ClickSure()
    {
        if (string.IsNullOrEmpty(_input.text)==false)
        {
            _text.text = _input.text;
            StreamWriter sw;
            FileInfo fi = new FileInfo(path);
            sw = fi.CreateText();      
            sw.WriteLine(_input.text);
            sw.Close();
            sw.Dispose();
        }
        _input.gameObject.SetActive(false);
        _sure.gameObject.SetActive(false);
    }
}
