﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet_Script : NetworkBehaviour
{

    public int damage;

    public GameObject explosionPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hit = collision.gameObject;

        if (hit.gameObject.tag == "Player")
        {
            var health = hit.GetComponent<ShipHealth_Script>();
            if (health != null)
            {
                health.TakeDamage(damage);
            } 
        }
        else if(hit.gameObject.tag == "Asteroid")
        {

        }

        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        NetworkServer.Spawn(explosion);

        Destroy(gameObject);
    }
}