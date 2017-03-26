using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ShootWeapon_Script : NetworkBehaviour
{
    public GameObject bulletSpawnPoint;
    public GameObject bulletPrefab;

    public Weapon thisWeapon;

    [SyncVar(hook = "OnRemainingAmmoChange")]
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

        GameObject.Find("Weapon Ammo Text").GetComponent<Text>().text = remainingAmmo + "/" + thisWeapon.clipSize;
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
            if (GetComponent<ShipSetup_Script>().cockpitBeingUsed)
            {
                CmdShoot(); 
            }
        }
    }

    void OnRemainingAmmoChange(int _remainingAmmo)
    {
        if (isLocalPlayer)
        {
            GameObject.Find("Weapon Ammo Text").GetComponent<Text>().text = remainingAmmo + "/" + thisWeapon.clipSize;
        }
    }

    [Command]
    void CmdShoot()
    {
        remainingAmmo = remainingAmmo - 1;

        GameObject _bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);

        _bullet.GetComponent<Rigidbody2D>().velocity = _bullet.transform.up * thisWeapon.bulletSpeed;
        _bullet.GetComponent<Bullet_Script>().damage = thisWeapon.bulletDamage;

        NetworkServer.Spawn(_bullet);

        Destroy(_bullet, thisWeapon.bulletRange / thisWeapon.bulletSpeed);
    }
}