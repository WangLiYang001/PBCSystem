using SimpleWebBrowser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMove : MonoBehaviour
{
    public WebBrowser2D _webBrowser;
    bool first = true;
    bool move = false;
    void Update()
    {
        if (_webBrowser == null)
        {
            _webBrowser = gameObject.transform.parent.GetComponent<WebBrowser2D>();
            return;
        }
    }
    public void OnPointerClick()
    {
        if (_webBrowser == null)
            return;
        if (move) return;
        Debug.Log("OnPointerClick");
        _webBrowser.ClickBrowser();
     
    }
    public void OnPointUp()
    {
        if (_webBrowser == null)
            return;
        if (!move) return;
        Debug.Log("OnPointUp");
        _webBrowser.DragBrowser(first, true);
        first = true;
        move = false;
    }

    public void OnPointerMove()
    {
        if (_webBrowser == null)
            return;
        move = true;
        Debug.Log("OnPointerMove");
        _webBrowser.DragBrowser(first, false);
        first = false;
    }
}

