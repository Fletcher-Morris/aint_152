using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem_Script : MonoBehaviour
{
	public string dropType = "Money";
	public int dropValue = 10;
	public float movementRange = 5f;
	public float pickupRange = 0.5f;
	public float moveSpeed = 0.3f;

	GameObject targetObject;
	public bool targetPlayer = true;

	void Start()
	{
		if (targetPlayer) {
			targetObject = GameObject.Find ("Player Ship");
		}
	}

	void Update()
	{
		if (targetObject) {
			if (moveSpeed > 0) {
				if (Vector2.Distance (transform.position, targetObject.transform.position) <= movementRange) {
					transform.position = Vector2.MoveTowards (transform.position, targetObject.transform.position, moveSpeed);
				}
			}
			
			if (Vector2.Distance (transform.position, targetObject.transform.position) <= pickupRange) {
				if (dropType == "Money") {
					GameObject.Find ("WM").GetComponent<WorldLoader_Script> ().theWorld.money += dropValue;
				}
				GameObject.Destroy (gameObject);
			}
		}
	}
}