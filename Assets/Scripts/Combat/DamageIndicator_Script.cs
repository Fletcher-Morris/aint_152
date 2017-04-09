using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator_Script : MonoBehaviour
{
	public float damageAmount;

	void Start(){
		transform.GetChild(0).gameObject.GetComponent<Text> ().text = damageAmount.ToString ();
	}

	void Update(){
		transform.GetChild(0).gameObject.GetComponent<Text> ().text = damageAmount.ToString ();
	}
}