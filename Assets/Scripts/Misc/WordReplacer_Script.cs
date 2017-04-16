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

}