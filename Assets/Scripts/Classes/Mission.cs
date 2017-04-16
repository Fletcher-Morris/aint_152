using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission{

	public string missionName;
	public string missionDescription;
	public int missionReward;
	public bool started;
	public bool completed;
	public int missionLocation;

	public Mission(){
		missionName = "New Mission";
		missionDescription = "A shiney new mission.";
		missionReward = 10000;
		started = true;
		completed = false;
		missionLocation = 0;
	}
}