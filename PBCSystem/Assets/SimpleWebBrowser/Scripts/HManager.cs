using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HManager : MonoBehaviour
{
    public GameObject _gameObject;
    Vector2 _lastPos;//鼠标上次位置
    Vector2 _currPos;//鼠标当前位置
    Vector2 _offset;//两次位置的偏移值
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            _currPos = Input.mousePosition;
            _offset = _currPos - _lastPos;
            _lastPos = _currPos;
        }
        if (_offset.y != 0)
        {
            _gameObject.SetActive(true);
        }
      
    }
    private void FixedUpdate()
    {
        //_gameObject.SetActive(false);
    }

}
