using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Paroxe.PdfRenderer;
using System.IO;

public class HUIWindowXxzl : HUIBase
{
    Texture2D tempImage;
    RawImage image;
    public List<PDFViewer> _pdfViewer = new List<PDFViewer>();
    public List<Button> buttons = new List<Button>();
    public List<GameObject> _objects = new List<GameObject>();
    public override void InitData(bool forceInit = false)
    {
    
    }
    public void Exit()
    {
        HUITipManager.Instance.PlayExit();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Main, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
    public void Click0()
    {
        HUITipManager.Instance.PlayQue();
        _objects[0].gameObject.SetActive(true);
    }
    public void Click1()
    {
        HUITipManager.Instance.PlayQue();
        _objects[1].gameObject.SetActive(true);
    }
    public void Click2()
    {
        HUITipManager.Instance.PlayQue();
        _objects[2].gameObject.SetActive(true);
    }
    public void Click3()
    {
        HUITipManager.Instance.PlayQue();
        _objects[3].gameObject.SetActive(true);
    }
    public void Click4()
    {
        HUITipManager.Instance.PlayQue();
        _objects[4].gameObject.SetActive(true);
    }
    public void Click5()
    {
        HUITipManager.Instance.PlayQue();
        _objects[5].gameObject.SetActive(true);
    }
    public void Click6()
    {
        HUITipManager.Instance.PlayQue();
        _objects[6].gameObject.SetActive(true);
    }
    public void Click7()
    {
        HUITipManager.Instance.PlayQue();
        _objects[7].gameObject.SetActive(true);
    }
    public void Click8()
    {
        HUITipManager.Instance.PlayQue();
        _objects[8].gameObject.SetActive(true);
    }
    public void Click9()
    {
        HUITipManager.Instance.PlayQue();
        _objects[9].gameObject.SetActive(true);
    }
    public void Click10()
    {
        HUITipManager.Instance.PlayQue();
        _objects[10].gameObject.SetActive(true);
    }
    public void Click11()
    {
        HUITipManager.Instance.PlayQue();
        _objects[11].gameObject.SetActive(true);
    }
    public void Click12()
    {
        HUITipManager.Instance.PlayQue();
        _objects[12].gameObject.SetActive(true);
    }
   
}

