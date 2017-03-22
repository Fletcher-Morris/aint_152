﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement_Script : NetworkBehaviour {

    public float rotateSpeed = 0.005f;
    public float moveSpeed = 1f;

    Vector2 axisInput;
    Vector2 axisNormalized;
    Vector2 axisFinalised;

    public bool canMove = false;
    public bool canRotate = false;

    public GameObject playerSprite;

    GameObject gM;
    [SyncVar]
    GameObject playerShip;

    public bool isInCockpitTrigger = false;
    public bool isInTurretTrigger = false;

    void Start()
    {
        gM = GameObject.Find("GM");
        ChildToShip();
    }

    void Update()
    {
        if(gM.GetComponent<GameState_Script>().gameState == "Paused" || gM.GetComponent<GameState_Script>().gameState == "Using Turret" || gM.GetComponent<GameState_Script>().gameState == "Flying Ship")
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
            //RigidbodyMovement();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInCockpitTrigger)
            {
                if (gameObject.GetComponent<ViewTransition_Script>().isViewingCrew)
                {
                    if (playerShip.GetComponent<ShipSetup_Script>().cockpitBeingUsed == false)
                    {
                        gM.GetComponent<GameState_Script>().gameState = "Flying Ship";
                        playerShip.GetComponent<ShipSetup_Script>().CmdSetCockitBeingUsed(true);
                        gameObject.GetComponent<ViewTransition_Script>().SwitchToShip();

                        playerShip.GetComponent<SpaceshipMovement_Script>().canMove = true;
                        playerShip.GetComponent<SpaceshipMovement_Script>().canRotate = true;
                    }
                }
                else
                {
                    gM.GetComponent<GameState_Script>().gameState = "Normal";
                    playerShip.GetComponent<ShipSetup_Script>().CmdSetCockitBeingUsed(false);
                    gameObject.GetComponent<ViewTransition_Script>().SwitchToCrew();

                    playerShip.GetComponent<SpaceshipMovement_Script>().canMove = false;
                    playerShip.GetComponent<SpaceshipMovement_Script>().canRotate = false;
                }
            }
        }
    }

    void LookAtMousePod()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    void Movement()
    {
        axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        axisNormalized = axisInput.normalized;
        axisFinalised = axisNormalized * moveSpeed;
        gameObject.transform.Translate(axisFinalised.x * Time.deltaTime, axisFinalised.y * Time.deltaTime, 0);
    }

    void RigidbodyMovement()
    {
        axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        axisNormalized = axisInput.normalized;
        axisFinalised = axisNormalized * moveSpeed;
        gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(axisFinalised * moveSpeed);
    }

    void ChildToShip()
    {
        gameObject.transform.parent = GameObject.Find("PlayerShip").transform;
        playerShip = GameObject.Find("PlayerShip");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Cockpit_Trigger")
        {
            Debug.Log(gameObject.name + ": entered cockpit trigger.");
            isInCockpitTrigger = true;
        }
        else if (col.gameObject.name == "Turret_Trigger")
        {
            Debug.Log(gameObject.name + ": entered turret trigger.");
            isInTurretTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Cockpit_Trigger")
        {
            Debug.Log(gameObject.name + ": left cockpit trigger.");
            isInCockpitTrigger = false;
        }
        else if (col.gameObject.name == "Turret_Trigger")
        {
            Debug.Log(gameObject.name + ": left turret trigger.");
            isInTurretTrigger = false;
        }
    }
}
