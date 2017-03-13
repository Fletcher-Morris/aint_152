using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceshipMovement_Script : NetworkBehaviour {

    public float rotateSpeed = 0.005f;
    public float moveSpeed = 0.01f;

    public bool canMove = true;
    public bool canRotate = true;

    Vector2 axisInput;
    Vector2 axisNormalized;
    Vector2 axisFinalised;

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
        if (canRotate)
        {
            LookAtMousePod(); 
        }

        if (canMove)
        {
            Movement();
        }
    }

    void LookAtMousePod()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * rotateSpeed * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    void Movement()
    {
        axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        axisNormalized = axisInput.normalized;
        axisFinalised = axisNormalized * moveSpeed;
        transform.Translate(axisFinalised.x, axisFinalised.y, 0);
    }
}
