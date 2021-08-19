using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HClick : MonoBehaviour
{
    public GameObject _object;
    public Image _image;
    public GameObject _Object;
    List<Sprite> sprites = new List<Sprite>();
    public Text _curPage;
    private void OnEnable()
    {
        sprites.Clear();
        _object.gameObject.SetActive(false);
        _curPage.text = j + 1 + " / " + _Object.transform.childCount;
    }
    public void OnClick()
    {
        _object.gameObject.SetActive(true);
        var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Sprite sprite = button.GetComponent<Image>().sprite;
        _image.GetComponent<Image>().sprite = sprite;
        _curPage.text = j + 1 + " / " + _Object.transform.childCount + "张";
    }
    int j = 0;
    public void DownClick()
    {
        j = j + 1;
        for (int i = 0; i < _Object.transform.childCount; i++)
        {
            Sprite sprite1 = _Object.transform.GetChild(i).gameObject.GetComponent<Image>().sprite;
            sprites.Add(sprite1);
        }
        if(j<_Object.transform.childCount)
        {
            _image.GetComponent<Image>().sprite = sprites[j];
            _curPage.text = j+1 + " / " + _Object.transform.childCount + "张";
        }
        else
        {
            j=  j - 1;
            HUITipManager.Instance.ShowTip("这是最后一张了");
        }
       
    }
    public void UpClick()
    {
        j= j-1;
        for (int i = 0; i < _Object.transform.childCount; i++)
        {
            Sprite sprite1 = _Object.transform.GetChild(i).gameObject.GetComponent<Image>().sprite;
            sprites.Add(sprite1);
        }
        if (j>=0)
        {
            _curPage.text = j + 1 + " / " + _Object.transform.childCount + "张";
            _image.GetComponent<Image>().sprite = sprites[j];
        }
        else
        {
            j = j + 1;
            HUITipManager.Instance.ShowTip("这是第一张");
        }

    }
}
