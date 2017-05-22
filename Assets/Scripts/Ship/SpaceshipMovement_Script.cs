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
    public bool useAlternativeMovement = false;

    public Vector2 axisInput;
    public Vector2 axisNormalized;
    public Vector2 axisFinalised;
    public float desiredAngle;

    void Start()
    {
		weaponWheelUI = GameObject.Find ("Weapon Wheel");
    }

    void FixedUpdate()
    {
        GetAxis();

        if (canRotate)
        {
            if (!useAlternativeMovement)
            {
                RotateShip(axisInput.x * 2f); 
            }
            else
            {
                AlternativeMovement();
            }
        }

		if (canMove && GetComponent<Rigidbody2D> ().bodyType == RigidbodyType2D.Dynamic)
        {
            if (!useAlternativeMovement)
            {
                ThrustShip(axisInput.y); 
            }
            else
            {
                AlternativeMovement();
            }
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
					GameObject.Find ("GM").GetComponent<GameState_Script> ().isUsingWeaponWheel = true;
				}
			}
		} else {
			if (GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.weaponsList [weaponWheelUI.GetComponent<WeaponWheel_Script> ().hoverItem - 1].weaponType != "null") {
				GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon = GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.weaponsList [weaponWheelUI.GetComponent<WeaponWheel_Script> ().hoverItem - 1];
			}
			weaponWheelUI.transform.GetChild(0).gameObject.SetActive (false);
			weaponWheelUI.GetComponent<WeaponWheel_Script> ().enabled = false;
			GameObject.Find ("GM").GetComponent<GameState_Script> ().isUsingWeaponWheel = false;
		}

        if (Input.GetKeyDown(KeyCode.M))
        {
            useAlternativeMovement = !useAlternativeMovement;
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

    void RotateShip(float amount)
    {
        gameObject.transform.Rotate(Vector3.back * Time.deltaTime * amount * rotateSpeed);
    }

    void ThrustShip(float amount)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * amount * thrustPower);
    }

    void AlternativeMovement()
    {
        float magicNumber = -57.4f;

        desiredAngle = Mathf.Atan2(axisNormalized.x, axisNormalized.y) * magicNumber;

        if (axisInput.magnitude != 0)
        {
            if (GetZAngle(transform.eulerAngles.z) >= desiredAngle + 1)
            {
                RotateShip(1);
            }
            else if (GetZAngle(transform.eulerAngles.z) <= desiredAngle - 1)
            {
                RotateShip(-1);
            }
        }

        ThrustShip(axisNormalized.magnitude * .5f);
    }

    float GetZAngle(float rawAngle)
    {

        float _zAngle = rawAngle;

        if (rawAngle >= 180f)
        {
            _zAngle = -(360 - rawAngle);
        }
        else if (rawAngle <= -180f)
        {
            _zAngle = -(360 - rawAngle);
        }
        else
        {
            _zAngle = rawAngle;
        }

        return _zAngle;
    }
}
