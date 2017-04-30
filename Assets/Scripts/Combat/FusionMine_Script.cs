﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionMine_Script : MonoBehaviour {

    public float detectionRange = 5;
    public float explosionRange = 1;
    public float moveSpeed = .05f;

    public GameObject targetObject;

    public GameObject explosionPrefab;

    private void Update()
    {
        if (!targetObject)
        {
            targetObject = SearchForTarget();
        }
        else
        {
            if(Vector2.Distance(targetObject.transform.position, gameObject.transform.position) <= explosionRange)
            {
                Explode();
            }

            else
            {
                MoveToTarget();
            }
        }
    }

    public GameObject SearchForTarget()
    {
        GameObject tempTarget = new GameObject();

        List<GameObject> avaliableTargets = new List<GameObject>();

        foreach(GameObject possibbleTarget in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if(Vector2.Distance(possibbleTarget.transform.position, gameObject.transform.position) <= detectionRange)
            {
                avaliableTargets.Add(possibbleTarget);
            }
        }

        int chosenIndex = Random.Range(0, avaliableTargets.Count - 1);

        if (avaliableTargets.Count >= 1)
        {
            return avaliableTargets[chosenIndex]; 
        }
        else
        {
            return null;
        }
    }

    public void MoveToTarget()
    {
        if(moveSpeed > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetObject.transform.position, moveSpeed);
        }
    }

    public void Explode()
    {
        GameObject.Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}