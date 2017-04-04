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
		} if (currentAngle >= -225 + 22.5) {
			hoverItem = 5;
			selectorObject.transform.rotation = Quaternion.AngleAxis (180, Vector3.back);
		} if (currentAngle >= -180 + 22.5) {
			hoverItem = 4;
			selectorObject.transform.rotation = Quaternion.AngleAxis (135, Vector3.back);
		} if (currentAngle >= -135 + 22.5) {
			hoverItem = 3;
			selectorObject.transform.rotation = Quaternion.AngleAxis (90, Vector3.back);
		} if (currentAngle >= -90 + 22.5) {
			hoverItem = 2;
			selectorObject.transform.rotation = Quaternion.AngleAxis (45, Vector3.back);
		} if (currentAngle >= -45 + 22.5) {
			hoverItem = 1;
			selectorObject.transform.rotation = Quaternion.AngleAxis (0, Vector3.back);
		} if (currentAngle >= 45 - 22.5) {
			hoverItem = 8;
			selectorObject.transform.rotation = Quaternion.AngleAxis (315, Vector3.back);
		} if (currentAngle >= 90 - 22.5) {
			hoverItem = 7;
			selectorObject.transform.rotation = Quaternion.AngleAxis (270, Vector3.back);
		}


	}
}