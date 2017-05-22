using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Need some random object to have health?
//  Just add this script to it.

public class GenericHealth_Script : MonoBehaviour
{
    public int currentHealth = 100;
    public int maxHealth = 100;

    public bool explodeOnDeath = true;
    public GameObject explosionPrefab;
    public float explosionScale = 1;

    public bool destroyOnDeath = true;
	public bool dropItemsOnDeath = true;
    public bool gameOverOnDeath = false;
    public string gameOverMessage;

	public GameObject damageIndicatorPrefab;
    public float timeSinceDamageTaken;
	public float damageCollectionTime = .2f;
	private float damageTakenInTime = 0;

	public void Start()
	{
        currentHealth = maxHealth;
		timeSinceDamageTaken = damageCollectionTime;
	}

	public void TakeDamage(float damageAmount)
    {
		currentHealth -= Mathf.RoundToInt(damageAmount);

		if (damageAmount > 0) {
			IndicateDamage (Mathf.RoundToInt(damageAmount));
		}

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log(gameObject.name + " died!");

            if(timeSinceDamageTaken < damageCollectionTime)
            {
                ForceIndicateDamage(Mathf.RoundToInt(damageTakenInTime));
            }

            if (explodeOnDeath)
            {
				GameObject explosion = GameObject.Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.Euler(0,0, Random.Range(0,360)));
                explosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);
            }

            if (dropItemsOnDeath && GetComponent<DropOnDeath_Script>())
            {
                gameObject.GetComponent<DropOnDeath_Script>().Drop();
            }

            if(gameObject.name == "Asteroid(Clone)" || gameObject.name == "Asteroid 2(Clone)")
            {
                GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.asteroidsDestroyed ++;
                if (GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.asteroidsDestroyed >= 3)
                {
                    if (GameObject.Find("WM").GetComponent<WorldLoader_Script>().MissionExists("Destroy Three Asteroids"))
                    {
                        if (!GameObject.Find("WM").GetComponent<WorldLoader_Script>().FindMission("Destroy Three Asteroids").completed)
                        {
                            //GameObject.Find("WM").GetComponent<WorldLoader_Script>().FindMission("Destroy Three Asteroids").completed = true;
                            GameObject.Find("WM").GetComponent<WorldLoader_Script>().CompleteMission("Destroy Three Asteroids");
                            GameObject.Find("WM").GetComponent<WorldLoader_Script>().ActivateMission("Destroy The Thief");
                            GameObject.Find("WM").GetComponent<WaveManager_Script>().doSpawn = true;
                        } 
                    }
                }
                GameObject.Find("WM").GetComponent<WorldLoader_Script>().SpawnNewAsteroid();
            }
            else if(gameObject.tag == "Enemy")
            {
                GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.asteroidsDestroyed++;
            }

            if (gameOverOnDeath)
            {
                GameObject.Find("Player Ship").GetComponent<ShipSetup_Script>().GameOver(gameOverMessage);
            }

            if (destroyOnDeath)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

	public void IndicateDamage(float rawDamage)
	{
		damageTakenInTime += rawDamage;

		if (timeSinceDamageTaken >= damageCollectionTime) {
            if (damageTakenInTime >= 1)
            {
                ForceIndicateDamage(damageTakenInTime);
                damageTakenInTime = 0;
                timeSinceDamageTaken = 0;
            }
		}
	}

	public void ForceIndicateDamage(float _damageTaken)
	{
		GameObject dmgIndicator = damageIndicatorPrefab;
		dmgIndicator.GetComponent<DamageIndicator_Script> ().damageAmount = _damageTaken;
		Vector2 screenPos = Camera.main.WorldToScreenPoint (new Vector2 (transform.position.x + Random.Range(-.5f, .5f), transform.position.y));
        if (_damageTaken >= 1)
        {
            GameObject.Instantiate(dmgIndicator, screenPos, Quaternion.Euler(0, 0, Random.Range(-10, 10)), GameObject.Find("Player UI Canvas").transform); 
        }
	}


    private void Update()
    {
        timeSinceDamageTaken += Time.deltaTime;

        if (gameObject.tag == "Objective" && GameObject.Find("Player UI Canvas"))
        {
            float fCurrent = currentHealth;
            float fMax = maxHealth;

            GameObject objectiveHealthBar = GameObject.Find("Player UI Canvas").transform.GetChild(6).gameObject;
            objectiveHealthBar.SetActive(true);

            objectiveHealthBar.GetComponent<Slider>().value = (fCurrent / fMax);
            objectiveHealthBar.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Objective Health";
        }
    }
}