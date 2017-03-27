using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHealth_Script : MonoBehaviour
{
    public int currentHealth = 100;

    public bool explodeOnDeath = true;
    public GameObject explosionPrefab;
    public float explosionScale = 1;

    public bool destroyOnDeath = true;

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log(gameObject.name + " died!");

            if (explodeOnDeath)
            {
                GameObject explosion = GameObject.Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
                explosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);
            }

            if (destroyOnDeath)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}