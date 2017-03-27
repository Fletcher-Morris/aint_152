using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
    }

    void SetupPlayerShip()
    {
        isPlayer = true;
        gameObject.transform.tag = "Player";
        shipDetails = GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.playerShip;
        transform.position = shipDetails.shipPos;
        transform.rotation = Quaternion.Euler(shipDetails.shipRot);
        shipDetails.shipHealth = 100;
    }

    public void TakeDamage(int damageAmount)
    {
        shipDetails.shipHealth -= TakeShieldDamage(damageAmount);

        if (shipDetails.shipHealth <= 0)
        {
            shipDetails.shipHealth = 0;
            Debug.Log(gameObject.name + " died!");
            GameObject explosion = GameObject.Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
            explosion.GetComponent<SpriteRenderer>().color = Color.yellow;
            explosion.transform.localScale = new Vector3(3,3,3);
            GameObject.Destroy(gameObject);
        }

        GameObject.Find("Health Text").GetComponent<Text>().text = "HEALTH: " + shipDetails.shipHealth;
        GameObject.Find("Power Text").GetComponent<Text>().text = "POWER: " + Mathf.Floor(shipDetails.shipReactor.currentPower);
        GameObject.Find("Shield Text").GetComponent<Text>().text = "SHIELD: " + Mathf.Floor(shipDetails.shipShield.shieldHealth);
    }

    public int TakeShieldDamage(int damageAmount)
    {
        if (shipDetails.shipShield.shieldHealth > 0)
        {
            shipDetails.shipShield.shieldHealth = (shipDetails.shipShield.shieldHealth - damageAmount);
            damageAmount = damageAmount - damageAmount / (100 - shipDetails.shipShield.absorbPercent);
        }

        shieldRechargeDelayTimer = shipDetails.shipShield.chargeDelay;

        GameObject.Find("Health Text").GetComponent<Text>().text = "HEALTH: " + shipDetails.shipHealth;
        GameObject.Find("Power Text").GetComponent<Text>().text = "POWER: " + Mathf.Floor(shipDetails.shipReactor.currentPower);
        GameObject.Find("Shield Text").GetComponent<Text>().text = "SHIELD: " + Mathf.Floor(shipDetails.shipShield.shieldHealth);

        return damageAmount;
    }

    public void TakePower(int powerAmount)
    {
        shipDetails.shipReactor.currentPower -= powerAmount;

        powerRechargeDelayTimer = shipDetails.shipReactor.rechargeDelay;

        GameObject.Find("Health Text").GetComponent<Text>().text = "HEALTH: " + shipDetails.shipHealth;
        GameObject.Find("Power Text").GetComponent<Text>().text = "POWER: " + Mathf.Floor(shipDetails.shipReactor.currentPower);
        GameObject.Find("Shield Text").GetComponent<Text>().text = "SHIELD: " + Mathf.Floor(shipDetails.shipShield.shieldHealth);
    }

    private void Update()
    {
        if (isPlayer)
        {
            GameObject.Find("Health Text").GetComponent<Text>().text = "HEALTH: " + shipDetails.shipHealth;
            GameObject.Find("Power Text").GetComponent<Text>().text = "POWER: " + Mathf.Floor(shipDetails.shipReactor.currentPower);
            GameObject.Find("Shield Text").GetComponent<Text>().text = "SHIELD: " + Mathf.Floor(shipDetails.shipShield.shieldHealth);
        }

        RechargePower();
    }

    void RechargePower()
    {
        powerRechargeDelayTimer = powerRechargeDelayTimer - 1 * Time.deltaTime;
        if (powerRechargeDelayTimer <= 0)
        {
            powerRechargeDelayTimer = 0;
            if (shipDetails.shipReactor.currentPower <= shipDetails.shipReactor.maxPower)
            {
                shipDetails.shipReactor.currentPower += shipDetails.shipReactor.rechargeRate * Time.deltaTime;
            }
            else
            {
                shipDetails.shipReactor.currentPower = shipDetails.shipReactor.maxPower;
            }
        }

        GameObject.Find("Health Text").GetComponent<Text>().text = "HEALTH: " + shipDetails.shipHealth;
        GameObject.Find("Power Text").GetComponent<Text>().text = "POWER: " + Mathf.Floor(shipDetails.shipReactor.currentPower);
        GameObject.Find("Shield Text").GetComponent<Text>().text = "SHIELD: " + Mathf.Floor(shipDetails.shipShield.shieldHealth);
    }

    void RechargeShield()
    {
        shieldRechargeDelayTimer = shieldRechargeDelayTimer - 1 * Time.deltaTime;
        if (shieldRechargeDelayTimer <= 0)
        {
            shieldRechargeDelayTimer = 0;
            if (shipDetails.shipShield.shieldHealth <= shipDetails.shipShield.maxShieldHealth)
            {
                shipDetails.shipShield.shieldHealth += shipDetails.shipShield.chargeRate * Time.deltaTime;
            }
            else
            {
                shipDetails.shipShield.shieldHealth = shipDetails.shipShield.maxShieldHealth;
            }
        }

        GameObject.Find("Health Text").GetComponent<Text>().text = "HEALTH: " + shipDetails.shipHealth;
        GameObject.Find("Power Text").GetComponent<Text>().text = "POWER: " + Mathf.Floor(shipDetails.shipReactor.currentPower);
        GameObject.Find("Shield Text").GetComponent<Text>().text = "SHIELD: " + Mathf.Floor(shipDetails.shipShield.shieldHealth);
    }
}
