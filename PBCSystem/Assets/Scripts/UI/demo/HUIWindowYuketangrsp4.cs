using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HUIWindowYuketangrsp4 : HUIBase
{
    DateTime resumeT;
    public override void InitData(bool forceInit = false)
    {
        resumeT = DateTime.Now;
    }
    public void ClickExit()
    {
        DateTime resume = DateTime.Now;
        TimeSpan ts1 = new TimeSpan(resume.Ticks);
        TimeSpan ts2 = new TimeSpan(resumeT.Ticks);
        TimeSpan tsSub = ts1.Subtract(ts2).Duration();
        if (tsSub.Minutes > 3)
        {
            bool isAdd = true;
            string path = Application.streamingAssetsPath + "/积分系统/积分系统得分.txt";
            string content = File.ReadAllText(path);
            string[] flies = content.Split('.');
            string str = flies[3];
            string[] str1 = str.Split(':');
            string str2 = str1[1];
            int _grade = int.Parse(str2);
            HLoginDataManager.Instance.SetGrade(_grade, isAdd);
        }
        HUITipManager.Instance.PlayExit();
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Yunketangsp4, finish: (selfWindow) => {
            selfWindow.InitData();

        });
    }
}
