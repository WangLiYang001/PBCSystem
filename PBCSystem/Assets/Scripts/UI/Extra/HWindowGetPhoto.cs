using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HWindowGetPhoto : MonoBehaviour
{
    public GameObject _storeObj;
    public GameObject _content;
    public Text _showName;
    public Text _shouNum;
    public GameObject _gameObject;
    public Button _getImage;
    public Image _image;
    string _path;
    int n;
    int s;
    List<Texture2D> allTex2d = new List<Texture2D>();
    public void Start()
    {
        _showName.text = _gameObject.name.ToString();
        _path = Application.streamingAssetsPath + "/党员活动/活动相册/" + _showName.text;
        List<string> filePaths = new List<string>();
        string imgtype = "*.BMP|*.JPG|*.GIF|*.PNG|*.MP4";
        string[] ImageType = imgtype.Split('|');
        for (int i = 0; i < ImageType.Length; i++)
        {
            string[] dirs = Directory.GetFiles((_path), ImageType[i]);
            for (int j = 0; j < dirs.Length; j++)
            {
                filePaths.Add(dirs[j]);
            }
        }
        for (int i = 0; i < filePaths.Count; i++)
        {
            Texture2D tx = new Texture2D(100, 100);
            tx.LoadImage(getImageByte(filePaths[i]));
            allTex2d.Add(tx);
            _shouNum.text = "";
            _shouNum.text = (i + 1).ToString() + "张";
            n = i + 1;

        }
        _showName.text = _gameObject.name.ToString();      
    }
    public void ClickUI()
    {
        _showName.text = _gameObject.name.ToString();
        _path = Application.streamingAssetsPath + "/党员活动/活动相册/" + _showName.text;
        Loadfile();
        _storeObj.SetActive(false);
    }
    void Loadfile()
    {
        for (int i = 0; i <n; i++)
        {
            GameObject temp = Instantiate(_storeObj, _storeObj.transform.position, Quaternion.identity);
            temp.GetComponent<Transform>().SetParent(_content.transform);
            Sprite sprite = Sprite.Create(allTex2d[i], new Rect(0, 0, allTex2d[i].width, allTex2d[i].height), new Vector2(0.2f, 0.2f));
            temp.GetComponent<Image>().sprite = sprite;
            Sprite sprite1 = Sprite.Create(allTex2d[0], new Rect(0, 0, allTex2d[0].width, allTex2d[0].height), new Vector2(0.2f, 0.2f));
            _getImage.GetComponent<Image>().sprite = sprite1;
            temp.transform.name = "Element" + i;
        }
       
    }
 
    private static byte[] getImageByte(string imagePath)
    {
        FileStream files = new FileStream(imagePath, FileMode.Open);
        byte[] imgByte = new byte[files.Length];
        files.Read(imgByte, 0, imgByte.Length);
        files.Close();
        return imgByte;
    }
}
