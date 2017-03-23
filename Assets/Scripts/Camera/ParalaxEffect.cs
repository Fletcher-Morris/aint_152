using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    public float paralaxDepth = 1;

    public GameObject playerObject;

    private void Start()
    {
        playerObject = GameObject.Find("PlayerShip");
    }

    private void Update()
    {
        playerObject = GameObject.Find("PlayerShip");
        gameObject.transform.position = playerObject.transform.position * paralaxDepth;
    }
}