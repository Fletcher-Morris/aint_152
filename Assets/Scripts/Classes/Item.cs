using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{

	public string itemName;
	public string itemType;
	public int itemValue;
	public string itemDetails;

	public Item(string _itemName, string _itemType, int _itemVelue, string _itemDetails){
		itemName = _itemName;
		itemType = _itemType;
		itemValue = _itemVelue;
		itemDetails = _itemDetails;
	}
}