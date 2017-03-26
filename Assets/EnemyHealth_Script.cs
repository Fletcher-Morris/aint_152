using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyHealth_Script : NetworkBehaviour
{
    [SerializeField]
    public const int MAX_HEALTH = 100;

    [SerializeField]
    [SyncVar(hook = "OnEnemyHealthChange")]
    int currentHealth = MAX_HEALTH;

    public void TakeDamage(int damageAmount)
    {
        if (!isServer)
            return;

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log(gameObject.name + " died!");
        }
    }

    void OnEnemyHealthChange(int newHealth)
    {
        if (isLocalPlayer)
        {

        }
    }
}