using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    public float paralaxDepth = 1;
    public bool usePlayerShipAsTarget = true;

    public GameObject focusObject;

    private void Update()
    {
        if (usePlayerShipAsTarget)
        {
            focusObject = GameObject.Find("Player Ship"); 
        }
        if(focusObject)
            gameObject.transform.position = focusObject.transform.position * paralaxDepth;
    }
}