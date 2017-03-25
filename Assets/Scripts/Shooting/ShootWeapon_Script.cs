using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootWeapon_Script : NetworkBehaviour
{
    public GameObject bulletSpawnPoint;
    public GameObject bulletPrefab;

    public Weapon thisWeapon;

    public int remainingAmmo;
    public float reloadTimer;
    public bool reloading = false;
    public bool readyToFire = true;
    public bool isTryingToShoot = false;

    float lastShootTime;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        remainingAmmo = thisWeapon.clipSize;
        reloadTimer = thisWeapon.reloadTime;
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        thisWeapon = GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon;

        if (Input.GetMouseButtonDown(0))
        {
            isTryingToShoot = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isTryingToShoot = false;
        }

        if(remainingAmmo == 0)
            StartCoroutine(ReloadGun());

        if (isTryingToShoot)
        {
            if (thisWeapon.auto)
            {
                if (remainingAmmo > 0 && readyToFire && !reloading)
                {
                    CmdShoot();
                }

                ShootDelay();
            }
            else
            {
                if (remainingAmmo > 0 && readyToFire && !reloading)
                {
                    CmdShoot();
                }

                isTryingToShoot = false;

                ShootDelay();
            }
        }
    }

    void ShootDelay()
    {
        readyToFire = false;
        if(Time.time > (lastShootTime + thisWeapon.shootDelay))
        {
            readyToFire = true;
            lastShootTime = Time.time;
        }
    }

    IEnumerator ReloadGun()
    {
        reloading = true;

        yield return new WaitForSeconds(thisWeapon.reloadTime);

        remainingAmmo = thisWeapon.clipSize;
        reloading = false;
    }

    [Command]
    void CmdShoot()
    {
        remainingAmmo = remainingAmmo - 1;

        GameObject _bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);

        _bullet.GetComponent<Rigidbody2D>().velocity = _bullet.transform.up * thisWeapon.bulletSpeed;

        NetworkServer.Spawn(_bullet);

        Destroy(_bullet, thisWeapon.bulletRange/thisWeapon.bulletSpeed);
    }
}