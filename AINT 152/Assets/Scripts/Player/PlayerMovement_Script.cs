using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement_Script : NetworkBehaviour {

    public float rotateSpeed = 0.005f;
    public float moveSpeed = 0.005f;

    Vector2 axisInput;
    Vector2 axisNormalized;
    Vector2 axisFinalised;

    public bool canMove = true;
    public bool canRotate = true;

    public GameObject playerSprite;

    void Start()
    {
        CheckLocal();
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
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        playerSprite.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    void Movement()
    {
        axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        axisNormalized = axisInput.normalized;
        axisFinalised = axisNormalized * moveSpeed;
        transform.Translate(axisFinalised.x, axisFinalised.y, 0);
    }
}
