using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer_Script : MonoBehaviour {

    public GameObject myPlayerObject;
    public GameObject myShipObject;

    public bool followPlayer;
    public bool followShip;

    public Vector3 targetPos;
    public Quaternion targetRot;

    void Start()
    {
        GetMyShip();
        GetLocalPlayer();
    }

    void Update()
    {
        if (followPlayer)
        {
            FollowPlayer();
        }
        else if (followShip)
        {
            FollowShip();
        }
    }

    void FollowPlayer()
    {
        targetPos = new Vector3(myPlayerObject.transform.position.x, myPlayerObject.transform.position.y, -10);
        targetRot = new Quaternion(myShipObject.transform.rotation.x, myShipObject.transform.rotation.y, myShipObject.transform.rotation.z, 1);
    }

    void FollowShip()
    {
        targetPos = new Vector3(myShipObject.transform.position.x, myShipObject.transform.position.y, -10);
        targetRot = new Quaternion(0,0,0,1);
    }

    void GetLocalPlayer()
    {
        foreach (GameObject playerObject in (GameObject.FindGameObjectsWithTag("Player")))
        {
            if (playerObject.GetComponent<PlayerMovement_Script>())
            {
                myPlayerObject = playerObject;
            }
        }
    }

    void GetMyShip()
    {
        if (GameObject.Find("Ship_Normandy(clone)"))
        {
            myShipObject = GameObject.Find("Ship_Normandy");
        }
        else if (GameObject.Find("Ship_Normandy"))
        {
            myShipObject = GameObject.Find("Ship_Normandy (clone)");
        }
    }
}
