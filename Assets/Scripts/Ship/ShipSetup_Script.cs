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

        healthUiText = GameObject.Find("Health Text");
        healthUiText.GetComponent<Text>().text = "HEALTH: " + shipDetails.shipHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        shipDetails.shipHealth -= damageAmount;

        if (shipDetails.shipHealth <= 0)
        {
            shipDetails.shipHealth = 0;
            Debug.Log(gameObject.name + " died!");
            GameObject explosion = GameObject.Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
            explosion.GetComponent<SpriteRenderer>().color = Color.yellow;
            explosion.transform.localScale = new Vector3(3,3,3);
            GameObject.Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isPlayer)
        {
            healthUiText = GameObject.Find("Health Text");
            healthUiText.GetComponent<Text>().text = "HEALTH: " + shipDetails.shipHealth;
        }
    }
}
