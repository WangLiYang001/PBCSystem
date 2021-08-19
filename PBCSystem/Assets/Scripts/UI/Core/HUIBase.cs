using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUIBase : MonoBehaviour {

    public bool _log = false;
    [HideInInspector]
    public string _windowPath;
    public bool _hasTop = false;
    public bool _hasBottom = false;

    protected bool _hasInitData = false;
    protected bool _hasInitUI = false;

    public virtual void InitData(bool forceInit = false)
    { }

    public virtual void Back() { }

    public virtual void Init(bool forceInit = false) { }

    public void BringToFront() {
        transform.SetAsLastSibling();
    }
    public void BringToBack()
    {
        transform.SetAsFirstSibling();
    }

    public void ResetUIState()
    {
        _hasInitData = false;
        _hasInitUI = false;
    }

}
