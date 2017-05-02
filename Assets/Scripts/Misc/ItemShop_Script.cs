using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop_Script : MonoBehaviour
{
	public GameObject weaponShopItemPrefab;
	public int minValue;
	public int maxValue;
	public List<Weapon> buyableWeaponsList;
	public int selectedItem = 1;

	public Sprite ionBlasterSprite;
	public Sprite fusionMineSprite;
	public Sprite hunterLauncherSprite;
	public Sprite quantumPrismSprite;
    public Sprite refractionMatrixSprite;

	public Color normalColour;
	public Color normalTextColour;
	public Color selectedColour;
	public Color selectedTextColour;

	void Start()
	{
		CloseItemShop ();
	}

	public void OpenShop()
	{
		GetWeaponsToBuy ();
		InstantiateUIObjects ();
		if (selectedItem <= 0) {
			selectedItem = 1;
		}
	}

	public void GetWeaponsToBuy()
	{
		if (!GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.hasIonBlaster) {
			buyableWeaponsList.Add (GameObject.Find("GM").GetComponent<WeaponData_Script>().ionBlasterUpgrades[0]);
		}

		if (!GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.hasFusionMine) {
			buyableWeaponsList.Add (GameObject.Find("GM").GetComponent<WeaponData_Script>().fusionMineUpgrades[0]);
		}

		if (!GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.hasHunterLauncher) {
			buyableWeaponsList.Add (GameObject.Find("GM").GetComponent<WeaponData_Script>().hunterLauncherUpgrades[0]);
		}

		if (!GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.hasQuantumPrism) {
			buyableWeaponsList.Add (GameObject.Find("GM").GetComponent<WeaponData_Script>().quantumPrismUpgrades[0]);
		}

        if (!GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.hasRefractionMatrix)
        {
            buyableWeaponsList.Add(GameObject.Find("GM").GetComponent<WeaponData_Script>().refractionMatrixUpgrades[0]);
        }
    }

	public void InstantiateUIObjects()
	{
		foreach (Weapon weaponItem in buyableWeaponsList) {
			
			GameObject newObject = weaponShopItemPrefab;

			if (weaponItem.weaponType == "Ion Blaster") {
				newObject.transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = ionBlasterSprite;
			}
            else if (weaponItem.weaponType == "Fusion Mine") {
				newObject.transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = fusionMineSprite;
			}
            else if (weaponItem.weaponType == "Hunter Launcher") {
				newObject.transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = hunterLauncherSprite;
			}
            else if (weaponItem.weaponType == "Quantum Prism") {
				newObject.transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = quantumPrismSprite;
			}
            else if (weaponItem.weaponType == "Refraction Matrix")
            {
                newObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = refractionMatrixSprite;
            }

            newObject.transform.GetChild (1).gameObject.GetComponent<Text> ().text = weaponItem.weaponName;

			GameObject spawnedObject = GameObject.Instantiate (newObject, Vector3.zero, Quaternion.identity, gameObject.transform.GetChild(0).transform);

			spawnedObject.transform.localScale = new Vector3 (1, 1, 1);
			spawnedObject.transform.localPosition = Vector3.zero;
			spawnedObject.transform.localRotation = Quaternion.Euler (Vector3.zero);
		}
	}

	public void Update()
	{

		if (selectedItem <= 0 || selectedItem > buyableWeaponsList.Count) {
			selectedItem = 1;
		}

		if (transform.GetChild(0).transform.childCount >= 1 && buyableWeaponsList.Count >= 1) {
			gameObject.transform.GetChild (1).GetChild (0).GetChild (0).gameObject.GetComponent<Text> ().text = "Cost: $" + buyableWeaponsList [selectedItem - 1].weaponValue.ToString ();
			gameObject.transform.GetChild (1).GetChild (1).GetChild (0).gameObject.GetComponent<Text> ().text = buyableWeaponsList [selectedItem - 1].weaponDescription;
			
			for (int i = 1; i <= buyableWeaponsList.Count; i++) {
				gameObject.transform.GetChild (0).GetChild (i - 1).gameObject.GetComponent<Image> ().color = normalColour;
				gameObject.transform.GetChild (0).GetChild (i - 1).GetChild (0).gameObject.GetComponent<Image> ().color = normalTextColour;
				gameObject.transform.GetChild (0).GetChild (i - 1).GetChild (1).gameObject.GetComponent<Text> ().color = normalTextColour;
			}
			
			gameObject.transform.GetChild (0).GetChild (selectedItem - 1).gameObject.GetComponent<Image> ().color = selectedColour;
			gameObject.transform.GetChild (0).GetChild (selectedItem - 1).GetChild (0).gameObject.GetComponent<Image> ().color = selectedTextColour;
			gameObject.transform.GetChild (0).GetChild (selectedItem - 1).GetChild (1).gameObject.GetComponent<Text> ().color = selectedTextColour;

			if (buyableWeaponsList [selectedItem - 1].weaponValue > GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.money) {
				gameObject.transform.GetChild (1).GetChild (0).GetChild (0).gameObject.GetComponent<Text> ().color = Color.red;
				gameObject.transform.GetChild (1).GetChild (2).GetComponent<Button> ().interactable = false;
				gameObject.transform.GetChild (1).GetChild (2).GetChild (0).gameObject.SetActive (false);
			} else {
				gameObject.transform.GetChild (1).GetChild (0).GetChild (0).gameObject.GetComponent<Text> ().color = Color.white;
				gameObject.transform.GetChild (1).GetChild (2).GetComponent<Button> ().interactable = true;
				gameObject.transform.GetChild (1).GetChild (2).GetChild (0).gameObject.SetActive (true);
			}
		}

		if (Input.GetButtonDown ("Vertical")) {
			if (Input.GetAxis ("Vertical") < 0) {
				if (selectedItem < buyableWeaponsList.Count) {
					selectedItem++;
				} else {
					selectedItem = 1;
				}
			} else {
				if (selectedItem >= 2) {
					selectedItem--;
				} else {
					selectedItem = buyableWeaponsList.Count;
				}
			}
		}
	}

	public void BuySelectedItem()
	{
		if (buyableWeaponsList [selectedItem - 1].weaponType == "Ion Blaster") {
			GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.hasIonBlaster = true;
		}
        else if (buyableWeaponsList [selectedItem - 1].weaponType == "Fusion Mine") {
			GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.hasFusionMine = true;
		}
        else if (buyableWeaponsList [selectedItem - 1].weaponType == "Hunter Launcher") {
			GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.hasHunterLauncher = true;
		}
        else if (buyableWeaponsList [selectedItem - 1].weaponType == "Quantum Prism") {
			GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.hasQuantumPrism = true;
		}
        else if (buyableWeaponsList[selectedItem - 1].weaponType == "Refraction Matrix")
        {
            GameObject.Find("WM").GetComponent<WorldLoader_Script>().theWorld.hasRefractionMatrix = true;
        }

        GameObject.Find ("WM").GetComponent<WorldLoader_Script>().theWorld.playerShip.shipTurret.AddWeapon(buyableWeaponsList[selectedItem - 1]);
		GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.money -= buyableWeaponsList [selectedItem - 1].weaponValue;

		buyableWeaponsList.RemoveAt (selectedItem - 1);
		GameObject.Destroy (transform.GetChild(0).GetChild(selectedItem - 1).gameObject);
	}

	public void CloseItemShop()
	{
		GetComponent<Canvas> ().enabled = false;

		buyableWeaponsList.Clear ();

		for (int i = 1; i <= transform.GetChild(0).transform.childCount; i++)
		{
			GameObject.Destroy (transform.GetChild(0).GetChild(i - 1).gameObject);
		}

		GameObject.Find("Player").GetComponent<Rigidbody2D> ().isKinematic = false;
		GameObject.Find ("GM").GetComponent<GameState_Script> ().SetPlayerState ("Normal");
	}
}