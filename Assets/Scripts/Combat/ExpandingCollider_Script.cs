using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandingCollider_Script : MonoBehaviour {

    public float expansionSpeed = 1f;
    public float maxExpansionFactor = 5f;
    public bool destroyWhenDone = true;

    float currentExpansionFactor;
    float realativeMaxExpansion;

    private void Start()
    {
        currentExpansionFactor = gameObject.GetComponent<CircleCollider2D>().radius;
        realativeMaxExpansion = currentExpansionFactor * maxExpansionFactor;
    }

    private void FixedUpdate()
    {
        if(currentExpansionFactor <= realativeMaxExpansion)
        {
            Expand();
        }
        else if (destroyWhenDone)
        {
            Destroy(gameObject);
        }
    }

    public void Expand()
    {
        currentExpansionFactor += expansionSpeed * Time.fixedDeltaTime;

        gameObject.GetComponent<CircleCollider2D>().radius = currentExpansionFactor;
    }
}
