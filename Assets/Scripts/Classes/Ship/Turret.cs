using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Turret
{
    public string turretName;
    public Weapon turretWeapon;
    public float rotationSpeed;

	public Weapon[] weaponsList;

    public Turret()
    {
        turretName = "New Turret";
        turretWeapon = new Weapon();
        rotationSpeed = 120f;
		weaponsList = new Weapon[8];
		weaponsList [0] = new Weapon ();
		WeaponUpgrades upgradesList = new WeaponUpgrades ();
		upgradesList.SetDefaults ();
		weaponsList [0] = upgradesList.ionBlaster [0];
		weaponsList [1] = upgradesList.quantumPrism [0];
    }
}
