using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Script : MonoBehaviour
{

    public float rotateSpeed = 0.005f;
    public float moveSpeed = 1f;

    Vector2 axisInput;
    Vector2 axisNormalized;
    Vector2 axisFinalised;

    public bool canMove = false;
    public bool canRotate = false;

    public GameObject playerObject;

    GameObject gM;
    GameObject playerShip;


    public GameObject cockpitTriggerObject;
    public GameObject mapTriggerObject;
    public GameObject computerTriggerObject;
	public GameObject engineRoomTriggerObject;
    public float cockpitRange = 0.1f;
    public float mapRange = 0.2f;
    public float computerRange = 0.2f;
    public bool isInCockpitTrigger = false;
    public bool isInMapTrigger = false;
    public bool isInComputerTrigger = false;
	public bool isInEngineRoomTrigger = false;

    void Start()
    {
        gM = GameObject.Find("GM");
    }

    private void Update()
    {
        TestPlayerCockpitDistance();

        if (Input.GetKeyDown(KeyCode.E))
        {
			if (isInCockpitTrigger) {
				if (gameObject.GetComponent<ViewTransition_Script> ().isViewingCrew) {
					gM.GetComponent<GameState_Script> ().SetPlayerState ("Flying Ship");
					gameObject.GetComponent<ViewTransition_Script> ().SwitchToShip ();

					gameObject.GetComponent<SpaceshipMovement_Script> ().canMove = true;
					gameObject.GetComponent<SpaceshipMovement_Script> ().canRotate = true;

					playerObject.GetComponent<Rigidbody2D> ().isKinematic = true;

					GameObject.Find ("RM").GetComponent<WaveManager_Script> ().doSpawn = true;
				} else {
					gM.GetComponent<GameState_Script> ().SetPlayerState ("Normal");
					gameObject.GetComponent<ViewTransition_Script> ().SwitchToCrew ();

					gameObject.GetComponent<SpaceshipMovement_Script> ().canMove = false;
					gameObject.GetComponent<SpaceshipMovement_Script> ().canRotate = false;

					playerObject.GetComponent<Rigidbody2D> ().isKinematic = false;
				}
			} else if (isInEngineRoomTrigger) {
				if (gameObject.GetComponent<ViewTransition_Script> ().isViewingCrew) {
					if (gM.GetComponent<GameState_Script> ().GetPlayerState() == "Normal") {
						playerObject.GetComponent<Rigidbody2D> ().isKinematic = true;
						GameObject.Find ("Shop Canvas").GetComponent<Canvas> ().enabled = true;
						gM.GetComponent<GameState_Script> ().SetPlayerState("In Menu");
						GameObject.Find ("Shop Canvas").GetComponent<ItemShop_Script> ().OpenShop ();
					} else if(gM.GetComponent<GameState_Script> ().GetPlayerState() == "In Menu"){
						GameObject.Find ("Shop Canvas").GetComponent<ItemShop_Script> ().CloseItemShop ();
					}
				}
			}
        }
    }

    void FixedUpdate()
    {
		if (gM.GetComponent<GameState_Script>().GetPlayerState() != "Normal")
        {
            canMove = false;
            canRotate = false;
        }
        else
        {
            canRotate = true;
            canMove = true;
        }

        if (canRotate)
        {
            LookAtMousePod(); 
        }

        if (canMove)
        {
            Movement();
        }

        TestPlayerCockpitDistance();
        TestMapDistance();
        TestComputerDistance();
		TestEngineRoomDistance ();
    }

    void LookAtMousePod()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
    }

    void Movement()
    {
        axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        axisNormalized = axisInput.normalized;
        axisFinalised = axisNormalized * moveSpeed;
        playerObject.transform.Translate(axisFinalised.x * Time.deltaTime, axisFinalised.y * Time.deltaTime, 0);
    }

    void RigidbodyMovement()
    {
        axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        axisNormalized = axisInput.normalized;
        axisFinalised = axisNormalized * moveSpeed;
        playerObject.GetComponent<Rigidbody2D>().AddRelativeForce(axisFinalised * moveSpeed);
    }

    void TestPlayerCockpitDistance()
    {
        Vector2 playerPos2d = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y);
        Vector2 triggerPos2d = new Vector2(cockpitTriggerObject.transform.position.x, cockpitTriggerObject.transform.position.y);
        if(Vector2.Distance(playerPos2d, triggerPos2d) <= cockpitRange)
        {
            isInCockpitTrigger = true;
            if (canMove)
            {
                GameObject.Find("Cockpit_Trigger").transform.GetChild(0).gameObject.SetActive(true); 
            }
        }
        else
        {
            isInCockpitTrigger = false;
            GameObject.Find("Cockpit_Trigger").transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void TestMapDistance()
    {
        Vector2 playerPos2d = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y);
        Vector2 triggerPos2d = new Vector2(mapTriggerObject.transform.position.x, mapTriggerObject.transform.position.y);
        if (Vector2.Distance(playerPos2d, triggerPos2d) <= mapRange)
        {
            isInMapTrigger = true;
            if (canMove)
            {
                GameObject.Find("Map_Trigger").transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else
        {
            isInMapTrigger = false;
            GameObject.Find("Map_Trigger").transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void TestComputerDistance()
    {
        Vector2 playerPos2d = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y);
        Vector2 triggerPos2d = new Vector2(computerTriggerObject.transform.position.x, computerTriggerObject.transform.position.y);
        if (Vector2.Distance(playerPos2d, triggerPos2d) <= computerRange)
        {
            isInComputerTrigger = true;
            if (canMove)
            {
                GameObject.Find("Computer_Trigger").transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else
        {
            isInComputerTrigger = false;
            GameObject.Find("Computer_Trigger").transform.GetChild(0).gameObject.SetActive(false);
        }
    }

	void TestEngineRoomDistance()
	{
		Vector2 playerPos2d = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y);
		Vector2 triggerPos2d = new Vector2(engineRoomTriggerObject.transform.position.x, engineRoomTriggerObject.transform.position.y);
		if (Vector2.Distance(playerPos2d, triggerPos2d) <= computerRange)
		{
			isInEngineRoomTrigger = true;
			if (canMove)
			{
				GameObject.Find("EngineRoom_Trigger").transform.GetChild(0).gameObject.SetActive(true);
			}
		}
		else
		{
			isInEngineRoomTrigger = false;
			GameObject.Find("EngineRoom_Trigger").transform.GetChild(0).gameObject.SetActive(false);
		}
	}
}
