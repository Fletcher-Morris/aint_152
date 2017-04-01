using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop_Script : MonoBehaviour
{

	public int playerLevel = 0;
	public int minValue;
	public int maxValue;
	public Weapon weapon1;
	public Weapon weapon2;

	void Start()
	{
		playerLevel = GetPlayerLevel ();
		weapon1 = new Weapon ();
		weapon1.RandomizeWeapon (minValue, maxValue);
		weapon2 = new Weapon();
		weapon2.RandomizeWeapon(minValue, maxValue);
	}

	public int GetPlayerLevel()
	{
		return Mathf.RoundToInt(GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.highScore / 100);
	}
}