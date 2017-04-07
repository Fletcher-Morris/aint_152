using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData_Script : MonoBehaviour
{
	[SerializeField]
	public WeaponUpgrades weaponUpgrades;

	void Awake()
	{
		weaponUpgrades = weaponUpgrades.LoadUpgrades ();
	}
}