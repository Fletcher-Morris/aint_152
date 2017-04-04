using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;

public class ShipSetup_Script : MonoBehaviour
{
    public Ship shipDetails;
    public bool isPlayer = false;

    public GameObject healthUiText;
    GameObject ShieldsUiText;

    public GameObject explosionPrefab;

    float powerRechargeDelayTimer;
    float shieldRechargeDelayTimer;

    void Start()
    {
        if (isPlayer)
        {
            SetupPlayerShip();
        }
        else
        {
            SetupEnemyShip();
        }
    }

    void SetupEnemyShip()
    {
        isPlayer = false;
        gameObject.transform.tag = "Enemy";
		shipDetails.shipTurret.turretWeapon.RandomizeWeapon (0, 200);
    }

    void SetupPlayerShip()
    {
        isPlayer = true;
        gameObject.transform.tag = "Player";
        shipDetails = GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.playerShip;
        transform.position = shipDetails.shipPos;
        transform.rotation = Quaternion.Euler(shipDetails.shipRot);

        Time.timeScale = 1;

        if (isPlayer)
        {
            UpdateUI();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        shipDetails.shipHealth -= TakeShieldDamage(damageAmount);

        if (shipDetails.shipHealth <= 0)
        {
            shipDetails.shipHealth = 0;
            Debug.Log(gameObject.name + " died!");
            GameObject explosion = GameObject.Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
            explosion.transform.localScale = new Vector3(3,3,3);

            if (isPlayer)
            {
				Time.timeScale = 0.2f;
                GameObject.Find("Pause Menu Canvas").transform.GetChild(3).gameObject.SetActive(true);
				if (GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.hardcore) {
					Directory.Delete(Application.dataPath + "/Saves/" + GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.worldName, true);
				}
            }

            GameObject.Destroy(gameObject);
        }

        if (isPlayer)
        {
            UpdateUI();
        }
    }

    public int TakeShieldDamage(int damageAmount)
    {
        if (shipDetails.shipShield.shieldHealth > 0)
        {
            shipDetails.shipShield.shieldHealth = (shipDetails.shipShield.shieldHealth - damageAmount);
            damageAmount = damageAmount - (damageAmount * shipDetails.shipShield.absorbPercent/100);
        }

        shieldRechargeDelayTimer = shipDetails.shipShield.chargeDelay;

        if (isPlayer)
        {
            UpdateUI();
        }

        return damageAmount;
    }

    public void TakePower(int powerAmount)
    {
        shipDetails.shipReactor.currentPower -= powerAmount;

        powerRechargeDelayTimer = shipDetails.shipReactor.rechargeDelay;
    }

    void UpdateUI()
    {
        GameObject.Find("Health Text").GetComponent<Text>().text = "HEALTH: " + shipDetails.shipHealth;
        GameObject.Find("Power Text").GetComponent<Text>().text = "POWER: " + Mathf.RoundToInt(shipDetails.shipReactor.currentPower);
        GameObject.Find("Shield Text").GetComponent<Text>().text = "SHIELD: " + Mathf.RoundToInt(shipDetails.shipShield.shieldHealth);
    }

    private void Update()
    {
        if (isPlayer)
        {
            UpdateUI();
        }

        if (shipDetails.shipReactor.currentPower < shipDetails.shipReactor.maxPower)
        {
            RechargePower(); 
        }
        else
        {
            shipDetails.shipReactor.currentPower = shipDetails.shipReactor.maxPower;
        }

        if(shipDetails.shipReactor.currentPower > 0)
        {
            RechargeShield();
        }
    }

    void RechargePower()
    {
        if(shipDetails.shipReactor.currentPower < 0)
        {
            shipDetails.shipReactor.currentPower = 0;
        }

        powerRechargeDelayTimer = powerRechargeDelayTimer - 1 * Time.deltaTime;
        if (powerRechargeDelayTimer <= 0)
        {
            powerRechargeDelayTimer = 0;
            if (shipDetails.shipReactor.currentPower < shipDetails.shipReactor.maxPower)
            {
                shipDetails.shipReactor.currentPower += shipDetails.shipReactor.rechargeRate * Time.deltaTime;
            }
            else
            {
                shipDetails.shipReactor.currentPower = shipDetails.shipReactor.maxPower;
            }
        }

        if (isPlayer)
        {
            UpdateUI();
        }
    }

    void RechargeShield()
    {
        if (shipDetails.shipShield.shieldHealth < 0)
        {
            shipDetails.shipShield.shieldHealth = 0;
        }

        shieldRechargeDelayTimer = shieldRechargeDelayTimer - 1 * Time.deltaTime;
        if (shieldRechargeDelayTimer <= 0)
        {
            shieldRechargeDelayTimer = 0;
            if (shipDetails.shipShield.shieldHealth < shipDetails.shipShield.maxShieldHealth)
            {
                shipDetails.shipShield.shieldHealth += shipDetails.shipShield.chargeRate * Time.deltaTime;
            }
            else
            {
                shipDetails.shipShield.shieldHealth = shipDetails.shipShield.maxShieldHealth;
            }
        }

        if(shipDetails.shipShield.shieldHealth >= shipDetails.shipShield.maxShieldHealth)
        {
            shipDetails.shipShield.shieldHealth = shipDetails.shipShield.maxShieldHealth;
        }

        if (isPlayer)
        {
            UpdateUI();
        }
    }
}
