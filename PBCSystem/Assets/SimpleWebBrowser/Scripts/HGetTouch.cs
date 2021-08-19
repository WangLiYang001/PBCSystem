using UnityEngine;
using UnityEngine.UI;
using SimpleWebBrowser;
using UnityEngine.EventSystems;

public class HGetTouch : MonoBehaviour
{
    public WebBrowser2D _webBrowser;
    Vector2 _lastPos;//鼠标上次位置
    Vector2 _currPos;//鼠标当前位置
    Vector2 _offset;//两次位置的偏移值
    void Update()
    {
        if(_webBrowser == null)
        {
            _webBrowser = gameObject.transform.parent.GetComponent<WebBrowser2D>();
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _lastPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            _currPos = Input.mousePosition;
            _offset = _currPos - _lastPos;
            _lastPos = _currPos;
            if (Mathf.Abs(_offset.y) > 5f)
            {
                move = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            targetY = _offset.y;
            smooth = true;
        }
    }
    float currentVelocity;
    float targetY;
    bool smooth = false;
    bool move = false;
    private void LateUpdate()
    {
        if (_webBrowser == null)
            return;

        if (smooth)
        {
            targetY = Mathf.SmoothDamp(targetY, 0f, ref currentVelocity, 0.25f);
            _webBrowser.GetOffset((int)targetY);
            if (Mathf.Abs(targetY - 0.01f) <= 1f)
            {
                targetY = 0;
                smooth = false;
                _offset.y = 0f;
            }
        }
        else
        {
            _webBrowser.GetOffset((int)_offset.y);
        }
    }
    public void OnPointerUpXXX()
    {
        if (_webBrowser == null)
            return;
        if (!move)
        {
            _webBrowser.ClickBrowser();
        }
        move = false;
    }
}
