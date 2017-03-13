using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class TurretController_Script : NetworkBehaviour
{

    public float rotateSpeed = 1;
    public GameObject turretObject;

    public bool canRotate = false;

    void Start()
    {
        //CheckLocal();
    }

    void CheckLocal()
    {
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            canRotate = !canRotate;
        }

        if (canRotate)
        {
            LookAtMousePod();
        }
    }

    void LookAtMousePod()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * rotateSpeed * Mathf.Rad2Deg;
        turretObject.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}