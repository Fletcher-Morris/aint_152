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
    public GameObject turretTriggerObject;
    public float triggerRange = 0.1f;
    public bool isInCockpitTrigger = false;
    public bool isInTurretTrigger = false;

    void Start()
    {
        gM = GameObject.Find("GM");
    }

    private void Update()
    {
        TestPlayerCockpitDistance();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInCockpitTrigger)
            {
                if (gameObject.GetComponent<ViewTransition_Script>().isViewingCrew)
                {
                    gM.GetComponent<GameState_Script>().gameState = "Flying Ship";
                    gameObject.GetComponent<ViewTransition_Script>().SwitchToShip();

                    gameObject.GetComponent<SpaceshipMovement_Script>().canMove = true;
                    gameObject.GetComponent<SpaceshipMovement_Script>().canRotate = true;

                    playerObject.GetComponent<Rigidbody2D>().isKinematic = true;
                }
                else
                {
                    gM.GetComponent<GameState_Script>().gameState = "Normal";
                    gameObject.GetComponent<ViewTransition_Script>().SwitchToCrew();

                    gameObject.GetComponent<SpaceshipMovement_Script>().canMove = false;
                    gameObject.GetComponent<SpaceshipMovement_Script>().canRotate = false;

                    playerObject.GetComponent<Rigidbody2D>().isKinematic = false;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (gM.GetComponent<GameState_Script>().gameState == "Paused" || gM.GetComponent<GameState_Script>().gameState == "Using Turret" || gM.GetComponent<GameState_Script>().gameState == "Flying Ship")
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
        TestPlayerTurretDistance();
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
        if(Vector2.Distance(playerPos2d, triggerPos2d) <= triggerRange)
        {
            isInCockpitTrigger = true;
        }
        else
        {
            isInCockpitTrigger = false;
        }
    }

    void TestPlayerTurretDistance()
    {
        Vector2 playerPos2d = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y);
        Vector2 triggerPos2d = new Vector2(turretTriggerObject.transform.position.x, turretTriggerObject.transform.position.y);
        if (Vector2.Distance(playerPos2d, triggerPos2d) <= triggerRange)
        {
            isInTurretTrigger = true;
        }
        else
        {
            isInTurretTrigger = false;
        }
    }
}
