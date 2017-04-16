using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class WordReplacer_Script : MonoBehaviour {

	public List<Replacement> replacements;

	public string ReplaceWords(string words){
		
		StringBuilder sb = new StringBuilder (words);

		foreach (Replacement _replacement in replacements) {

			if (_replacement.playerName)
				_replacement.output = GetComponent<GamePrefs_Script> ().gamePrefs.playerName;
			else if (_replacement.system1 && GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.starSystems.Count >= 1)
				_replacement.output = GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.starSystems [0].systemName;
			else if (_replacement.system2 && GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.starSystems.Count >= 2)
				_replacement.output = GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.starSystems [1].systemName;

			sb.Replace (_replacement.input, _replacement.output);
			Debug.Log ("Replaced " + _replacement.input + " with " + _replacement.output + ".");
		}

		return sb.ToString();
	}
}

[System.Serializable]
public class Replacement{


	public string input;
	public string output;

	public bool playerName;
	public bool system1;
	public bool system2;
}