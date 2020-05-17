using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropboxControl : MonoBehaviour
{
    Dropdown m_Dropdown;
    public GameObject camearaControlObj;
    private CameraController cameraController = null;

    private void Awake()
    {
        cameraController = camearaControlObj.GetComponent<CameraController>();
    }

    void Start()
    {
        //Fetch the Dropdown GameObject
        m_Dropdown = GetComponent<Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(m_Dropdown);
        });

        //Initialise the Text to say the first value of the Dropdown
        SetDropDown(m_Dropdown.value);
    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(Dropdown change)
    {
        SetDropDown(change.value);
    }

    void SetDropDown(int value)
    {
        switch (value)
        {
            case 0:
                {
                    cameraController.ShowSideACamera();
                    break;
                }
            case 1:
                {
                    cameraController.ShowSideBCamera();
                    break;
                }
            case 2:
                {
                    cameraController.ShowMainCamera();
                    break;
                }
            default:
                {
                    cameraController.ShowMainCamera();
                    break;
                }
        }
    }
}
