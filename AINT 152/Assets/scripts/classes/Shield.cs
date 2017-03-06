using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shield{

	public string shieldName;
	public float shieldHealth;
	public float absorbPercent;
	public float chargeRate;
	public float chargeDelay;
	public float powerUse;
	public int idlePowerUse;

	public Shield(){
		shieldName = "New Shield";
		shieldHealth = 1000;
		absorbPercent = 50;
		chargeRate = 5;
		chargeDelay = 5;
		powerUse = 1000;
		idlePowerUse = 10;
	}
}