using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTransition_Script : MonoBehaviour
{

    public float crewCamSize = 1.25f;
    public float shipCamSize = 5;

    public float transitionSpeed = 20;

    public GameObject cameraObject;
    public GameObject uiCameraObject;
    public GameObject playerObject;

    public bool isViewingCrew = false;
    public bool isViewingShip = false;
    public bool isSwitchingToCrew = true;
    public bool isSwitchingToShip = false;

    public LayerMask crewLayers;
    public LayerMask shipLayers;

    public float cameraRotationDiff = 0f;

	public float zAngle;

    void Start()
    {
        cameraObject = Camera.main.gameObject;
        uiCameraObject = cameraObject.transform.parent.GetChild(1).gameObject;
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
		GetZAngle ();
    }

    public void SwitchToShip()
    {
        isSwitchingToCrew = false;
        isSwitchingToShip = true;
		GetZAngle ();
    }

    void Update()
    {
		
        if (isSwitchingToShip)
        {
			playerObject.GetComponent<Collider2D>().isTrigger = true;
            cameraObject.GetComponent<Camera>().cullingMask = shipLayers;
            GameObject.Find("Cockpit_Trigger").transform.GetChild(0).gameObject.SetActive(false);
            gameObject.GetComponent<TurretController_Script>().canRotate = true;
            gameObject.GetComponent<ShootWeapon_Script>().canShoot = true;

			if (cameraObject.GetComponent<Camera>().orthographicSize < shipCamSize - 0.01f)
            {
                Debug.Log("Trying To Switch To Ship View.");
				cameraObject.GetComponent<Camera> ().orthographicSize = Mathf.Lerp (cameraObject.GetComponent<Camera> ().orthographicSize, shipCamSize, transitionSpeed);
                //cameraObject.GetComponent<Camera>().orthographicSize += transitionSpeed * Time.deltaTime;
                uiCameraObject.GetComponent<Camera>().orthographicSize = cameraObject.GetComponent<Camera>().orthographicSize;
                cameraObject.transform.eulerAngles = new Vector3(0, 0, (cameraObject.transform.eulerAngles.z + 1));
                //cameraObject.transform.eulerAngles = new Vector3 (0,0,Mathf.Lerp (cameraObject.transform.eulerAngles.z, 0, transitionSpeed));
                cameraObject.GetComponent<Camera>().cullingMask = shipLayers;
            }
            else
            {
                cameraObject.GetComponent<Camera>().orthographicSize = shipCamSize;
                uiCameraObject.GetComponent<Camera>().orthographicSize = cameraObject.GetComponent<Camera>().orthographicSize;
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
            gameObject.GetComponent<TurretController_Script>().canRotate = false;
            gameObject.GetComponent<ShootWeapon_Script>().canShoot = false;

            if (cameraObject.GetComponent<Camera>().orthographicSize > crewCamSize + 0.01f)
            {
				cameraObject.GetComponent<Camera> ().orthographicSize = Mathf.Lerp (cameraObject.GetComponent<Camera> ().orthographicSize, crewCamSize, transitionSpeed);
                //cameraObject.GetComponent<Camera>().orthographicSize -= transitionSpeed * Time.deltaTime;
                uiCameraObject.GetComponent<Camera>().orthographicSize = cameraObject.GetComponent<Camera>().orthographicSize;
				cameraObject.transform.eulerAngles = new Vector3 (0,0,Mathf.Lerp (GetZAngle(cameraObject.transform.eulerAngles.z), GetZAngle(transform.eulerAngles.z), transitionSpeed));
                cameraObject.GetComponent<Camera>().cullingMask = crewLayers;
            }
            else
            {
                cameraObject.GetComponent<Camera>().orthographicSize = crewCamSize;
                uiCameraObject.GetComponent<Camera>().orthographicSize = cameraObject.GetComponent<Camera>().orthographicSize;
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

	void GetZAngle(){
		
		if (transform.eulerAngles.z >= 180f) {
			zAngle = -(360 - transform.eulerAngles.z);
		} else if (transform.eulerAngles.z <= -180f) {
			zAngle = -(360 - transform.eulerAngles.z);
		} else {
			zAngle = transform.eulerAngles.z;
		}
	}

	float GetZAngle(float rawAngle){

		float _zAngle = rawAngle;

		if (rawAngle >= 180f) {
			_zAngle = -(360 - rawAngle);
		} else if (rawAngle <= -180f) {
			_zAngle = -(360 - rawAngle);
		} else {
			_zAngle = rawAngle;
		}

		return _zAngle;
	}
}
