using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUITipManager : MonoBehaviour {

    public Text _tip;
    public AudioSource _audiosource;
    public AudioSource _audioRight;
    public AudioSource _audioFalse;
    public AudioSource _audioExit;
    public AudioSource _audioQue;
    public GameObject _gameObj;
    public GameObject _gameObj1;
    public GameObject _showFPS;
    private CanvasGroup canvasGroup;
    private float _startTime;
    private static HUITipManager _instance;
    private float _controllerTime = -1;
    private bool _dontHide = false;
    public static HUITipManager Instance
    {
        get {
            if (_instance == null)
            {
                _instance = FindObjectOfType<HUITipManager>();
            }
            if (_instance == null)
            {
                GameObject tipObj = Instantiate(Resources.Load("UI/TipCanvas")) as GameObject;
                _instance = tipObj.GetComponent<HUITipManager>();
            }
            return _instance;
        }
    }

    private void OnEnable()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponentInChildren<CanvasGroup>();
        _startTime = Time.realtimeSinceStartup;
    }
    public void Play()
    {
        _audiosource.Play();
    }
    public void ShowTip(string content)
    {
        _tip.gameObject.SetActive(true);
        _tip.text = "提示:" + content;
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1;
            _startTime = Time.realtimeSinceStartup;
            if (!canvasGroup.blocksRaycasts)
                canvasGroup.blocksRaycasts = true;
        }
    }
    public void ShowFPS()
    {
        _showFPS.SetActive(true);
    }
    public void ShowImageOK( )
    {
        _audioRight.Play();
        _gameObj.gameObject.SetActive(true);
        _gameObj1.gameObject.SetActive(false);
        _tip.gameObject.SetActive(false);
        StartCoroutine(Wait1(2f));
    }
    public void ShowImageNO( )
    {
        _audioFalse.Play();
        _gameObj.gameObject.SetActive(false);
        _gameObj1.gameObject.SetActive(true);
        _tip.gameObject.SetActive(false);
        StartCoroutine(Wait(2f));
    }
    public void PlayExit()
    {
        _audioExit.Play();
    }
    public void PlayQue()
    {
        _audioQue.Play();
    }
    IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);
        _gameObj1.gameObject.SetActive(false);
    }
    IEnumerator Wait1(float t)
    {
        yield return new WaitForSeconds(t);
        _gameObj.gameObject.SetActive(false);
    }
    public void ShowTip(string content,float controllerTime)
    {
        _tip.text = "提示:" + content;
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1;
            _startTime = Time.realtimeSinceStartup;
            _controllerTime = controllerTime;
            if (!canvasGroup.blocksRaycasts)
                canvasGroup.blocksRaycasts = true;
        }
    }
    public void ShowTip(string content, bool noHide)
    {
        _tip.text = "提示:" + content;
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1;
            _startTime = Time.realtimeSinceStartup;
            _dontHide = noHide;
            if (!canvasGroup.blocksRaycasts)
                canvasGroup.blocksRaycasts = true;
        }
    }

    public void ForceHideTip()
    {
        _dontHide = false;
        canvasGroup.alpha = 0;
        if (canvasGroup.blocksRaycasts)
            canvasGroup.blocksRaycasts = false;
    }

    public void ShowTip(string content,Color contentColor)
    {
        _tip.color = contentColor;
        _tip.text = "提示:" + content;
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1;
            _startTime = Time.realtimeSinceStartup;
        }
    }

    private void FixedUpdate()
    {
        if (_dontHide) return;
        if (canvasGroup != null && canvasGroup.alpha > 0)
        {
          
            if (_controllerTime < 0)
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, 1f / 1f * (Time.realtimeSinceStartup - _startTime));
            else
            {
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, 1f / _controllerTime * (Time.realtimeSinceStartup - _startTime));
            }
        }
        else if (canvasGroup.alpha == 0f)
        {
            if (_controllerTime > 0f)
                _controllerTime = -1;
            if (canvasGroup.blocksRaycasts)
                canvasGroup.blocksRaycasts = false;
        }
    }

}
