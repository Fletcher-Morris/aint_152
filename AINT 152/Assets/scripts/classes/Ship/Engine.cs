using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Engine{

	public string engineName;
	public int maxThrust;
	public int currentThrust;
	public int maxHeat;
	public int currentHeat;
	public int powerConsumption;

	public Engine(){
		engineName = "New Engine";
		maxThrust = 1000;
		currentThrust = 0;
		maxHeat = 1000;
		currentHeat = 0;
		powerConsumption = 100;
	}
}