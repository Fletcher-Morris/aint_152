using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reactor{

	public string reactorName;
    public float maxPower;
    public float currentPower;
    public float rechargeDelay;
    public float rechargeRate;

	public Reactor(){
		reactorName = "New Reactor";
        maxPower = 100f;
        currentPower = maxPower;
        rechargeDelay = 2f;
        rechargeRate = 10f;
	}

	public Reactor(string _name, int _maxOutput){
		reactorName = _name;
        maxPower = _maxOutput;
        currentPower = maxPower;
        rechargeDelay = 2f;
        rechargeRate = 100f;
    }
}