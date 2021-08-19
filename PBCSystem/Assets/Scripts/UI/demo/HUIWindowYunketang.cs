using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HUIWindowYunketang : HUIBase 
{
    public GameObject _object;
    public Text _num;
    public override void InitData(bool forceInit = false)
    {
        GetFiles();
    }
    public void GetFiles()
    {
        string path = Application.streamingAssetsPath + "/云课堂";
        string[] dirs = Directory.GetDirectories(@path, "*");
        for (int i = 0; i < dirs.Length; i++)
        {
            if (i > 6)
            {
                _num.text = dirs.Length.ToString() + "个文件";
                string filePath = dirs[i];
                string fileName = Path.GetFileName(dirs[i]);
                GameObject go = Instantiate(_object);
                go.name = fileName;
                go.GetComponent<RectTransform>().SetParent(_object.transform.parent);
                go.GetComponent<RectTransform>().localPosition = new Vector3(250 * (i-7), -250, 0);
                go.GetComponent<RectTransform>().localScale = Vector3.one;
            }
            else
            {
                _num.text = dirs.Length.ToString() + "个";
                string filePath = dirs[i];
                string fileName = Path.GetFileName(dirs[i]);
                GameObject go = Instantiate(_object);
                go.name = fileName;
                go.GetComponent<RectTransform>().SetParent(_object.transform.parent);
                go.GetComponent<RectTransform>().localPosition = new Vector3(250 * i, 0, 0);
                go.GetComponent<RectTransform>().localScale = Vector3.one;
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
}
