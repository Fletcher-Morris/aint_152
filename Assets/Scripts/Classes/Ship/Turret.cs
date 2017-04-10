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
        rotationSpeed = 120f;

		weaponsList = new Weapon[8];

		weaponsList [0] = new Weapon("null");
		weaponsList [1] = new Weapon("null");
		weaponsList [2] = new Weapon("null");
		weaponsList [3] = new Weapon("null");
		weaponsList [4] = new Weapon("null");
		weaponsList [5] = new Weapon("null");
		weaponsList [6] = new Weapon("null");
		weaponsList [7] = new Weapon("null");

		turretWeapon = weaponsList [0];
    }

	public void AddWeapon(Weapon _weapon)
	{
		for (int i = 1; i <= weaponsList.Length; i++) {
			if (weaponsList [i-1].weaponType == "null") {
				weaponsList [i-1] = _weapon;
				break;
			}
		}
	}
}
