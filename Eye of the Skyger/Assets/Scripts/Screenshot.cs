using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ScreenCapture.CaptureScreenshot("SKEYE_Screenshot_" + PlayerPrefs.GetInt("ScreenshotCount",0) + ".png", 2);
            PlayerPrefs.SetInt("ScreenshotCount", PlayerPrefs.GetInt("ScreenshotCount", 0) + 1);
            Debug.Log(Application.persistentDataPath);
        }
    }
}
