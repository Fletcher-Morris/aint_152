using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DoDamageOnHit_Script : NetworkBehaviour
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
                var health = hit.GetComponent<ShipHealth_Script>();
                if (health != null)
                {
                    health.TakeDamage(damageAmount);
                } 
            }
        }
    }
}