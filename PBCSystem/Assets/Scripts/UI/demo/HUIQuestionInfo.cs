using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUIQuestionInfo : MonoBehaviour
{
    public GameObject _image;
    public GameObject _selectImage;
    public Text _text;
    public Text _selectText;

    public Color _defaultColor;
    public Color _rightColor;
    private HUIWindowDyjy _parentUI;

    private string _ID;
    public string ID
    {
        get { return _ID; }
    }
    private bool _bSelect = false;
    public bool BSelect
    {
        get { return _bSelect; }
        set { _bSelect = value; }
    }
    // Start is called before the first frame update
    public void Init(HUIWindowDyjy parent,string id,string strText,string strSelText)
    {
        _ID = id;
        _parentUI = parent;
        _bSelect = false;
        _defaultColor = Color.black;
        _rightColor = Color.white;
        _text.text = strText;
        _selectText.text = strSelText;
        NoSelectUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectUI()
    {
        _bSelect = true;
        _image.SetActive(true);
        _selectImage.SetActive(true);
        _text.color = _rightColor;
        _selectText.color = _rightColor;
    }

    public void NoSelectUI()
    {
        _bSelect = false;
        _image.SetActive(true);
        _selectImage.SetActive(false);
        _text.color = _defaultColor;
        _selectText.color = _defaultColor;
    }

    public void OnBtnClick()
    {
        _parentUI.OnBtnClick(gameObject);
    }

}
