using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootWeapon_Script : NetworkBehaviour
{
    public GameObject bulletSpawnPoint;
    public GameObject bulletPrefab;

    public float bulletSpeed = 10f;
    public float destroyTime = 2f;

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            CmdShoot();
        }
    }

    [Command]
    void CmdShoot()
    {
        GameObject _bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);

        _bullet.GetComponent<Rigidbody2D>().velocity = _bullet.transform.up * bulletSpeed;

        NetworkServer.Spawn(_bullet);

        Destroy(_bullet, destroyTime);
    }
}