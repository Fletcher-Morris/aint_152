using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reactor{

	public string reactorName;
	public int maxOutput;
	public int currentOutput;
	public int maxHeat;
	public int currentHeat;

	public Reactor(){
		reactorName = "New Reactor";
		maxOutput = 1000000000;
		currentOutput = 0;
		maxHeat = 1000;
		currentHeat = 0;
	}

	public Reactor(string _name, int _maxOutput, int _maxHeat){
		reactorName = _name;
		maxOutput = _maxOutput;
		maxHeat = _maxHeat;
	}
}