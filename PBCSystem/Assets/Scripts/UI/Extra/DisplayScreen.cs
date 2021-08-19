using UnityEngine;

public class DisplayScreen : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 90;
        for (int i = 0; i < Display.displays.Length; i++)
        {
            Display display = Display.displays[i];
            display.Activate(display.systemWidth, display.systemHeight,60);
            display.SetRenderingResolution(display.systemWidth, display.systemHeight);
        }

        Screen.fullScreen = true;

    }
}