using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootWeapon_Script : MonoBehaviour
{
    public GameObject bulletSpawnPoint;
    public GameObject bulletPrefab;
    public bool isTryingToShoot = false;

    float shootDelayTimer;

    private void Start()
    {
        shootDelayTimer = GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.shootDelay;
    }

    private void Update()
    {
        shootDelayTimer = shootDelayTimer - 1 * Time.deltaTime;
        if (shootDelayTimer <= 0)
            shootDelayTimer = 0;

        if (Input.GetMouseButton(0))
        {
            if (GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.auto)
            {
                if (shootDelayTimer == 0)
                {
                    Shoot();
                } 
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (shootDelayTimer == 0)
            {
                Shoot();
            }
        }
    }
    public void Shoot()
    {
        if (shootDelayTimer <= 0)
        {
            GetComponent<ShipSetup_Script>().TakePower(GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.powerUse);

            GameObject _bullet = GameObject.Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);

            _bullet.GetComponent<Rigidbody2D>().velocity = _bullet.transform.up * GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.bulletSpeed;
            _bullet.GetComponent<Bullet_Script>().damage = GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.bulletDamage;

            Destroy(_bullet, GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.bulletRange / GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.bulletSpeed);

            shootDelayTimer = GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.shootDelay;
        }
    }
}