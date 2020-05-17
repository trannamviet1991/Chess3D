using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton
{
    private Singleton()
    {
        //Class initialization goes here.
    }

    private static Singleton _instance;
    public static Singleton Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Singleton();
            return _instance;
        }
    }
}

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public Camera sideACam;
    public Camera sideBCam;

    public void ShowMainCamera()
    {
        mainCamera.enabled = true;
        sideACam.enabled = false;
        sideBCam.enabled = false;
    }

    public void ShowSideACamera()
    {
        mainCamera.enabled = false;
        sideACam.enabled = true;
        sideBCam.enabled = false;
    }

    public void ShowSideBCamera()
    {
        mainCamera.enabled = false;
        sideACam.enabled = false;
        sideBCam.enabled = true;
    }
}
