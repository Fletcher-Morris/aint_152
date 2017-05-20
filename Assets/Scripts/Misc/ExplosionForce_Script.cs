using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce_Script : MonoBehaviour {

    public float radius = 5f;
    public float force = 10f;

    public bool explodeOnStart = true;

    private void Start()
    {
        if (explodeOnStart)
        {
            Explode(radius, force);
        }
    }

    public void Explode(float _radius, float _force)
    {
        Vector2 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.AddForce(new Vector2(rb.gameObject.transform.position.x - explosionPos.x, rb.gameObject.transform.position.y - explosionPos.y).normalized * force, ForceMode2D.Impulse);
                Debug.Log("Exploded an object: " + rb.gameObject.name);
            }
        }
    }
}
