using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAi_Script : MonoBehaviour
{
    public float enemyDetectionRange = 30;
	public float enemyShootRange = 8;
    public float rotateSpeed = 50f;
    public float rotationSmoothing = 5f;

    public float playerPriority = 4;
    public float objectivePriority = 1f;
    public float priorityRatio = 0;
    public float objectiveDistance = 0;
    public float playerDistance = 0;
    GameObject playerTarget;
    GameObject objectiveTarget;

    public GameObject turretObject;
    public GameObject targetEnemy;
    public float currentEnemyRange;

	public Sprite ionBlasterTurretSprite;
	public Sprite quantumPrismTurretSprite;

    private void Update()
    {
        targetEnemy = SearchForTarget();

        currentEnemyRange = Vector2.Distance(targetEnemy.transform.position, gameObject.transform.position);

        if (Vector2.Distance(targetEnemy.transform.position, gameObject.transform.position) > enemyDetectionRange)
        {
            targetEnemy = null;
        }

        if (targetEnemy && GameObject.Find("GM").GetComponent<GameState_Script>().GetPlayerState() == "Flying Ship")
        {
            AimTurret();
            if (currentEnemyRange <= enemyShootRange)
            {
                ShootGun();
            }
        }

        if (currentEnemyRange >= 3 && targetEnemy && GameObject.Find("GM").GetComponent<GameState_Script>().GetPlayerState() == "Flying Ship")
        {
            MoveShipRigidbody();
            RotateShip();
        }

        if (GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.weaponType == "Ion Blaster") {
			turretObject.GetComponent<SpriteRenderer> ().sprite = ionBlasterTurretSprite;
			turretObject.transform.GetChild (0).gameObject.transform.localPosition = new Vector3 (0,0.2f,0);
		} else if (GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.weaponType == "Quantum Prism") {
			turretObject.GetComponent<SpriteRenderer> ().sprite = quantumPrismTurretSprite;
			turretObject.transform.GetChild (0).gameObject.transform.localPosition = new Vector3 (0,0,0);
		}
    }

    GameObject SearchForTarget()
    {
        priorityRatio = playerPriority / objectivePriority;

        foreach (GameObject _foundObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (Vector2.Distance(_foundObject.transform.position, gameObject.transform.position) <= enemyDetectionRange)
            {
                playerTarget = _foundObject;
                playerDistance = Vector2.Distance(_foundObject.transform.position, gameObject.transform.position);
            }
        }

        foreach (GameObject _foundObject in GameObject.FindGameObjectsWithTag("Objective"))
        {
            objectiveTarget = _foundObject;
            objectiveDistance = Vector2.Distance(_foundObject.transform.position, gameObject.transform.position);
        }

        if (playerTarget && objectiveTarget)
        {
            if(playerDistance <= objectiveDistance * priorityRatio)
            {
                return playerTarget;
            }
            else
            {
                return objectiveTarget;
            }
        }
        else if (objectiveTarget)
        {
            return objectiveTarget;
        }
        else if (playerTarget)
        {
            return playerTarget;
        }
        else
        {
            return null;
        }
    }

    void MoveShip()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, gameObject.GetComponent<ShipSetup_Script>().shipDetails.shipEngine.maxThrust * 0.01f);
    }

    void MoveShipRigidbody()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * gameObject.GetComponent<ShipSetup_Script>().shipDetails.shipEngine.maxThrust);
    }

    void RotateShip()
    {
        Vector3 difference = targetEnemy.transform.position - transform.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion newRot = Quaternion.Euler(new Vector3(0.0f, 0.0f, rotZ + -90));
        gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * rotationSmoothing);
    }

	void AimTurret(){
		Vector3 difference = targetEnemy.transform.position - transform.position;
		difference.Normalize();
		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		Quaternion newRot = Quaternion.Euler(new Vector3(0.0f, 0.0f, rotZ + -90));
		turretObject.transform.rotation = Quaternion.Lerp(turretObject.transform.rotation, newRot, Time.deltaTime * gameObject.GetComponent<ShipSetup_Script>().shipDetails.shipTurret.rotationSpeed);
	}

    void ShootGun()
    {
		GetComponent<ShootWeapon_Script> ().Shoot ();
    }
}