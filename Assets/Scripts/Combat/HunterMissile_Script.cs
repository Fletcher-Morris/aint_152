using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterMissile_Script : MonoBehaviour {

    public float thrust;
    public float rotationSpeed;
    public int damage;
    public bool active = false;
    public GameObject explosionPrefab;

    private void Update()
    {
        MoveToTarget();
        RotateToTarget();
    }

    void MoveToTarget()
    {
        if (gameObject.GetComponent<Rigidbody2D>())
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * thrust); 
        }
    }

    void RotateToTarget()
    {
        Vector3 vectorToTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (active)
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
            else if (hit.gameObject.tag == "Enemy")
            {
                var health = hit.GetComponent<ShipSetup_Script>();
                if (health != null)
                {
                    health.TakeDamage(damage);

                    if (GameObject.Find("Player Ship"))
                    {
                        GameObject.Find("Player Ship").GetComponent<ShipSetup_Script>().shipDetails.shipTurret.AddExperience();
                    }
                }
            }
            else if (hit.GetComponent<GenericHealth_Script>())
            {

                var health = hit.GetComponent<GenericHealth_Script>();

                if (health != null)
                {

                    health.TakeDamage(damage);
                }
            }

            GameObject.Instantiate(explosionPrefab, transform.position, transform.rotation);

            Destroy(gameObject); 
        }
    }
}
