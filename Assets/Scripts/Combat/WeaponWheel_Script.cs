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
	public GameObject item1Object;
	public GameObject item2Object;
	public GameObject item3Object;
	public GameObject item4Object;
	public GameObject item5Object;
	public GameObject item6Object;
	public GameObject item7Object;
	public GameObject item8Object;

	public string item1;
	public string item2;
	public string item3;
	public string item4;
	public string item5;
	public string item6;
	public string item7;
	public string item8;

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



		if (hoverItem == 1) {
			item1Object.GetComponent<Image> ().sprite = fillHex;
			item1Object.transform.GetChild (0).gameObject.GetComponent<Image> ().material = selectedMat;
			item1Object.transform.GetChild (0).gameObject.GetComponent<Image> ().color = selectedColour;
		} else {
			item1Object.GetComponent<Image> ().sprite = hollowHex;
			item1Object.transform.GetChild (0).gameObject.GetComponent<Image> ().material = null;
			item1Object.transform.GetChild (0).gameObject.GetComponent<Image> ().color = normalColour;
		}

		if (hoverItem == 2) {
			item2Object.GetComponent<Image> ().sprite = fillHex;
			item2Object.transform.GetChild (0).gameObject.GetComponent<Image> ().material = selectedMat;
			item2Object.transform.GetChild (0).gameObject.GetComponent<Image> ().color = selectedColour;
		} else {
			item2Object.GetComponent<Image> ().sprite = hollowHex;
			item2Object.transform.GetChild (0).gameObject.GetComponent<Image> ().material = null;
			item2Object.transform.GetChild (0).gameObject.GetComponent<Image> ().color = normalColour;
		}

		if (hoverItem == 3) {
			item3Object.GetComponent<Image> ().sprite = fillHex;
			item3Object.transform.GetChild (0).gameObject.GetComponent<Image> ().material = selectedMat;
			item3Object.transform.GetChild (0).gameObject.GetComponent<Image> ().color = selectedColour;
		} else {
			item3Object.GetComponent<Image> ().sprite = hollowHex;
			item3Object.transform.GetChild (0).gameObject.GetComponent<Image> ().material = null;
			item3Object.transform.GetChild (0).gameObject.GetComponent<Image> ().color = normalColour;
		}

		if (hoverItem == 4) {
			item4Object.GetComponent<Image> ().sprite = fillHex;
			item4Object.transform.GetChild (0).gameObject.GetComponent<Image> ().material = selectedMat;
			item4Object.transform.GetChild (0).gameObject.GetComponent<Image> ().color = selectedColour;
		} else {
			item4Object.GetComponent<Image> ().sprite = hollowHex;
			item4Object.transform.GetChild (0).gameObject.GetComponent<Image> ().material = null;
			item4Object.transform.GetChild (0).gameObject.GetComponent<Image> ().color = normalColour;
		}

		if (hoverItem == 5) {
			item5Object.GetComponent<Image> ().sprite = fillHex;
			item5Object.transform.GetChild (0).gameObject.GetComponent<Image> ().material = selectedMat;
			item5Object.transform.GetChild (0).gameObject.GetComponent<Image> ().color = selectedColour;
		} else {
			item5Object.GetComponent<Image> ().sprite = hollowHex;
			item5Object.transform.GetChild (0).gameObject.GetComponent<Image> ().material = null;
			item5Object.transform.GetChild (0).gameObject.GetComponent<Image> ().color = normalColour;
		}

		if (hoverItem == 6) {
			item6Object.GetComponent<Image> ().sprite = fillHex;
			item6Object.transform.GetChild (0).gameObject.GetComponent<Image> ().material = selectedMat;
			item6Object.transform.GetChild (0).gameObject.GetComponent<Image> ().color = selectedColour;
		} else {
			item6Object.GetComponent<Image> ().sprite = hollowHex;
			item6Object.transform.GetChild (0).gameObject.GetComponent<Image> ().material = null;
			item6Object.transform.GetChild (0).gameObject.GetComponent<Image> ().color = normalColour;
		}

		if (hoverItem == 7) {
			item7Object.GetComponent<Image> ().sprite = fillHex;
			item7Object.transform.GetChild (0).gameObject.GetComponent<Image> ().material = selectedMat;
			item7Object.transform.GetChild (0).gameObject.GetComponent<Image> ().color = selectedColour;
		} else {
			item7Object.GetComponent<Image> ().sprite = hollowHex;
			item7Object.transform.GetChild (0).gameObject.GetComponent<Image> ().material = null;
			item7Object.transform.GetChild (0).gameObject.GetComponent<Image> ().color = normalColour;
		}

		if (hoverItem == 8) {
			item8Object.GetComponent<Image> ().sprite = fillHex;
			item8Object.transform.GetChild (0).gameObject.GetComponent<Image> ().material = selectedMat;
			item8Object.transform.GetChild (0).gameObject.GetComponent<Image> ().color = selectedColour;
		} else {
			item8Object.GetComponent<Image> ().sprite = hollowHex;
			item8Object.transform.GetChild (0).gameObject.GetComponent<Image> ().material = null;
			item8Object.transform.GetChild (0).gameObject.GetComponent<Image> ().color = normalColour;
		}

	}
}