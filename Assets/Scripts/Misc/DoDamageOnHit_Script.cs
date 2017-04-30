using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamageOnHit_Script : MonoBehaviour
{
    public int damageAmount = 5;
    public float minimumVelocity = 2f;
	public bool damagePlayer = true;
	public bool damageEnemies = true;
    public bool damageOther = true;

    public bool useTrigger = false;

    private void Start()
    {
        if (gameObject.GetComponent<Collider2D>().isTrigger)
            useTrigger = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!useTrigger)
        {
            var hit = collision.gameObject;

            if (hit.gameObject.tag == "Player" && damagePlayer)
            {
                if (collision.relativeVelocity.magnitude >= minimumVelocity)
                {
                    var health = hit.GetComponent<ShipSetup_Script>();
                    if (health != null)
                    {
                        health.TakeDamage(damageAmount);
                    }
                }
            }
            else if (hit.gameObject.tag == "Enemy" && damageEnemies)
            {
                if (collision.relativeVelocity.magnitude >= minimumVelocity)
                {
                    var health = hit.GetComponent<ShipSetup_Script>();
                    if (health != null)
                    {
                        health.TakeDamage(damageAmount);
                    }
                }
            }
            else if (damageOther)
            {
                var health = hit.GetComponent<GenericHealth_Script>();
                if (health != null)
                {
                    health.TakeDamage(damageAmount);
                }
            }
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (useTrigger)
        {
            var hit = collision.gameObject;

            if (hit.gameObject.tag == "Player" && damagePlayer)
            {
                var health = hit.GetComponent<ShipSetup_Script>();
                if (health != null)
                {
                    health.TakeDamage(damageAmount);
                }
            }
            else if (hit.gameObject.tag == "Enemy" && damageEnemies)
            {
                var health = hit.GetComponent<ShipSetup_Script>();
                if (health != null)
                {
                    health.TakeDamage(damageAmount);
                }
            }
            else if (damageOther)
            {
                var health = hit.GetComponent<GenericHealth_Script>();
                if (health != null)
                {
                    health.TakeDamage(damageAmount);
                }
            }
        }
	}
}