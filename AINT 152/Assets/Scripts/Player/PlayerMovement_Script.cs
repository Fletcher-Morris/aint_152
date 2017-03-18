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

    public bool canMove = false;
    public bool canRotate = false;

    public GameObject playerSprite;

    void Start()
    {
        ChildToShip();
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
            //RigidbodyMovement();
        }
    }

    void LookAtMousePod()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
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

    }
}
