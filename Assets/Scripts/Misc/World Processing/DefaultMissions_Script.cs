using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DefaultMissions_Script : MonoBehaviour {
    
	public List<Mission> defaultMissions;
	public List<Mission> loadedMissions; 

	void Start(){
		if (!Directory.Exists (Application.dataPath + "/Data/Missions/")) {
			Debug.LogWarning ("Missions Directory Does Not Exist, Creating A New One.");
			Directory.CreateDirectory (Application.dataPath + "/Data/Missions/");
			SaveDefaults ();
		}

		loadedMissions = LoadMissions ();
	}

	void SaveDefaults(){

		foreach (Mission _mission in defaultMissions) {
			string jsonString = JsonUtility.ToJson(_mission);
			File.WriteAllText(Application.dataPath + "/Data/Missions/" + _mission.missionName + ".json", jsonString.ToString());
			Debug.Log("Saving mission file (" + _mission.missionName + ").");
		}
	}

	public List<Mission> LoadMissions(){
		
		List<Mission> _missions = new List<Mission> ();

		foreach(string _directory in Directory.GetFiles(Application.dataPath + "/Data/Missions/")){

			if (_directory.EndsWith(".json")) {

				string jsonString = File.ReadAllText (_directory);
				//jsonString = (GetComponent<WordReplacer_Script> ().ReplaceWords (jsonString));
				Mission _mission = JsonUtility.FromJson<Mission> (jsonString);
				_missions.Add (_mission);
				Debug.Log ("Loaded mission file (" + _mission.missionName + ").");
			}
		}

		return _missions;
	}

    public Mission Find(string _name)
    {
        foreach (Mission _mission in loadedMissions)
        {
            if(_mission.missionName == _name)
            {
                return _mission;
            }
        }

        return null;
    }
}
