using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceshipMovement_Script : MonoBehaviour
{

    public GameObject statsUI;

    public float rotateSpeed = 50;
    public float thrustPower = 50000;

    public bool canMove = false;
    public bool canRotate = false;

    public Vector2 axisInput;
    public Vector2 axisNormalized;
    public Vector2 axisFinalised;

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            canRotate = !canRotate;
        }

        GetAxis();

        if (canRotate)
        {
            RotateShip();
        }

        if (canMove)
        {
            ThrustShip();
        }

        if(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1 && axisInput.magnitude == 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (gameObject.GetComponent<Rigidbody2D>().angularVelocity <= 5)
        {
            gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }

        if (statsUI)
        {
            statsUI.GetComponent<Text>().text = "Velocity: " + gameObject.GetComponent<Rigidbody2D>().velocity.magnitude; 
        }
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
        axisFinalised = axisNormalized * thrustPower;
    }

    void RotateShip()
    {
        gameObject.transform.Rotate(Vector3.back * Time.deltaTime * axisInput.x * rotateSpeed);
    }

    void ThrustShip()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * axisInput.y * thrustPower);
    }
}
