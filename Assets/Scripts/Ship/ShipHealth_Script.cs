using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealth_Script : MonoBehaviour
{
    [SerializeField]
    public const int MAX_HEALTH = 100;

    [SerializeField]
    int currentHealth = MAX_HEALTH;

    public GameObject healthUiText;
    public GameObject ShieldsUiText;

    private void Start()
    {
        healthUiText = GameObject.Find("Health Text");
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log(gameObject.name + " died!");
        }
    }

    void OnHealthChange(int newHealth)
    {
        healthUiText.GetComponent<Text>().text = "HEALTH: " + newHealth;
    }
}