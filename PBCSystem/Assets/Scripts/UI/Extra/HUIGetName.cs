using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUIGetName : MonoBehaviour
{
    public Text _showName;
    public Button _button;
    public Text _shouNum;
    RawImage image;
    Texture2D tempImage;
    public GameObject _gameObject;
    private void Start()
    {
        _showName.text = _gameObject.name.ToString();
        string texturePath = Application.streamingAssetsPath + "/云课堂图片/" + _showName.text + ".png";
        string filePath = "file://" + texturePath;
        WWW www = new WWW(filePath);
        image = _button.GetComponent<RawImage>();
        if (www.error != null)
        {
            image.gameObject.SetActive(false);
        }
        else
        {
            image.gameObject.SetActive(true);
            tempImage = www.texture;
            image.texture = tempImage;
        }
    }
    public void Click()
    {
        HUITipManager.Instance.PlayQue();
        if (_showName.text=="“不忘初心 牢记使命”主题教育")
        {
            HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Yunketangsp, finish: (selfWindow) => {
                selfWindow.InitData();

            });
        }
        else if(_showName.text == "《中国共产党纪律处分条例》解读")
        {
            HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Yunketangsp1, finish: (selfWindow) => {
                selfWindow.InitData();

            });
        }
        else if (_showName.text == "党支部组织生活制度")
        {
            HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Yunketangsp2, finish: (selfWindow) => {
                selfWindow.InitData();

            });
        }
        else if (_showName.text == "改革开放发展史")
        {
            HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Yunketangsp3, finish: (selfWindow) => {
                selfWindow.InitData();

            });
        }
        else if (_showName.text == "新时代共产党员的党性修养")
        {
            HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Yunketangsp4, finish: (selfWindow) => {
                selfWindow.InitData();

            });
        }
        else if (_showName.text == "社会主义发展史")
        {
            HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Yunketangsp5, finish: (selfWindow) => {
                selfWindow.InitData();

            });
        }
        else if (_showName.text == "跟习近平总书记学党史")
        {
            HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Yunketangsp6, finish: (selfWindow) => {
                selfWindow.InitData();

            });
        }
        else if (_showName.text == "跟习近平总书记学新中国史")
        {
            HUIManager.Instance.OpenUI(HUIWindowDefine.Window_Yunketangsp7, finish: (selfWindow) => {
                selfWindow.InitData();

            });
        }
    }
}
