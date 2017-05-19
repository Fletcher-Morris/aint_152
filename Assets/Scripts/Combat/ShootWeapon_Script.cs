using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootWeapon_Script : MonoBehaviour
{
    public GameObject bulletSpawnPoint;
    public GameObject bulletPrefab;
    public GameObject fusionMinePrefab;
    public GameObject hunterMissilePrefab;
	public GameObject prismBeamPrefab;
    public GameObject audioObjectPrefab;
    public AudioClip bulletSound1;
    public AudioClip negativeToneSound;
    public bool canShoot = true;
    public bool isTryingToShoot = false;

    float shootDelayTimer;

	private GameObject myPrismBeam;

    private void Start()
    {
        shootDelayTimer = GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.shootDelay;
    }

    private void Update()
    {
        shootDelayTimer = shootDelayTimer - 1 * Time.deltaTime;
        if (shootDelayTimer <= 0)
            shootDelayTimer = 0;

        if (Input.GetMouseButton(0) && canShoot)
        {
            if (GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.auto)
            {
				AutoShoot ();
            }
        }

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
			Shoot ();
        }

		if (Input.GetMouseButtonUp (0))
		{
			StopShoot ();
		}
    }

	public void Shoot()
	{
		if (GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.weaponType == "Ion Blaster") {
			ShootIonBlaster ();
		}
        else if (GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.weaponType == "Fusion Mine")
        {
            ShootFusionMine();
        }
        else if (GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.weaponType == "Quantum Prism")
        {
			ShootQuantumPrism ();
		}
        else if(GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.weaponType == "Hunter Launcher")
        {
            if (!GetComponent<AudioSource>().isPlaying && gameObject.transform.tag == "Player")
            {
                if (GetComponent<ShipSetup_Script>().shipDetails.shipReactor.currentPower < GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.powerUse)
                {
                    GetComponent<AudioSource>().PlayOneShot(negativeToneSound); 
                }
            }
        }
	}

    public void AutoShoot()
    {
		if (GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.weaponType == "Ion Blaster") {
			ShootIonBlaster ();
		}
        else if (GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.weaponType == "Hunter Launcher")
        {
            ChargeHunterLauncher();
        }
        else if (GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.weaponType == "Fusion Mine")
        {
            ShootFusionMine();
        }
    }

	public void StopShoot()
	{
		if (GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.weaponType == "Quantum Prism") {
			GameObject.Destroy (myPrismBeam);
		}
        else if(GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.weaponType == "Hunter Launcher")
        {
            ReleaseHunterLauncher();
        }
	}


	public void ShootIonBlaster()
	{
		if (shootDelayTimer <= 0)
		{
			GetComponent<ShipSetup_Script>().TakePower(GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.powerUse);

			if (GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.weaponLevel != 6) {
				GameObject _bullet = GameObject.Instantiate (bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
				
				_bullet.GetComponent<Rigidbody2D> ().velocity = _bullet.transform.up * GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletSpeed;
				_bullet.GetComponent<Bullet_Script> ().damage = GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletDamage;
				
				Destroy (_bullet, GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletRange / GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletSpeed);
			} else {
				
				GameObject _bullet1 = GameObject.Instantiate (bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.Euler(bulletSpawnPoint.transform.rotation.eulerAngles.x, bulletSpawnPoint.transform.rotation.eulerAngles.y, bulletSpawnPoint.transform.rotation.eulerAngles.z - 10f));
				GameObject _bullet2 = GameObject.Instantiate (bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.Euler(bulletSpawnPoint.transform.rotation.eulerAngles.x, bulletSpawnPoint.transform.rotation.eulerAngles.y, bulletSpawnPoint.transform.rotation.eulerAngles.z));
				GameObject _bullet3 = GameObject.Instantiate (bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.Euler(bulletSpawnPoint.transform.rotation.eulerAngles.x, bulletSpawnPoint.transform.rotation.eulerAngles.y, bulletSpawnPoint.transform.rotation.eulerAngles.z + 10f));


				_bullet1.GetComponent<Rigidbody2D> ().velocity = _bullet1.transform.up * GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletSpeed;
				_bullet1.GetComponent<Bullet_Script> ().damage = GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletDamage / 2;
				_bullet1.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
				_bullet2.GetComponent<Rigidbody2D> ().velocity = _bullet2.transform.up * GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletSpeed;
				_bullet2.GetComponent<Bullet_Script> ().damage = GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletDamage;
				_bullet3.GetComponent<Rigidbody2D> ().velocity = _bullet3.transform.up * GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletSpeed;
				_bullet3.GetComponent<Bullet_Script> ().damage = GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletDamage / 2;
				_bullet3.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);

				Destroy (_bullet1, GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletRange / GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletSpeed);
				Destroy (_bullet2, GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletRange / GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletSpeed);
				Destroy (_bullet3, GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletRange / GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletSpeed);
			}

            GameObject bulletAudioObject = GameObject.Instantiate(audioObjectPrefab, gameObject.transform.position, gameObject.transform.rotation);
            bulletAudioObject.GetComponent<AudioSource>().volume = GameObject.Find("GM").GetComponent<GamePrefs_Script>().gamePrefs.musicVolumeLevel / 20;
            bulletAudioObject.GetComponent<AudioSource>().PlayOneShot(bulletSound1);
            bulletAudioObject.GetComponent<AutoDestroy_Script>().destroyTime = bulletSound1.length + 0.5f;

			shootDelayTimer = GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.shootDelay;
		}
	}


    public void ShootFusionMine()
    {
        if(shootDelayTimer <= 0 && GetComponent<ShipSetup_Script>().shipDetails.shipReactor.currentPower >= GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.powerUse)
        {
            GetComponent<ShipSetup_Script>().TakePower(GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.powerUse);
            GameObject fusionMineObject = GameObject.Instantiate(fusionMinePrefab, transform.position + new Vector3(0,0,1), transform.rotation);
            fusionMineObject.GetComponent<FusionMine_Script>().damage = GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.bulletDamage;

            shootDelayTimer = GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.shootDelay;
        }
        else if(gameObject.transform.tag == "Player")
        {
            if (!GetComponent<AudioSource>().isPlaying && gameObject.transform.tag == "Player")
            {
                GetComponent<AudioSource>().PlayOneShot(negativeToneSound);
            }
        }
    }

    public void ChargeHunterLauncher()
    {
        if (GetComponent<ShipSetup_Script>().shipDetails.shipReactor.currentPower >= GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.powerUse)
        {
            if (shootDelayTimer <= 0)
            {
                List<GameObject> hunterObjects = new List<GameObject>();

                GetComponent<ShipSetup_Script>().TakePower(GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.powerUse);
                GameObject hunterMissile = GameObject.Instantiate(hunterMissilePrefab, transform.position, transform.rotation, gameObject.transform);
                Destroy(hunterMissile.GetComponent<Rigidbody2D>());
                hunterMissile.GetComponent<AutoDestroy_Script>().enabled = false;
                hunterMissile.GetComponent<HunterMissile_Script>().damage = GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.bulletDamage;
                hunterMissile.GetComponent<Collider2D>().enabled = false;

                foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (gameObj.name == "Hunter Missile(Clone)" && gameObj.transform.IsChildOf(gameObj.transform))
                    {
                        hunterObjects.Add(gameObj);
                    }
                }

                hunterMissile.transform.localPosition = new Vector3(NumberToMissilePos(hunterObjects.Count),0,0);

                hunterMissile.GetComponent<HunterMissile_Script>().damage = GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.bulletDamage;
                shootDelayTimer = GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.shootDelay;
            }
        }
        else
        {
            ReleaseHunterLauncher();
        }
    }

    public void ReleaseHunterLauncher()
    {
        foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (gameObj.name == "Hunter Missile(Clone)")
            {
                gameObj.AddComponent<Rigidbody2D>();
                gameObj.transform.parent = null;
                gameObj.GetComponent<AutoDestroy_Script>().enabled = true;
                gameObj.GetComponent<HunterMissile_Script>().active = true;
                gameObj.GetComponent<Collider2D>().enabled = true;
                gameObj.GetComponent<AudioSource>().enabled = true;
                gameObj.GetComponent<AudioSource>().Play();
            }
        }
    }


    public void ShootQuantumPrism()
	{
		if (GetComponent<ShipSetup_Script> ().shipDetails.shipReactor.currentPower > 0) {
			
			myPrismBeam = GameObject.Instantiate (prismBeamPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation, bulletSpawnPoint.gameObject.transform);

			myPrismBeam.GetComponent<QuantumPrismBeam_Script> ().maxIntensity = gameObject.GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.bulletDamage;

			myPrismBeam.GetComponent<QuantumPrismBeam_Script> ().controllerObject = this.gameObject;

		}
	}

    public int NumberToMissilePos(int input)
    {
        float temp = input;

        if(input >= 3)
        {
            temp -= 0.5f;
        }

        if(input % 2 == 0)
        {
            temp -= 1;
            temp *= -1;
        }

        return Mathf.RoundToInt(temp);
    }
}