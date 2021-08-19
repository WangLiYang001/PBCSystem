using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HDelMasage
{
    public Ecode _resultCode;
}
     public enum Ecode
     {
        UnKnown=9000,
        Success=1000,
        KeyNotExist=1001,
        KeyExist=1002
     }
