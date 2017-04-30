using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootWeapon_Script : MonoBehaviour
{
    public GameObject bulletSpawnPoint;
    public GameObject bulletPrefab;
    public GameObject fusionMinePrefab;
	public GameObject prismBeamPrefab;
    public GameObject audioObjectPrefab;
    public AudioClip bulletSound1;
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
	}

    public void AutoShoot()
    {
		if (GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.weaponType == "Ion Blaster") {
			ShootIonBlaster ();
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
            GameObject.Instantiate(fusionMinePrefab, transform.position, transform.rotation);
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
}