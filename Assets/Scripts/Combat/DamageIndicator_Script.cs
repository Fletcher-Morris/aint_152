using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator_Script : MonoBehaviour
{
	public float damageAmount;
    public Color textColour;

	void Start(){
		transform.GetChild(0).gameObject.GetComponent<Text> ().text = damageAmount.ToString ();
        transform.GetChild(0).gameObject.GetComponent<Text>().color = textColour;
    }

	void Update(){
		transform.GetChild(0).gameObject.GetComponent<Text> ().text = damageAmount.ToString ();
	}
}