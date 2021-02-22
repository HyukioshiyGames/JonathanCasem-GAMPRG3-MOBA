using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotaCamera : MonoBehaviour
{
    [Header("Camera")]
    public float cameraSpeed;
    public int guiSize;
    Rect rectDown,rectUp,rectLeft,rectRight;

    void Update()
    {
        rectDown = new Rect(0, 0, Screen.width, guiSize);
        rectUp = new Rect(0, Screen.height - guiSize, Screen.width, guiSize);

        rectLeft = new Rect(0, 0, guiSize, Screen.height);
        rectRight = new Rect(Screen.width - guiSize, 0, guiSize, Screen.height);

        if (rectDown.Contains(Input.mousePosition)) 
        {
            transform.Translate(0, 0, cameraSpeed, Space.World);
        }
        if (rectUp.Contains(Input.mousePosition)) 
        {
            transform.Translate(0, 0, -cameraSpeed, Space.World);
        }
        if (rectRight.Contains(Input.mousePosition)) 
        {
            transform.Translate(-cameraSpeed, 0, 0, Space.World);
        }
        if (rectLeft.Contains(Input.mousePosition)) 
        {
            transform.Translate(cameraSpeed, 0, 0, Space.World);
        }
    }
}
