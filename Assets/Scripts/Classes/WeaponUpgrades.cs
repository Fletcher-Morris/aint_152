using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class WeaponUpgrades
{
	public Weapon[] ionBlaster;
	public Weapon[] quantumPrism;

	public WeaponUpgrades()
	{
		this.SetDefaults ();
	}

	public void SetDefaults()
	{
		SetIonBlaster ();
		SetQuantumPrism ();
	}

	public void SetIonBlaster()
	{

		ionBlaster = new Weapon[5];

		ionBlaster [0] = new Weapon ();
		ionBlaster [0].auto = false;
		ionBlaster [0].bulletDamage = 5;
		ionBlaster [0].bulletSpeed = 10;
		ionBlaster [0].powerUse = 2;
		ionBlaster [0].weaponName = "Ion Blaster";
		ionBlaster [0].weaponType = "Ion Blaster";
		ionBlaster [0].weaponLevel = 1;

		ionBlaster [1] = new Weapon ();
		ionBlaster [1].auto = true;
		ionBlaster [1].bulletDamage = 7;
		ionBlaster [1].bulletSpeed = 10;
		ionBlaster [1].powerUse = 2;
		ionBlaster [1].weaponName = "Ion Blaster";
		ionBlaster [1].weaponType = "Ion Blaster";
		ionBlaster [1].weaponLevel = 2;

		ionBlaster [2] = new Weapon ();
		ionBlaster [2].auto = true;
		ionBlaster [2].bulletDamage = 10;
		ionBlaster [2].bulletSpeed = 10;
		ionBlaster [2].powerUse = 3;
		ionBlaster [2].weaponName = "Ion Blaster";
		ionBlaster [2].weaponType = "Ion Blaster";
		ionBlaster [2].weaponLevel = 3;

		ionBlaster [3] = new Weapon ();
		ionBlaster [3].auto = true;
		ionBlaster [3].bulletDamage = 12;
		ionBlaster [3].bulletSpeed = 10;
		ionBlaster [3].powerUse = 3;
		ionBlaster [3].weaponName = "Ion Blaster";
		ionBlaster [3].weaponType = "Ion Blaster";
		ionBlaster [3].weaponLevel = 4;

		ionBlaster [4] = new Weapon ();
		ionBlaster [4].auto = true;
		ionBlaster [4].bulletDamage = 15;
		ionBlaster [4].bulletSpeed = 15;
		ionBlaster [4].powerUse = 3;
		ionBlaster [4].weaponName = "Plasma Blaster";
		ionBlaster [4].weaponType = "Ion Blaster";
		ionBlaster [4].weaponLevel = 5;

	}

	public void SetQuantumPrism()
	{
		quantumPrism = new Weapon[5];

		quantumPrism [0] = new Weapon ();
		quantumPrism [0].auto = false;
		quantumPrism [0].bulletDamage = 1;
		quantumPrism [0].bulletSpeed = 100;
		quantumPrism [0].powerUse = 0;
		quantumPrism [0].weaponName = "Quantum Prism";
		quantumPrism [0].weaponType = "Quantum Prism";
		quantumPrism [0].weaponLevel = 1;

		quantumPrism [1] = new Weapon ();
		quantumPrism [1].auto = false;
		quantumPrism [1].bulletDamage = 2;
		quantumPrism [1].bulletSpeed = 100;
		quantumPrism [1].powerUse = 30;
		quantumPrism [1].weaponName = "Quantum Prism";
		quantumPrism [1].weaponType = "Quantum Prism";
		quantumPrism [1].weaponLevel = 2;

		quantumPrism [2] = new Weapon ();
		quantumPrism [2].auto = false;
		quantumPrism [2].bulletDamage = 3;
		quantumPrism [2].bulletSpeed = 100;
		quantumPrism [2].powerUse = 30;
		quantumPrism [2].weaponName = "Quantum Prism";
		quantumPrism [2].weaponType = "Quantum Prism";
		quantumPrism [2].weaponLevel = 3;

		quantumPrism [3] = new Weapon ();
		quantumPrism [3].auto = false;
		quantumPrism [3].bulletDamage = 4;
		quantumPrism [3].bulletSpeed = 100;
		quantumPrism [3].powerUse = 40;
		quantumPrism [3].weaponName = "Quantum Prism";
		quantumPrism [3].weaponType = "Quantum Prism";
		quantumPrism [3].weaponLevel = 4;

		quantumPrism [4] = new Weapon ();
		quantumPrism [4].auto = false;
		quantumPrism [4].bulletDamage = 5;
		quantumPrism [4].bulletSpeed = 100;
		quantumPrism [4].powerUse = 40;
		quantumPrism [4].weaponName = "Hyper Prism";
		quantumPrism [4].weaponType = "Quantum Prism";
		quantumPrism [4].weaponLevel = 5;
	}


	public void SaveUpgrades()
	{
		SetDefaults ();

		string jsonString = JsonUtility.ToJson(this);
		try
		{
			File.WriteAllText(Application.dataPath + "/Data/Weapon Data.json", jsonString.ToString());
			Debug.Log("Saving world file.");
		}
		catch (System.Exception)
		{
			Debug.LogWarning("Cannot find world file. Creating a new one.");
			Directory.CreateDirectory(Application.dataPath + "/Data");
			SaveUpgrades();
		}
	}

	public WeaponUpgrades LoadUpgrades()
	{
		WeaponUpgrades _upgrades = new WeaponUpgrades();
		try
		{
			string jsonString = File.ReadAllText(Application.dataPath + "/Data/Weapon Data.json");
			_upgrades = JsonUtility.FromJson<WeaponUpgrades>(jsonString);
		}
		catch (System.Exception)
		{
			SaveUpgrades();
		}
		Debug.Log("Loading world file.");
		return _upgrades;
	}
}