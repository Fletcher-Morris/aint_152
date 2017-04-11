using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceshipMovement_Script : MonoBehaviour
{

    public GameObject statsUI;
	public GameObject weaponWheelUI;

    public float rotateSpeed = 50;
    public float thrustPower = 50000;

    public bool canMove = false;
    public bool canRotate = false;

    public Vector2 axisInput;
    public Vector2 axisNormalized;
    public Vector2 axisFinalised;

    void Start()
    {
		weaponWheelUI = GameObject.Find ("Weapon Wheel");
    }

    void FixedUpdate()
    {
        GetAxis();

        if (canRotate)
        {
            RotateShip();
        }

		if (canMove && GetComponent<Rigidbody2D> ().bodyType == RigidbodyType2D.Dynamic)
        {
            ThrustShip();
        }

		if(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1 && axisInput.magnitude == 0 && GetComponent<Rigidbody2D> ().bodyType == RigidbodyType2D.Dynamic)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

		if (gameObject.GetComponent<Rigidbody2D>().angularVelocity <= 5 && GetComponent<Rigidbody2D> ().bodyType == RigidbodyType2D.Dynamic)
        {
            gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }

        if (statsUI)
        {
            statsUI.GetComponent<Text>().text = "Velocity: " + gameObject.GetComponent<Rigidbody2D>().velocity.magnitude; 
        }

		if (GetComponent<ShipSetup_Script>().isPlayer) {
			if (GameObject.Find ("GM").GetComponent<GameState_Script> ().GetPlayerState() == "Flying Ship") {
				GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
			} else {
				GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Static;
			}
		}

		if (Input.GetButton("Weapon Wheel") && GetComponent<ShipSetup_Script> ().isPlayer) {
			if (weaponWheelUI.transform.GetChild(0).gameObject.activeInHierarchy == false) {
				if (GetComponent<ViewTransition_Script>().isViewingShip) {
					weaponWheelUI.GetComponent<WeaponWheel_Script> ().weaponList = GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.weaponsList;
					weaponWheelUI.transform.GetChild(0).gameObject.SetActive (true);
					weaponWheelUI.GetComponent<WeaponWheel_Script> ().enabled = true;
				}
			}
		} else {
			if (GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.weaponsList [weaponWheelUI.GetComponent<WeaponWheel_Script> ().hoverItem - 1].weaponType != "null") {
				GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon = GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.weaponsList [weaponWheelUI.GetComponent<WeaponWheel_Script> ().hoverItem - 1];
			}
			weaponWheelUI.transform.GetChild(0).gameObject.SetActive (false);
			weaponWheelUI.GetComponent<WeaponWheel_Script> ().enabled = false;
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
