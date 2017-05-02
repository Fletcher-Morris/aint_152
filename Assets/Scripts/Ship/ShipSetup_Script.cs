using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;
using UnityStandardAssets.ImageEffects;

public class ShipSetup_Script : MonoBehaviour
{
    public Ship shipDetails;
    public bool isPlayer = false;
    public float healthPercentage;

    public GameObject healthUiText;
    GameObject ShieldsUiText;
	public GameObject shieldOverlaySprite;
	public Color shieldColour;
	private float shieldTransparencey = 0f;

    public GameObject explosionPrefab;
	public GameObject damageIndicatorPrefab;

    float powerRechargeDelayTimer;
    float shieldRechargeDelayTimer;

	private float timeSinceDamageTaken;
	public float damageCollectionTime = .5f;
	private float damageTakenInTime = 0;

    public float shakeDuration = .1f;
    public float shakeMagnitude = 1f;

    void Start()
    {
		SetupAllships ();

        if (isPlayer)
        {
            SetupPlayerShip();
        }
        else
        {
            SetupEnemyShip();
        }
    }

	void SetupAllships()
	{
		shipDetails.shipTurret.weaponsList[0] = (GameObject.Find("GM").GetComponent<WeaponData_Script>().ionBlasterUpgrades [0]);
		timeSinceDamageTaken = damageCollectionTime;
	}

    void SetupEnemyShip()
    {
        isPlayer = false;
        gameObject.transform.tag = "Enemy";
    }

    void SetupPlayerShip()
    {
		isPlayer = true;
        gameObject.transform.tag = "Player";
        shipDetails = GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.playerShip;
        transform.position = shipDetails.shipPos;
        transform.rotation = Quaternion.Euler(shipDetails.shipRot);

        Time.timeScale = 1;

        if (isPlayer)
        {
            UpdateUI();
        }
    }

	public void SavePlayerShip(){
		shipDetails.shipPos = gameObject.transform.position;
		shipDetails.shipRot = gameObject.transform.rotation.eulerAngles;
		GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.playerShip = this.shipDetails;
	}

	public void TakeDamage(float damageAmount)
    {
        if (shipDetails.invincible == false)
        {
            shipDetails.shipHealth -= Mathf.RoundToInt(TakeShieldDamage(damageAmount));

            if (damageAmount > 0)
            {
                IndicateDamage(Mathf.RoundToInt(damageAmount));
            }

            if (shipDetails.shipHealth <= 0)
            {
                shipDetails.shipHealth = 0;
                Debug.Log(gameObject.name + " died!");
                GameObject explosion = GameObject.Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                explosion.transform.localScale = new Vector3(3, 3, 3);

                if (isPlayer)
                {
                    Time.timeScale = 0.2f;
                    GameObject.Find("Pause Menu Canvas").transform.GetChild(3).gameObject.SetActive(true);
                    if (GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.hardcore)
                    {
                        Directory.Delete(Application.dataPath + "/Saves/" + GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.worldName, true);
                    }
                }
                else
                {
                    GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.enemiesDestroyed++;

                    if (GameObject.Find("WM").GetComponent<WorldLoader_Script>().MissionExists("Destroy The Theif"))
                    {
                        if (GameObject.Find("WM").GetComponent<WorldLoader_Script>().FindMission("Destroy The Theif").completed == false)
                        {
                            GameObject.Find("WM").GetComponent<WorldLoader_Script>().CompleteMission("Destroy The Theif");
                        } 
                    }
                }

                if (GetComponent<DropOnDeath_Script>())
                {
                    gameObject.GetComponent<DropOnDeath_Script>().Drop();
                }
                ForceIndicateDamage(damageTakenInTime);

                GameObject.Destroy(gameObject);
            }

            if (isPlayer)
            {
                UpdateUI();
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

	public float TakeShieldDamage(float damageAmount)
    {
        if (shipDetails.shipShield.shieldHealth > 0)
        {
            shipDetails.shipShield.shieldHealth = (shipDetails.shipShield.shieldHealth - damageAmount);
            damageAmount = damageAmount - (damageAmount * shipDetails.shipShield.absorbPercent/100);

			shieldOverlaySprite.GetComponent<SpriteRenderer> ().color = shieldColour;
			shieldTransparencey = shieldColour.a;
        }

        shieldRechargeDelayTimer = shipDetails.shipShield.chargeDelay;

        if (isPlayer)
        {
            UpdateUI();
            StartCoroutine(ShakeCamera());
        }

        return damageAmount;
    }

    public void TakePower(float powerAmount)
    {
        shipDetails.shipReactor.currentPower -= powerAmount;

        powerRechargeDelayTimer = shipDetails.shipReactor.rechargeDelay;
    }

    void UpdateUI()
    {
        GameObject.Find("Health Text").GetComponent<Text>().text = "HEALTH: " + shipDetails.shipHealth;
        GameObject.Find("Power Text").GetComponent<Text>().text = "POWER: " + Mathf.RoundToInt(shipDetails.shipReactor.currentPower);
        GameObject.Find("Shield Text").GetComponent<Text>().text = "SHIELD: " + Mathf.RoundToInt(shipDetails.shipShield.shieldHealth);

		WorldLoader_Script wL = GameObject.Find ("WM").GetComponent<WorldLoader_Script> ();
		GameObject.Find ("Money Text").GetComponent<Text> ().text = "$" + wL.theWorld.money;
		GameObject.Find ("Gold Text").GetComponent<Text> ().text = "GOLD: " + wL.theWorld.gold;
		GameObject.Find ("Score Text").GetComponent<Text> ().text = "SCORE: " + wL.theWorld.score;

        if(healthPercentage <= 20 && healthPercentage > 0)
        {
            Camera.main.gameObject.GetComponent<NoiseAndScratches>().grainIntensityMax = (1 / healthPercentage) * 5;
        }
        else
        {
            Camera.main.gameObject.GetComponent<NoiseAndScratches>().grainIntensityMax = 0;
        }
    }

    private void Update()
    {
        if (isPlayer)
        {
            healthPercentage = (float.Parse(shipDetails.shipHealth.ToString()) / float.Parse(shipDetails.maxShipHealth.ToString()) * 100);
            UpdateUI();
        }

        if (shipDetails.shipReactor.currentPower < shipDetails.shipReactor.maxPower)
        {
            RechargePower(); 
        }
        else
        {
            shipDetails.shipReactor.currentPower = shipDetails.shipReactor.maxPower;
        }

        if(shipDetails.shipHealth >= shipDetails.maxShipHealth)
        {
            shipDetails.shipHealth = shipDetails.maxShipHealth;
        }

		if (shipDetails.shipReactor.currentPower > 0) {
			RechargeShield ();
		} else {
			GetComponent<ShootWeapon_Script> ().StopShoot ();
		}

		timeSinceDamageTaken += Time.deltaTime;
		shieldOverlaySprite.GetComponent<SpriteRenderer> ().color = new Color (shieldColour.r, shieldColour.b, shieldColour.g, shieldTransparencey);
		shieldTransparencey -= Time.deltaTime * 2;
    }

    void RechargePower()
    {
        if(shipDetails.shipReactor.currentPower < 0)
        {
            shipDetails.shipReactor.currentPower = 0;
        }

        powerRechargeDelayTimer = powerRechargeDelayTimer - 1 * Time.deltaTime;
        if (powerRechargeDelayTimer <= 0)
        {
            powerRechargeDelayTimer = 0;
            if (shipDetails.shipReactor.currentPower < shipDetails.shipReactor.maxPower)
            {
                shipDetails.shipReactor.currentPower += shipDetails.shipReactor.rechargeRate * Time.deltaTime;
            }
            else
            {
                shipDetails.shipReactor.currentPower = shipDetails.shipReactor.maxPower;
            }
        }

        if (isPlayer)
        {
            UpdateUI();
        }
    }

    void RechargeShield()
    {
        if (shipDetails.shipShield.shieldHealth < 0)
        {
            shipDetails.shipShield.shieldHealth = 0;
        }

        shieldRechargeDelayTimer = shieldRechargeDelayTimer - 1 * Time.deltaTime;
        if (shieldRechargeDelayTimer <= 0)
        {
            shieldRechargeDelayTimer = 0;
            if (shipDetails.shipShield.shieldHealth < shipDetails.shipShield.maxShieldHealth)
            {
                shipDetails.shipShield.shieldHealth += shipDetails.shipShield.chargeRate * Time.deltaTime;
            }
            else
            {
                shipDetails.shipShield.shieldHealth = shipDetails.shipShield.maxShieldHealth;
            }
        }

        if(shipDetails.shipShield.shieldHealth >= shipDetails.shipShield.maxShieldHealth)
        {
            shipDetails.shipShield.shieldHealth = shipDetails.shipShield.maxShieldHealth;
        }

        if (isPlayer)
        {
            UpdateUI();
        }
    }

    IEnumerator ShakeCamera()
    {

        float elapsed = 0.0f;

        Vector3 originalCamPos = GameObject.Find("Cameras").transform.position;

        while (elapsed < shakeDuration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / shakeDuration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * .2f - .1f;
            float y = Random.value * .2f - .1f;
            x *= shakeMagnitude * damper;
            y *= shakeMagnitude * damper;

            GameObject.Find("Cameras").transform.position = new Vector3(x, y, originalCamPos.z);

            yield return null;
        }

        GameObject.Find("Cameras").transform.position = originalCamPos;
    }
}