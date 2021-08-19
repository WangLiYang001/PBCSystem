using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
public class HDleteEvent : HUIBase
{   
    public List<Button> buttons = new List<Button>();
     void Start()
     {
        AddClickEvents();
      }
void AddClickEvents()
{
    int x = 0;
    foreach (Button item in buttons)
    {
        int y = x;
        item.onClick.AddListener(() => ClickEvent2(y));//此处用的第二种点击方法
        x++;
    }
}

void ClickEvent2(int a)
{
    //通过判断点击按钮的名字调用相应的方法
    switch (buttons[a].name)
    {
            case "Button"://这里的Button1是指场景中,按钮的名字
                ClickButton();
                break;
            default:
            break;
    }
}
   
   void ClickButton()
    {
       
    }
}
