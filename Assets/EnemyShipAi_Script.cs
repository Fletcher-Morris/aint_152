using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAi_Script : MonoBehaviour
{
    public float enemyDetectionRange = 30;
    public float rotateSpeed = 50f;

    public Ship shipDetails;

    public GameObject targetEnemy;
    public float currentEnemyRange;

    private void Update()
    {
        if (!targetEnemy)
        {
            SearchForEnemy();
        }
        else
        {
            currentEnemyRange = Vector2.Distance(targetEnemy.transform.position, gameObject.transform.position);

            if (Vector2.Distance(targetEnemy.transform.position, gameObject.transform.position) > enemyDetectionRange)
            {
                targetEnemy = null;
            }

            Movement();
        }
    }

    void SearchForEnemy()
    {
        foreach (GameObject _foundObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(Vector2.Distance(_foundObject.transform.position, gameObject.transform.position) <= enemyDetectionRange)
            {
                targetEnemy = _foundObject;
            }
        }
    }

    void Movement()
    {
        RotateShip();
    }

    void RotateShip()
    {

    }

    void ShootGun()
    {

    }
}