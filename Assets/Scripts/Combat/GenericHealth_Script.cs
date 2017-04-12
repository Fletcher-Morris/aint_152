using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHealth_Script : MonoBehaviour
{
    public int currentHealth = 100;

    public bool explodeOnDeath = true;
    public GameObject explosionPrefab;
    public float explosionScale = 1;

    public bool destroyOnDeath = true;
	public bool dropItemsOnDeath = true;

	public GameObject damageIndicatorPrefab;
	private float timeSinceDamageTaken;
	private float damageCollectionTime = .5f;
	private float damageTakenInTime = 0;

	public void Start()
	{
		timeSinceDamageTaken = damageCollectionTime;
	}

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

		if (damageAmount > 0) {
			IndicateDamage (damageAmount);
		}

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log(gameObject.name + " died!");

			ForceIndicateDamage (damageTakenInTime);

            if (explodeOnDeath)
            {
				GameObject explosion = GameObject.Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.Euler(0,0, Random.Range(0,360)));
                explosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);
            }

            if (destroyOnDeath)
            {
                GameObject.Destroy(gameObject);
            }

			if (dropItemsOnDeath && GetComponent<DropOnDeath_Script> ()) {
				gameObject.GetComponent<DropOnDeath_Script> ().Drop ();
			}
        }
    }

	public void IndicateDamage(float rawDamage)
	{
		damageTakenInTime += rawDamage;

		if (timeSinceDamageTaken >= damageCollectionTime) {
			ForceIndicateDamage (damageTakenInTime);
			damageTakenInTime = 0;
			timeSinceDamageTaken = 0;
		}
	}

	public void ForceIndicateDamage(float _damageTaken)
	{
		GameObject dmgIndicator = damageIndicatorPrefab;
		dmgIndicator.GetComponent<DamageIndicator_Script> ().damageAmount = _damageTaken;
		Vector2 screenPos = Camera.main.WorldToScreenPoint (new Vector2 (transform.position.x + Random.Range(-.5f, .5f), transform.position.y));
		GameObject.Instantiate (dmgIndicator, screenPos, Quaternion.Euler(0,0,Random.Range(-10, 10)), GameObject.Find ("Player UI Canvas").transform);
	}
}