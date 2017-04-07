using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponWheel_Script : MonoBehaviour
{
	public float distanceFromCenter;
	public float wheelRadius;
	public float currentAngle;
	public int hoverItem;

	public Sprite hollowHex;
	public Sprite fillHex;
	public Color selectedColour;
	public Material selectedMat;
	public Color normalColour;

	public GameObject selectorObject;
	public GameObject[] itemObject;

	public void Update()
	{

		wheelRadius = gameObject.GetComponent<RectTransform> ().rect.width / 2;

		distanceFromCenter = Vector2.Distance (Input.mousePosition, gameObject.transform.position);

		Vector3 dir = transform.position - Input.mousePosition;
		currentAngle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg - 90;

		if (currentAngle >= -270 + 22.5) {
			hoverItem = 6;
			selectorObject.transform.rotation = Quaternion.AngleAxis (225, Vector3.back);
		}

		if (currentAngle >= -225 + 22.5) {
			hoverItem = 5;
			selectorObject.transform.rotation = Quaternion.AngleAxis (180, Vector3.back);
		}

		if (currentAngle >= -180 + 22.5) {
			hoverItem = 4;
			selectorObject.transform.rotation = Quaternion.AngleAxis (135, Vector3.back);
		}

		if (currentAngle >= -135 + 22.5) {
			hoverItem = 3;
			selectorObject.transform.rotation = Quaternion.AngleAxis (90, Vector3.back);
		}

		if (currentAngle >= -90 + 22.5) {
			hoverItem = 2;
			selectorObject.transform.rotation = Quaternion.AngleAxis (45, Vector3.back);
		}

		if (currentAngle >= -45 + 22.5) {
			hoverItem = 1;
			selectorObject.transform.rotation = Quaternion.AngleAxis (0, Vector3.back);
		}

		if (currentAngle >= 45 - 22.5) {
			hoverItem = 8;
			selectorObject.transform.rotation = Quaternion.AngleAxis (315, Vector3.back);
		}

		if (currentAngle >= 90 - 22.5) {
			hoverItem = 7;
			selectorObject.transform.rotation = Quaternion.AngleAxis (270, Vector3.back);
		}


		foreach (GameObject _object in itemObject) {
			_object.GetComponent<Image> ().color = normalColour;
		}


		for (int i = 1; i <= itemObject.Length; i++)
		{
			if (hoverItem == i) {
				itemObject[i - 1].GetComponent<Image> ().sprite = fillHex;
				itemObject[i - 1].transform.GetChild (0).gameObject.GetComponent<Image> ().material = selectedMat;
				itemObject[i - 1].transform.GetChild (0).gameObject.GetComponent<Image> ().color = selectedColour;
			} else {
				itemObject[i - 1].GetComponent<Image> ().sprite = hollowHex;
				itemObject[i - 1].transform.GetChild (0).gameObject.GetComponent<Image> ().material = null;
				itemObject[i - 1].transform.GetChild (0).gameObject.GetComponent<Image> ().color = normalColour;
			}
		}

	}

	public int GetCurrentSelectedWeaponName()
	{
		return hoverItem;
	}
}