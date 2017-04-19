using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class WeaponUpgrades
{
	public Weapon[] ionBlaster;
	public Weapon[] fusionMine;
	public Weapon[] hunterLauncher;
	public Weapon[] quantumPrism;

	public WeaponUpgrades()
	{
		
	}

	public void SetDefaults()
	{
		SetIonBlaster ();
		SetFusionMine ();
		SetHunterLauncher ();
		SetQuantumPrism ();
	}

	public void SetIonBlaster()
	{

		ionBlaster = new Weapon[6];

		ionBlaster [0] = new Weapon ();
		ionBlaster [0].auto = false;
		ionBlaster [0].bulletDamage = 5;
		ionBlaster [0].bulletSpeed = 10;
		ionBlaster [0].shootDelay = 0.4f;
		ionBlaster [0].powerUse = 2;
		ionBlaster [0].weaponName = "Ion Blaster";
		ionBlaster [0].weaponDescription = "This peashooter of a gun is so reliable that it even works without a power source. Frankly, our scientists are baffled.";
		ionBlaster [0].weaponType = "Ion Blaster";
		ionBlaster [0].weaponLevel = 1;
		ionBlaster [0].weaponValue = 1000;

		ionBlaster [1] = new Weapon ();
		ionBlaster [1].auto = true;
		ionBlaster [1].bulletDamage = 7;
		ionBlaster [1].bulletSpeed = 10;
		ionBlaster [1].shootDelay = 0.4f;
		ionBlaster [1].powerUse = 2;
		ionBlaster [1].weaponName = "Ion Blaster";
		ionBlaster [1].weaponDescription = "Everybody's favourite weapons-grade peashooter, now in an automatic variety.";
		ionBlaster [1].weaponType = "Ion Blaster";
		ionBlaster [1].weaponLevel = 2;
		ionBlaster [1].weaponValue = 1000;

		ionBlaster [2] = new Weapon ();
		ionBlaster [2].auto = true;
		ionBlaster [2].bulletDamage = 10;
		ionBlaster [2].bulletSpeed = 10;
		ionBlaster [2].shootDelay = 0.4f;
		ionBlaster [2].powerUse = 3;
		ionBlaster [2].weaponName = "Ion Blaster";
		ionBlaster [2].weaponDescription = "Ion Blaster lvl 3.";
		ionBlaster [2].weaponType = "Ion Blaster";
		ionBlaster [2].weaponLevel = 3;
		ionBlaster [2].weaponValue = 1000;

		ionBlaster [3] = new Weapon ();
		ionBlaster [3].auto = true;
		ionBlaster [3].bulletDamage = 12;
		ionBlaster [3].bulletSpeed = 10;
		ionBlaster [3].shootDelay = 0.3f;
		ionBlaster [3].powerUse = 3;
		ionBlaster [3].weaponName = "Ion Blaster";
		ionBlaster [3].weaponDescription = "Ion Blaster lvl 4.";
		ionBlaster [3].weaponType = "Ion Blaster";
		ionBlaster [3].weaponLevel = 4;
		ionBlaster [3].weaponValue = 1000;

		ionBlaster [4] = new Weapon ();
		ionBlaster [4].auto = true;
		ionBlaster [4].bulletDamage = 15;
		ionBlaster [4].bulletSpeed = 15;
		ionBlaster [4].shootDelay = 0.3f;
		ionBlaster [4].powerUse = 3;
		ionBlaster [4].weaponName = "Plasma Blaster";
		ionBlaster [4].weaponDescription = "Now shooting searng-hot rounds of plasma, this blaster is no longer permitted in schools.";
		ionBlaster [4].weaponType = "Ion Blaster";
		ionBlaster [4].weaponLevel = 5;
		ionBlaster [4].weaponValue = 1000;

		ionBlaster [5] = new Weapon ();
		ionBlaster [5].auto = true;
		ionBlaster [5].bulletDamage = 15;
		ionBlaster [5].bulletSpeed = 15;
		ionBlaster [5].shootDelay = 0.4f;
		ionBlaster [5].powerUse = 3;
		ionBlaster [5].weaponName = "Plasma Blaster";
		ionBlaster [5].weaponDescription = "The Omega Plasma Blaster fires two extra rounds of super-heated matter with every shot, dramatically increasing the damage output!";
		ionBlaster [5].weaponType = "Ion Blaster";
		ionBlaster [5].weaponLevel = 6;
		ionBlaster [5].weaponValue = 1000;

	}

	public void SetFusionMine()
	{

		fusionMine = new Weapon[5];

		fusionMine [0] = new Weapon ();
		fusionMine [0].auto = false;
		fusionMine [0].bulletDamage = 5;
		fusionMine [0].bulletSpeed = 10;
		fusionMine [0].powerUse = 2;
		fusionMine [0].weaponName = "Fusion Mine";
		fusionMine [0].weaponDescription = "Leave these adorably small nuclear explosives behind your spaceship and watch those pesky Pirates fail to avoid them!";
		fusionMine [0].weaponType = "Fusion Mine";
		fusionMine [0].weaponLevel = 1;
		fusionMine [0].weaponValue = 5000;

		fusionMine [1] = new Weapon ();
		fusionMine [1].auto = false;
		fusionMine [1].bulletDamage = 5;
		fusionMine [1].bulletSpeed = 10;
		fusionMine [1].powerUse = 2;
		fusionMine [1].weaponName = "Fusion Mine";
		fusionMine [1].weaponDescription = "Hunter Launcher lvl 2";
		fusionMine [1].weaponType = "Fusion Mine";
		fusionMine [1].weaponLevel = 2;

		fusionMine [2] = new Weapon ();
		fusionMine [2].auto = false;
		fusionMine [2].bulletDamage = 5;
		fusionMine [2].bulletSpeed = 10;
		fusionMine [2].powerUse = 2;
		fusionMine [2].weaponName = "Fusion Mine";
		fusionMine [2].weaponDescription = "Hunter Launcher lvl 3";
		fusionMine [2].weaponType = "Fusion Mine";
		fusionMine [2].weaponLevel = 3;

		fusionMine [3] = new Weapon ();
		fusionMine [3].auto = false;
		fusionMine [3].bulletDamage = 5;
		fusionMine [3].bulletSpeed = 10;
		fusionMine [3].powerUse = 2;
		fusionMine [3].weaponName = "Fusion Mine";
		fusionMine [3].weaponDescription = "Hunter Launcher lvl 4";
		fusionMine [3].weaponType = "Fusion Mine";
		fusionMine [3].weaponLevel = 4;

		fusionMine [4] = new Weapon ();
		fusionMine [4].auto = false;
		fusionMine [4].bulletDamage = 5;
		fusionMine [4].bulletSpeed = 10;
		fusionMine [4].powerUse = 2;
		fusionMine [4].weaponName = "Fusion Mine";
		fusionMine [4].weaponDescription = "Hunter Launcher lvl 5";
		fusionMine [4].weaponType = "Fusion Mine";
		fusionMine [4].weaponLevel = 5;

	}

	public void SetHunterLauncher()
	{

		hunterLauncher = new Weapon[5];

		hunterLauncher [0] = new Weapon ();
		hunterLauncher [0].auto = false;
		hunterLauncher [0].bulletDamage = 5;
		hunterLauncher [0].bulletSpeed = 10;
		hunterLauncher [0].powerUse = 2;
		hunterLauncher [0].weaponName = "Hunter Launcher";
		hunterLauncher [0].weaponDescription = "Description? What description?";
		hunterLauncher [0].weaponType = "Hunter Launcher";
		hunterLauncher [0].weaponLevel = 1;
		hunterLauncher [0].weaponValue = 15000;

		hunterLauncher [1] = new Weapon ();
		hunterLauncher [1].auto = false;
		hunterLauncher [1].bulletDamage = 5;
		hunterLauncher [1].bulletSpeed = 10;
		hunterLauncher [1].powerUse = 2;
		hunterLauncher [1].weaponName = "Hunter Launcher";
		hunterLauncher [1].weaponDescription = "Hunter Launcher lvl 2";
		hunterLauncher [1].weaponType = "Hunter Launcher";
		hunterLauncher [1].weaponLevel = 2;

		hunterLauncher [2] = new Weapon ();
		hunterLauncher [2].auto = false;
		hunterLauncher [2].bulletDamage = 5;
		hunterLauncher [2].bulletSpeed = 10;
		hunterLauncher [2].powerUse = 2;
		hunterLauncher [2].weaponName = "Hunter Launcher";
		hunterLauncher [2].weaponDescription = "Hunter Launcher lvl 3";
		hunterLauncher [2].weaponType = "Hunter Launcher";
		hunterLauncher [2].weaponLevel = 3;

		hunterLauncher [3] = new Weapon ();
		hunterLauncher [3].auto = false;
		hunterLauncher [3].bulletDamage = 5;
		hunterLauncher [3].bulletSpeed = 10;
		hunterLauncher [3].powerUse = 2;
		hunterLauncher [3].weaponName = "Hunter Launcher";
		hunterLauncher [3].weaponDescription = "Hunter Launcher lvl 4";
		hunterLauncher [3].weaponType = "Hunter Launcher";
		hunterLauncher [3].weaponLevel = 4;

		hunterLauncher [4] = new Weapon ();
		hunterLauncher [4].auto = false;
		hunterLauncher [4].bulletDamage = 5;
		hunterLauncher [4].bulletSpeed = 10;
		hunterLauncher [4].powerUse = 2;
		hunterLauncher [4].weaponName = "Hunter Launcher";
		hunterLauncher [4].weaponDescription = "Hunter Launcher lvl 5";
		hunterLauncher [4].weaponType = "Hunter Launcher";
		hunterLauncher [4].weaponLevel = 5;

	}

	public void SetQuantumPrism()
	{
		quantumPrism = new Weapon[5];

		quantumPrism [0] = new Weapon ();
		quantumPrism [0].auto = false;
		quantumPrism [0].bulletDamage = 1;
		quantumPrism [0].bulletSpeed = 100;
		quantumPrism [0].powerUse = 20;
		quantumPrism [0].weaponName = "Quantum Prism";
		quantumPrism [0].weaponDescription = "The colourful beams cast from this strange object bring love and happiness to even the darkest heart in the universe, before disintegrating it and ripping a hole in reality.";
		quantumPrism [0].weaponType = "Quantum Prism";
		quantumPrism [0].weaponLevel = 1;
		quantumPrism [0].experienceCap = 1000;
		quantumPrism [0].weaponValue = 75000;

		quantumPrism [1] = new Weapon ();
		quantumPrism [1].auto = false;
		quantumPrism [1].bulletDamage = 2;
		quantumPrism [1].bulletSpeed = 100;
		quantumPrism [1].powerUse = 30;
		quantumPrism [1].weaponName = "Quantum Prism";
		quantumPrism [1].weaponDescription = "Quantum Prism lvl 2";
		quantumPrism [1].weaponType = "Quantum Prism";
		quantumPrism [1].weaponLevel = 2;
		quantumPrism [1].experienceCap = 2000;

		quantumPrism [2] = new Weapon ();
		quantumPrism [2].auto = false;
		quantumPrism [2].bulletDamage = 3;
		quantumPrism [2].bulletSpeed = 100;
		quantumPrism [2].powerUse = 30;
		quantumPrism [2].weaponName = "Quantum Prism";
		quantumPrism [2].weaponDescription = "Quantum Prism lvl 3";
		quantumPrism [2].weaponType = "Quantum Prism";
		quantumPrism [2].weaponLevel = 3;
		quantumPrism [2].experienceCap = 3000;

		quantumPrism [3] = new Weapon ();
		quantumPrism [3].auto = false;
		quantumPrism [3].bulletDamage = 4;
		quantumPrism [3].bulletSpeed = 100;
		quantumPrism [3].powerUse = 40;
		quantumPrism [3].weaponName = "Quantum Prism";
		quantumPrism [3].weaponDescription = "Quantum Prism lvl 4";
		quantumPrism [3].weaponType = "Quantum Prism";
		quantumPrism [3].weaponLevel = 4;
		quantumPrism [3].experienceCap = 4000;

		quantumPrism [4] = new Weapon ();
		quantumPrism [4].auto = false;
		quantumPrism [4].bulletDamage = 5;
		quantumPrism [4].bulletSpeed = 100;
		quantumPrism [4].powerUse = 40;
		quantumPrism [4].weaponName = "Hyper Prism";
		quantumPrism [4].weaponDescription = "Quantum Prism lvl 5";
		quantumPrism [4].weaponType = "Quantum Prism";
		quantumPrism [4].weaponLevel = 5;
		quantumPrism [4].experienceCap = 5000;
	}


	public void SaveUpgrades()
	{
		SetDefaults ();

		string jsonString = JsonUtility.ToJson(this);
		try
		{
			File.WriteAllText(Application.dataPath + "/Data/Weapon Data.json", jsonString.ToString());
			Debug.Log(System.DateTime.Now.ToString() + "   Trying To Save Weapon Upgrades File.");
		}
		catch (System.Exception)
		{
			Debug.LogWarning(System.DateTime.Now.ToString() + "   COULD NOT SAVE WEAPON UPGRADES FILE, TRYING AGAIN.");
			Directory.CreateDirectory(Application.dataPath + "/Data");
			SaveUpgrades();
		}

		Debug.Log(System.DateTime.Now.ToString() + "   Saved Weapon Upgrades File.");
	}

	public WeaponUpgrades LoadUpgrades()
	{
		WeaponUpgrades _upgrades = new WeaponUpgrades();
		try
		{
			Debug.Log(System.DateTime.Now.ToString() + "   Trying To Load Weapon Upgrades File.");
			string jsonString = File.ReadAllText(Application.dataPath + "/Data/Weapon Data.json");
			_upgrades = JsonUtility.FromJson<WeaponUpgrades>(jsonString);
		}
		catch (System.Exception)
		{
			Debug.LogWarning(System.DateTime.Now.ToString() + "   COULD NOT LOAD WEAPON UPGRADES FILE, MAKING A NEW ONE.");
			SaveUpgrades();
		}

		Debug.Log(System.DateTime.Now.ToString() + "   Loaded Weapon Upgrades File.");
		return _upgrades;
	}
}