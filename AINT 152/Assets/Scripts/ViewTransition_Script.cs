using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTransition_Script : MonoBehaviour {

    public float crewCamSize = 1.25f;
    public float shipCamSize = 5;

    public float transitionSpeed = 20;

    public GameObject cameraObject;

    bool isViewingCrew = false;
    bool isViewingShip = false;
    public bool isSwitchingToCrew = false;
    public bool isSwitchingToShip = false;

    public LayerMask crewLayers;
    public LayerMask shipLayers;

    public void SwitchView()
    {
        if(isViewingCrew)
        {
            SwitchToShip();
        }
        else if(isViewingShip)
        {
            SwitchToCrew();
        }
        else
        {
            SwitchToCrew();
        }
    }

    public void SwitchToCrew()
    {
        isSwitchingToCrew = true;
        isSwitchingToShip = false;
    }

    public void SwitchToShip()
    {
        isSwitchingToCrew = false;
        isSwitchingToShip = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchView();
        }

        if (isSwitchingToShip)
        {
            cameraObject.GetComponent<Camera>().cullingMask = shipLayers;
            if (cameraObject.GetComponent<Camera>().orthographicSize < shipCamSize)
            {
                cameraObject.GetComponent<Camera>().orthographicSize += transitionSpeed * Time.deltaTime;
            }
            else
            {
                cameraObject.GetComponent<Camera>().orthographicSize = shipCamSize;
                isViewingShip = true;
                isViewingCrew = false;
                isSwitchingToShip = false;
                isSwitchingToCrew = false;
            }
        }
        else if (isSwitchingToCrew)
        {
            if (cameraObject.GetComponent<Camera>().orthographicSize > crewCamSize)
            {
                cameraObject.GetComponent<Camera>().orthographicSize -= transitionSpeed * Time.deltaTime;
            }
            else
            {
                cameraObject.GetComponent<Camera>().orthographicSize = crewCamSize;
                cameraObject.GetComponent<Camera>().cullingMask = crewLayers;
                isViewingShip = false;
                isViewingCrew = true;
                isSwitchingToShip = false;
                isSwitchingToCrew = false;
            }
        }
    }
}
