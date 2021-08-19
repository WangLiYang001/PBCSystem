using SimpleWebBrowser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUIWindowLilun : HUIBase 
{
    public GameObject _Browser;
    private string _url;
    public void Exit()
    {
        HUITipManager.Instance.PlayExit();
        Shutdown();
        HUIManager.Instance.Back(_windowPath);
    }

    public void OnEnable()
    {
        string _path = Application.streamingAssetsPath + "/" + "党建网址/党的思想网址/网址.txt";
        WWW www = new WWW(_path);
        _url = www.text;
        WebBrowser2D webBrowser = _Browser.GetComponent<WebBrowser2D>();
        if (webBrowser == null)
            webBrowser = _Browser.AddComponent<WebBrowser2D>();
        webBrowser.InitEngineNew(1920, 1080, "MainSharedMem", 9000, _url, false);

        //UnityEngine.Debug.Log("webBrowser.InitEngineNew OnEnable");
    }


    public void Shutdown()
    {
        //UnityEngine.Debug.Log("webBrowser.InitEngineNew OnDisable");
        WebBrowser2D webBrowser = _Browser.GetComponent<WebBrowser2D>();
        webBrowser.Shutdown();
        // Destroy(webBrowser);
    }
}
