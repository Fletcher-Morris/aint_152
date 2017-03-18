using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootWeapon : NetworkBehaviour {

    public GameObject shootObj;
    [SerializeField]
    public LayerMask shootMask;
    public Weapon weapon;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    void Shoot()
    {
        RaycastHit2D _hit = Physics2D.Raycast(shootObj.transform.position, shootObj.transform.up, weapon.weaponRange, shootMask);
        if (_hit.collider != null)
        {
            if(_hit.collider.tag == "RemotePlayer")
            {
                CmdWasShot(_hit.collider.name);
            }
        }
    }

    [Command]
    void CmdWasShot(string _ID)
    {
        Debug.Log(_ID + " was shot.");
    }
}
