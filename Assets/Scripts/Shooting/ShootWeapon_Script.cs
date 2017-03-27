using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootWeapon_Script : MonoBehaviour
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
        remainingAmmo = thisWeapon.clipSize;
        reloadTimer = thisWeapon.reloadTime;

        GameObject.Find("Weapon Ammo Text").GetComponent<Text>().text = remainingAmmo + "/" + thisWeapon.clipSize;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void OnRemainingAmmoChange(int _remainingAmmo)
    {
        GameObject.Find("Weapon Ammo Text").GetComponent<Text>().text = remainingAmmo + "/" + thisWeapon.clipSize;
    }
    void Shoot()
    {
        remainingAmmo = remainingAmmo - 1;

        GameObject _bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);

        _bullet.GetComponent<Rigidbody2D>().velocity = _bullet.transform.up * thisWeapon.bulletSpeed;
        _bullet.GetComponent<Bullet_Script>().damage = thisWeapon.bulletDamage;

        Destroy(_bullet, thisWeapon.bulletRange / thisWeapon.bulletSpeed);
    }
}