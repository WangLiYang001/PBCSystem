using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HUIWindow_Gsjs : HUIBase
{
    public GameObject _pdfPlayer;
    public GameObject _moviePlayer;
    public override void InitData(bool forceInit = false)
    {
        string fullPath = Application.streamingAssetsPath + "/" + "企业介绍" + "/";  //路径

        //获取指定路径下面的所有资源文件  
        if (Directory.Exists(fullPath))
        {
            DirectoryInfo direction = new DirectoryInfo(fullPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".MP4"))
                {
                    _pdfPlayer.SetActive(false);
                }
                else
                {
                    _pdfPlayer.SetActive(true);
                }

            }
        }
    }
    public void ClickExit()
    {
        HUITipManager.Instance.PlayExit();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Main, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
}
