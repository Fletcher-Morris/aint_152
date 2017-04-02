using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ItemUIController_Script : MonoBehaviour
{
	public Item thisItem;

	public GameObject nameLabel;
	public GameObject costLabel;
	public GameObject typeLabel;
	public GameObject detailsLabel;
	public GameObject iconGraphic;

	public void Update()
	{
		costLabel.GetComponent<Text> ().text = "Cost: " + thisItem.itemValue;
		nameLabel.GetComponent<Text> ().text = "Name: " + thisItem.itemName;
		typeLabel.GetComponent<Text> ().text = "Type: " + thisItem.itemType;
		detailsLabel.GetComponent<Text> ().text = thisItem.itemDetails;


		if (GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.money < thisItem.itemValue) {
			costLabel.GetComponent<Text> ().color = Color.red;
			gameObject.transform.GetChild (2).gameObject.SetActive (false);
		} else {
			costLabel.GetComponent<Text> ().color = Color.black;
			gameObject.transform.GetChild (2).gameObject.SetActive (true);
		}
	}

	public void BuyItem()
	{
		if (GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.money >= thisItem.itemValue) {
			GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.money -= thisItem.itemValue;


			GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.playerShip.shipTurret.turretWeapon = new Weapon ();
			GameObject.Destroy (gameObject);
		}

	}
}