﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDeath_Script : MonoBehaviour
{
	public bool dropMoney = false;
	public int moneyMin = 50;
	public int moneyMax = 200;
	public GameObject moneyPrefab;

	public bool dropHealth = false;
	public float dropChance = 0.5f;
	public GameObject healthPrefab;

    public bool dropCustom = false;
    public GameObject customDropPrefab;
    public int customDropQuantity = 4;

    public bool dropWithCurrentVelocity = true;

	public float positionVariation = 1f;

	public void Drop()
	{
		if (dropMoney && moneyPrefab) {

			int moneyAmount = Mathf.RoundToInt(Random.Range(moneyMin, moneyMax));

			for (int i = 1; i <= moneyAmount / 10; i++)
			{
				GameObject.Instantiate (moneyPrefab, new Vector3(gameObject.transform.position.x + Random.Range(0, positionVariation), gameObject.transform.position.y + Random.Range(0, positionVariation), 0), transform.rotation);
			}
		}

		if (dropHealth && healthPrefab) {
            if(Random.Range(0f, 1f) <= dropChance)
            {
                GameObject.Instantiate(healthPrefab, new Vector3(gameObject.transform.position.x + Random.Range(0, positionVariation), gameObject.transform.position.y + Random.Range(0, positionVariation), 0), transform.rotation);
            }
		}

        if(dropCustom && customDropPrefab)
        {
            for(int i = 0; i < customDropQuantity; i++)
            {
                GameObject droppedObject = GameObject.Instantiate(customDropPrefab, new Vector3(gameObject.transform.position.x + Random.Range(0, positionVariation), gameObject.transform.position.y + Random.Range(0, positionVariation), 0), transform.rotation);
                if (dropWithCurrentVelocity && droppedObject.GetComponent<Rigidbody2D>())
                {
                    droppedObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
                }
            }
        }
	}
}