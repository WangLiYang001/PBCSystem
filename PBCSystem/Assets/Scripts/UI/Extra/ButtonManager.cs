using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private AudioSource _music;
    private AudioClip _audioClip;

    private void Awake()
    {
        _music = gameObject.AddComponent<AudioSource>();
        _music.playOnAwake = false;
        _audioClip = Resources.Load<AudioClip>("music/1");
        _music.clip = _audioClip;
    }
    public void Play()
    {
    
    }
}
