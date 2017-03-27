using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSetup_Script : MonoBehaviour
{
    public Ship shipDetails;
    bool isPlayer = true;

    void Start()
    {
        if (shipDetails.isPlayer)
        {
            SetupPlayerShip();
        }
        else
        {
            SetupEnemyShip();
        }
    }

    void SetupEnemyShip()
    {
        gameObject.transform.tag = "Enemy";
        isPlayer = false;
    }

    void SetupPlayerShip()
    {
        gameObject.transform.tag = "Player";
        shipDetails = GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.playerShip;
        transform.position = shipDetails.shipPos;
        transform.rotation = Quaternion.Euler(shipDetails.shipRot);
    }
}
