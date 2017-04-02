using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop_Script : MonoBehaviour
{

	public int playerLevel = 0;
	public GameObject itemUiPrefab;
	public int minValue;
	public int maxValue;
	public Weapon weapon1;
	public Weapon weapon2;

	void Start()
	{
		CloseItemShop ();

		playerLevel = GetPlayerLevel ();
		weapon1 = new Weapon ();
		weapon1.RandomizeWeapon (minValue, maxValue);
		weapon2 = new Weapon();
		weapon2.RandomizeWeapon(minValue, maxValue);

		InstantiateUIObjects ();
	}

	public int GetPlayerLevel()
	{
		return Mathf.RoundToInt(GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.highScore / 100);
	}

	public void InstantiateUIObjects()
	{
		GameObject weapon1Object = itemUiPrefab;
		weapon1Object.GetComponent<ItemUIController_Script> ().thisItem = weapon1.weaponItem;
		GameObject.Instantiate (weapon1Object, this.transform.GetChild (1).transform);

		GameObject weapon2Object = itemUiPrefab;
		weapon2Object.GetComponent<ItemUIController_Script> ().thisItem = weapon2.weaponItem;
		GameObject.Instantiate (weapon2Object, this.transform.GetChild (1).transform);
	}

	public void CloseItemShop()
	{
		GetComponent<Canvas> ().enabled = false;
		GameObject.Find("Player").GetComponent<Rigidbody2D> ().isKinematic = false;
		GameObject.Find ("GM").GetComponent<GameState_Script> ().gameState = "Normal";
	}
}