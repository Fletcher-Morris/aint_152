using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceshipMovement_Script : NetworkBehaviour {

    public GameObject statsUI;

    public float rotateSpeed = 0.005f;
    public float moveSpeed = 0.01f;

    public bool canMove = false;
    public bool canRotate = false;

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            canRotate = !canRotate;
        }

        GetAxis();

        if (canRotate)
        {
            //  LookAtMousePod();
            RotateShip();
        }

        if (canMove)
        {
            ThrustShip();
        }

        statsUI.GetComponent<Text>().text = "Velocity: " + gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
    }

    void LookAtMousePod()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * rotateSpeed * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    void GetAxis()
    {
        axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        axisNormalized = axisInput.normalized;
        axisFinalised = axisNormalized * moveSpeed;
    }

    void RotateShip()
    {
        gameObject.transform.Rotate(Vector3.back * Time.deltaTime * axisInput.x * rotateSpeed);
    }

    void ThrustShip()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * axisFinalised.y);
    }
}
