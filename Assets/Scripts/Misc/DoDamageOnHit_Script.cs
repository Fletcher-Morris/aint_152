using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamageOnHit_Script : MonoBehaviour
{
    public int damageAmount = 5;
    public float minimumVelocity = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hit = collision.gameObject;

        if (hit.gameObject.tag == "Player")
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
        else if (hit.gameObject.tag == "Enemy")
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
    }
}