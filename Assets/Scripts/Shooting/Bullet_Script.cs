using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{

    public int damage;

    public GameObject explosionPrefab;

    public bool playerBullet = false;

    private void Update()
    {
        if (playerBullet)
        {
            gameObject.layer = 8;
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hit = collision.gameObject;

        if (hit.gameObject.tag == "Player")
        {
            var health = hit.GetComponent<ShipSetup_Script>();
            if (health != null)
            {
                health.TakeDamage(damage);
            } 
        }
        else if(hit.gameObject.tag == "Enemy")
        {
            var health = hit.GetComponent<ShipSetup_Script>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
        else if(hit.gameObject.tag == "Asteroid")
        {
            var health = hit.GetComponent<GenericHealth_Script>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}