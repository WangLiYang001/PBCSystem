using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using System.IO;

[RequireComponent(typeof(MediaPlayer))]
public class HWindowReload : MonoBehaviour
{
    MediaPlayer _mediaPlayer;
    public string _Path;
    MediaPlayer.FileLocation fileLocation= MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder;
    private void OnEnable()
    {
        if (_mediaPlayer == null)
        {
            _mediaPlayer = GetComponent<MediaPlayer>();
        }        
        _mediaPlayer.OpenVideoFromFile(fileLocation,_Path);
        _mediaPlayer.Play();
    }
}
 