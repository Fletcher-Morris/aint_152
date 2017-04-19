using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WeaponData_Script : MonoBehaviour
{

	public List<Weapon> ionBlasterUpgrades;
	public List<Weapon> fusionMineUpgrades;
	public List<Weapon> hunterLauncherUpgrades;
	public List<Weapon> quantumPrismUpgrades;

	public List<Weapon> defaultIonBlasterUpgrades;
	public List<Weapon> defautFusionMineUpgrades;
	public List<Weapon> defaultHunterLauncherUpgrades;
	public List<Weapon> defaultQuantumPrismUpgrades;

	void Start()
	{
		if (!Directory.Exists (Application.dataPath + "/Data/Weapons/")) {
			Debug.LogWarning ("Weapons Directory Does Not Exist, Creating A New One.");
			Directory.CreateDirectory (Application.dataPath + "/Data/Weapons/");
			SaveDefaults ();
		}

		LoadWeapons ();
	}

	void SaveDefaults(){

		foreach (Weapon _weapon in defaultIonBlasterUpgrades) {
			if (_weapon.weaponType != "") {
				string jsonString = JsonUtility.ToJson (_weapon);
				
				if (!Directory.Exists (Application.dataPath + "/Data/Weapons/" + _weapon.weaponType + "/"))
					Directory.CreateDirectory (Application.dataPath + "/Data/Weapons/" + _weapon.weaponType + "/");
				
				File.WriteAllText (Application.dataPath + "/Data/Weapons/" + _weapon.weaponType + "/" + _weapon.weaponLevel + ".json", jsonString.ToString ());
				Debug.Log ("Saving mission file (" + _weapon.weaponName + ").");
			}
		}

		foreach (Weapon _weapon in defautFusionMineUpgrades) {
			if (_weapon.weaponType != "") {
				string jsonString = JsonUtility.ToJson (_weapon);

				if (!Directory.Exists (Application.dataPath + "/Data/Weapons/" + _weapon.weaponType + "/"))
					Directory.CreateDirectory (Application.dataPath + "/Data/Weapons/" + _weapon.weaponType + "/");

				File.WriteAllText (Application.dataPath + "/Data/Weapons/" + _weapon.weaponType + "/" + _weapon.weaponLevel + ".json", jsonString.ToString ());
				Debug.Log ("Saving mission file (" + _weapon.weaponName + ").");
			}
		}

		foreach (Weapon _weapon in defaultHunterLauncherUpgrades) {
			if (_weapon.weaponType != "") {
				string jsonString = JsonUtility.ToJson (_weapon);

				if (!Directory.Exists (Application.dataPath + "/Data/Weapons/" + _weapon.weaponType + "/"))
					Directory.CreateDirectory (Application.dataPath + "/Data/Weapons/" + _weapon.weaponType + "/");

				File.WriteAllText (Application.dataPath + "/Data/Weapons/" + _weapon.weaponType + "/" + _weapon.weaponLevel + ".json", jsonString.ToString ());
				Debug.Log ("Saving mission file (" + _weapon.weaponName + ").");
			}
		}

		foreach (Weapon _weapon in defaultQuantumPrismUpgrades) {
			if (_weapon.weaponType != "") {
				string jsonString = JsonUtility.ToJson (_weapon);

				if (!Directory.Exists (Application.dataPath + "/Data/Weapons/" + _weapon.weaponType + "/"))
					Directory.CreateDirectory (Application.dataPath + "/Data/Weapons/" + _weapon.weaponType + "/");

				File.WriteAllText (Application.dataPath + "/Data/Weapons/" + _weapon.weaponType + "/" + _weapon.weaponLevel + ".json", jsonString.ToString ());
				Debug.Log ("Saving mission file (" + _weapon.weaponName + ").");
			}
		}
	}

	public void LoadWeapons(){

		foreach(string _directory in Directory.GetDirectories(Application.dataPath + "/Data/Weapons/")){

			foreach(string _file in Directory.GetFiles(_directory)){

				if (_file.EndsWith(".json")) {

					string jsonString = File.ReadAllText (_file);
					jsonString = (GetComponent<WordReplacer_Script> ().ReplaceWords (jsonString));
					Weapon _weapon = JsonUtility.FromJson<Weapon> (jsonString);

					if (_weapon.weaponType == "Ion Blaster")
						ionBlasterUpgrades.Add (_weapon);
					else if (_weapon.weaponType == "Fusion Mine")
						fusionMineUpgrades.Add (_weapon);
					else if (_weapon.weaponType == "Hunter Launcher")
						hunterLauncherUpgrades.Add (_weapon);
					else if (_weapon.weaponType == "Quantum Prism")
						quantumPrismUpgrades.Add (_weapon);

					Debug.Log ("Loaded Weapon File (" + _weapon.weaponName + ").");
				}
			}
		}
	}
}