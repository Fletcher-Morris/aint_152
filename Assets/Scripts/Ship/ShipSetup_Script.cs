﻿using System.Collections;
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
	public float damageCollectionTime = .2f;
	private float damageTakenInTime = 0;

    public float shakeDuration = .1f;
    public float shakeMagnitude = 1f;

    public Sprite invertedIonBlaster;
    public Sprite invertedFusionMine;
    public Sprite invertedHunterLauncherSprite;
    public Sprite invertedQuantumPrismSprite;

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

        Time.timeScale = 1;

        UpdateUI();
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
                    GameOver("YOUR SHIP WAS DESTROYED");
                }
                else
                {
                    if (GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.enemiesDestroyed == 0)
                    {
                        if (GameObject.Find("WM").GetComponent<WorldLoader_Script>().MissionExists("Destroy The Thief"))
                        {
                            GameObject.Find("WM").GetComponent<WorldLoader_Script>().CompleteMission("Destroy The Thief");
                            GameObject.Find("WM").GetComponent<WaveManager_Script>().doSpawn = false;
                            GameObject.Find("WM").GetComponent<WorldLoader_Script>().ActivateMission("Buy A New Weapon");
                            GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.money += 1000; 
                        }
                    }
                }

                if (GetComponent<DropOnDeath_Script>())
                {
                    gameObject.GetComponent<DropOnDeath_Script>().Drop();
                }

                if (timeSinceDamageTaken < damageCollectionTime)
                {
                    ForceIndicateDamage(Mathf.RoundToInt(damageTakenInTime));
                }

                GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.enemiesDestroyed++;

                GameObject.Destroy(gameObject);
            }

            if (isPlayer)
            {
                UpdateUI();
            } 
        }
    }

    public void GameOver(string causeMessage)
    {
        GameObject gameOverPanel = GameObject.Find("Pause Menu Canvas").transform.GetChild(3).gameObject;
        Time.timeScale = 0.2f;

        gameOverPanel.transform.gameObject.SetActive(true);
        GameObject.Find("Death Panel").transform.GetChild(3).gameObject.SetActive(false);
        gameOverPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = causeMessage;

        if (GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.hardcore)
        {
            File.Delete(Application.dataPath + "/Data/Saves/" + GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.worldName);
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
        GameObject powerTextObject = GameObject.Find("Power Text");
        GameObject shieldTextObject = GameObject.Find("Shield Text");
        GameObject healthTextObject = GameObject.Find("Health Text");

        float maxHealth = shipDetails.maxShipHealth;
        float currentHealth = shipDetails.shipHealth;
        healthTextObject.GetComponent<Text>().text = currentHealth.ToString();
        healthTextObject.transform.parent.GetChild(1).GetChild(0).GetChild(0).GetComponent<Slider>().value = currentHealth / maxHealth;
        healthTextObject.transform.parent.GetChild(2).GetComponent<Slider>().value = 1 - (currentHealth / maxHealth);

        float maxPower = shipDetails.shipReactor.maxPower;
        float currentPower = shipDetails.shipReactor.currentPower;
        powerTextObject.GetComponent<Text>().text = Mathf.RoundToInt(currentPower).ToString();
        powerTextObject.transform.parent.GetChild(1).GetChild(0).GetChild(0).GetComponent<Slider>().value = currentPower / maxPower;
        powerTextObject.transform.parent.GetChild(2).GetComponent<Slider>().value = 1 - (currentPower / maxPower);

        float maxShield = shipDetails.shipShield.maxShieldHealth;
        float currentShield = shipDetails.shipShield.shieldHealth;
        shieldTextObject.GetComponent<Text>().text = Mathf.RoundToInt(currentShield).ToString();
        shieldTextObject.transform.parent.GetChild(1).GetChild(0).GetChild(0).GetComponent<Slider>().value = currentShield / maxShield;
        shieldTextObject.transform.parent.GetChild(2).GetComponent<Slider>().value = 1 - (currentShield / maxShield);
        
		WorldLoader_Script wL = GameObject.Find ("WM").GetComponent<WorldLoader_Script> ();
		GameObject.Find ("Money Text").GetComponent<Text> ().text = wL.theWorld.money.ToString();

        float _currentWeaponProgress = SafeDivideByZero(shipDetails.shipTurret.turretWeapon.weaponExperience, shipDetails.shipTurret.turretWeapon.experienceCap);

        GameObject.Find ("Weapon Progress Text").GetComponent<Text> ().text = Mathf.RoundToInt(_currentWeaponProgress * 100).ToString() + "%";
        GameObject.Find("Weapon Progress Text").transform.parent.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Slider>().value = _currentWeaponProgress;
        GameObject.Find("Weapon Progress Text").transform.parent.GetChild(1).gameObject.GetComponent<Slider>().value = 1-_currentWeaponProgress;

        if (shipDetails.shipTurret.turretWeapon.weaponType == "Ion Blaster")
        {
            GameObject.Find("Weapon Progress Text").transform.parent.GetChild(1).gameObject.GetComponent<Image>().sprite = GameObject.Find("Weapon Wheel").GetComponent<WeaponWheel_Script>().ionBlasterSprite;
            GameObject.Find("Weapon Progress Text").transform.parent.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = invertedIonBlaster;
        }
        else if (shipDetails.shipTurret.turretWeapon.weaponType == "Fusion Mine")
        {
            GameObject.Find("Weapon Progress Text").transform.parent.GetChild(1).gameObject.GetComponent<Image>().sprite = GameObject.Find("Weapon Wheel").GetComponent<WeaponWheel_Script>().fusionMineSprite;
            GameObject.Find("Weapon Progress Text").transform.parent.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = invertedFusionMine;
        }
        else if (shipDetails.shipTurret.turretWeapon.weaponType == "Hunter Launcher")
        {
            GameObject.Find("Weapon Progress Text").transform.parent.GetChild(1).gameObject.GetComponent<Image>().sprite = GameObject.Find("Weapon Wheel").GetComponent<WeaponWheel_Script>().hunterLauncherSprite;
            GameObject.Find("Weapon Progress Text").transform.parent.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = invertedHunterLauncherSprite;
        }
        else if (shipDetails.shipTurret.turretWeapon.weaponType == "Quantum Prism")
        {
            GameObject.Find("Weapon Progress Text").transform.parent.GetChild(1).gameObject.GetComponent<Image>().sprite = GameObject.Find("Weapon Wheel").GetComponent<WeaponWheel_Script>().quantumPrismSprite;
            GameObject.Find("Weapon Progress Text").transform.parent.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = invertedQuantumPrismSprite;
        }

        if (healthPercentage <= 20 && healthPercentage > 0)
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
            float x = Random.value * .2f - .1f;
            float y = Random.value * .2f - .1f;
            x *= shakeMagnitude * damper;
            y *= shakeMagnitude * damper;

            GameObject.Find("Cameras").transform.position = new Vector3(x, y, originalCamPos.z);

            yield return null;
        }

        GameObject.Find("Cameras").transform.position = originalCamPos;
    }

    public float SafeDivideByZero(float a, float b)
    {
        if(b != 0)
        {
            return (a / b);
        }
        else
        {
            return 0;
        }
    }
}