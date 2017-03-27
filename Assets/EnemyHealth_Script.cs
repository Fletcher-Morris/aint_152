using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth_Script : MonoBehaviour
{
    [SerializeField]
    public const int MAX_HEALTH = 100;

    [SerializeField]
    int currentHealth = MAX_HEALTH;

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log(gameObject.name + " died!");
        }
    }
}