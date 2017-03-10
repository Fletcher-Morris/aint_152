using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceshipMovement_Script : NetworkBehaviour {

    public float rotateSpeed = 1;

    void Start()
    {
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
    }

    void Update()
    {
        LookAtMousePod();
    }

    void LookAtMousePod()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * rotateSpeed * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
