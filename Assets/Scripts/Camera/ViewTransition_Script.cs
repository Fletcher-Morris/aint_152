using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTransition_Script : MonoBehaviour {

    public float crewCamSize = 1.25f;
    public float shipCamSize = 5;

    public float transitionSpeed = 20;

    public GameObject cameraObject;
    public GameObject playerObject;

    public bool isViewingCrew = false;
    public bool isViewingShip = false;
    public bool isSwitchingToCrew = true;
    public bool isSwitchingToShip = false;

    public LayerMask crewLayers;
    public LayerMask shipLayers;

    public float cameraRotationDiff = 0f;

    void Start()
    {
        cameraObject = Camera.main.gameObject;
    }

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
        cameraRotationDiff = (gameObject.transform.rotation.z - cameraObject.transform.rotation.z);

        if (isSwitchingToShip)
        {
            playerObject.GetComponent<Collider2D>().isTrigger = true;
            cameraObject.GetComponent<Camera>().cullingMask = shipLayers;
            if (cameraObject.GetComponent<Camera>().orthographicSize < shipCamSize)
            {
                cameraObject.GetComponent<Camera>().orthographicSize += transitionSpeed * Time.deltaTime;
            }
            else
            {
                cameraObject.GetComponent<Camera>().orthographicSize = shipCamSize;
                cameraObject.transform.rotation = new Quaternion(0,0,0,0);
                cameraObject.GetComponent<Camera>().cullingMask = shipLayers;
                isViewingShip = true;
                isViewingCrew = false;
                isSwitchingToShip = false;
                isSwitchingToCrew = false;
            }
        }
        else if (isSwitchingToCrew)
        {
            playerObject.GetComponent<Collider2D>().isTrigger = false;

            if (cameraObject.GetComponent<Camera>().orthographicSize > crewCamSize)
            {
                cameraObject.GetComponent<Camera>().orthographicSize -= transitionSpeed * Time.deltaTime;
            }
            else
            {
                cameraObject.GetComponent<Camera>().orthographicSize = crewCamSize;
                cameraObject.transform.rotation = gameObject.transform.rotation;
                cameraObject.GetComponent<Camera>().cullingMask = crewLayers;
                isViewingShip = false;
                isViewingCrew = true;
                isSwitchingToShip = false;
                isSwitchingToCrew = false;
            }
        }

        if (isViewingCrew)
        {
            cameraObject.transform.rotation = gameObject.transform.rotation;
        }
    }
}
