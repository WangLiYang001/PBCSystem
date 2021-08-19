using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HUIWindowZuzhigz : HUIBase
{
    public Text title;
    public Text title1;
    public Text title2;
    public Text title3;
    public Text text;
    public GameObject _btn;
    public List<GameObject> _objects = new List<GameObject>();
    public List<GameObject> _object = new List<GameObject>();
    public List<Text> texts;
    string[] fliesName;
    string path;
    string fileName;
    Texture2D tempImage;
    public RawImage image;
    public override void InitData(bool forceInit = false)
    {
        GetFileName();
        string path = "/组织结构/组织结构/标题栏.txt";
        string localPath = Application.streamingAssetsPath + path;
        WWW www = new WWW(localPath);
        title.text = www.text;
        string path1 = "/组织结构/组织结构/组织结构1.txt";
        string localPath1 = Application.streamingAssetsPath + path1;
        WWW www1 = new WWW(localPath1);
        title1.text = www1.text;
        string path2 = "/组织结构/组织结构/组织结构2.txt";
        string localPath2 = Application.streamingAssetsPath + path2;
        WWW www2 = new WWW(localPath2);
        title2.text = www2.text;
        string path3 = "/组织结构/组织结构/组织简介.txt";
        string localPath3 = Application.streamingAssetsPath + path3;
        WWW www3 = new WWW(localPath3);
        title3.text = www3.text;
    }
    //private void Start()
    //{
    //    string texturePath = Application.streamingAssetsPath + "/组织结构/"  + "组织结构.png";
    //    string filePath = "file://" + texturePath;
    //    WWW www = new WWW(filePath);
    //    if (www.error != null)
    //    {
    //        Debug.LogError(filePath + www.error);
    //        image.gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        image.gameObject.SetActive(true);
    //        tempImage = www.texture;
    //        image.texture = tempImage;
    //    }
    
    //}
    public void GetFileName()
    { 
        string fullPath = Application.streamingAssetsPath + "/" + "组织架构";
        string[] dirs = Directory.GetDirectories(@fullPath, "*");
        if (Directory.Exists(fullPath))
        {           
                string filePath = dirs[0];
                fileName = Path.GetFileName(dirs[0]);
                text.text = fileName;          
        }
        path = Application.streamingAssetsPath + "/" + "组织架构/" + fileName;
        string[] dirs1 = Directory.GetDirectories(@path, "*");
        if (Directory.Exists(path))
        {
            for (int j = 0; j < _objects.Count; j++)
            {
                _objects[j].gameObject.SetActive(false);
            }
            for (int i = 0; i < dirs1.Length; i++)
            {           
                string filePath = dirs1[i];
                texts[i].text = Path.GetFileName(dirs1[i]);
                _objects[i].gameObject.SetActive(true);
                string path1 = path + "/" + texts[i].text;
                if (Directory.Exists(path1))
                {
                    DirectoryInfo direction1 = new DirectoryInfo(path1);
                    FileInfo[] files1 = direction1.GetFiles();
                    for (int j = 0; j < files1.Length; j++)
                    {
                        _objects[j].gameObject.SetActive(true);
                        if (files1.Length != 0)
                        {
                            if (files1[j].Name.EndsWith(".txt"))
                            {
                                _object[i].SetActive(true);
                            }
                        }
                    }
                }
            }
        }    
    }
    public void Click()
    {
     
        text = _btn.transform.Find("Text").GetComponent<Text>();
        var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        string txt=button.GetComponentInChildren<Text>().text;
        string path1 = path+"/"+txt;
        if (Directory.Exists(path1))
        {
            DirectoryInfo direction1 = new DirectoryInfo(path1);
            FileInfo[] files1 = direction1.GetFiles();
            for (int j = 0; j < files1.Length; j++)
            {
                _objects[j].gameObject.SetActive(false);
            }
            for (int j = 0; j < files1.Length; j++)
            {
                _objects[j].gameObject.SetActive(false);
                if (files1.Length != 0)
                {
                  
                    if (files1[j].Name.EndsWith(".txt"))
                    {

                        string _path = path1 + "/" + files1[j].Name;
                        WWW www = new WWW(_path);
                        string[] flies = www.text.Split(',');
                        for(int i = 0; i < texts.Count; i++)
                        {
                            texts[i].text = flies[i];
                            _object[i].SetActive(false);
                            _objects[i].gameObject.SetActive(true);
                        }
                        if (string.IsNullOrEmpty(texts[j].text))
                        {
                            _objects[j].gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        string[] fliesName1 = files1[j].Name.Split('.');
                        texts[j].text = fliesName1[0];
                        if (string.IsNullOrEmpty(texts[j].text) == false)
                        {
                            _objects[j].gameObject.SetActive(true);
                        }
                    }
                }
               
            }
        }
    }
    public void Return()
    {
        HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Main, finish: (selfWindow) => {
            selfWindow.InitData();
        });
    }
}
