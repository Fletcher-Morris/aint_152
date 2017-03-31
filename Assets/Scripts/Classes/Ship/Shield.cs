using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shield{

	public string shieldName;
	public float maxShieldHealth;
    public float shieldHealth;
	public int absorbPercent;
	public float chargeRate;
	public float chargeDelay;

	public Shield(){
		shieldName = "New Shield";
        maxShieldHealth = 100;
        shieldHealth = maxShieldHealth;
		absorbPercent = 50;
		chargeRate = 5;
		chargeDelay = 2;
	}
}