using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumPrismBeam_Script : MonoBehaviour
{
	public float beamLength;
	public float maxIntensity = 1;
	public float speed = 10;
	public float sinSpeed = 5;

	public GameObject controllerObject;
	public float timeSpentShooting;
	public float currentIntensity;
	public float startAngle = 45;
	private float angleProgress;
	public float minAngle = 10;

	public GameObject beam1;
	public GameObject beam2;
	public GameObject beam3;
	public GameObject beam4;
	public GameObject beam5;
	public GameObject beam6;
	public GameObject beam7;

	void Start(){
		
		Vector3 beamScale = transform.localScale;
		angleProgress = startAngle;
		currentIntensity = 0;

		beam1.transform.GetChild (0).transform.localScale = new Vector3 (beamLength, 0.3f, beamScale.z);
		beam2.transform.GetChild (0).transform.localScale = new Vector3 (beamLength, 0.3f, beamScale.z);
		beam3.transform.GetChild (0).transform.localScale = new Vector3 (beamLength, 0.3f, beamScale.z);
		beam4.transform.GetChild (0).transform.localScale = new Vector3 (beamLength, 0.3f, beamScale.z);
		beam5.transform.GetChild (0).transform.localScale = new Vector3 (beamLength, 0.3f, beamScale.z);
		beam6.transform.GetChild (0).transform.localScale = new Vector3 (beamLength, 0.3f, beamScale.z);
		beam7.transform.GetChild (0).transform.localScale = new Vector3 (beamLength, 0.3f, beamScale.z);

		beam1.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,0,0);
		beam2.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,0,0);
		beam3.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,0,0);
		beam4.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,0,0);
		beam5.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,0,0);
		beam6.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,0,0);
		beam7.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,0,0);
	}

	void Update()
	{
		transform.eulerAngles = new Vector3 (transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, transform.parent.rotation.eulerAngles.z - 90);

		timeSpentShooting += Time.deltaTime;

		if (angleProgress > minAngle) {
			angleProgress -= Time.deltaTime * speed;
		} else {
			angleProgress = minAngle;
		}

		if (currentIntensity < maxIntensity) {
			currentIntensity += 5 * timeSpentShooting;
		} else {
			currentIntensity = maxIntensity;
		}

		beam1.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + (0f * (Mathf.PI / 2)))));
		beam2.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + (.14f * 2f * (Mathf.PI / 2)))));
		beam3.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + (.28f * 2f * (Mathf.PI / 2)))));
		beam4.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + (.43f * 2f * (Mathf.PI / 2)))));
		beam5.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + (.57f * 2f * (Mathf.PI / 2)))));
		beam6.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + (.71f * 2f * (Mathf.PI / 2)))));
		beam7.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + (.86f * 2f * (Mathf.PI / 2)))));

		beam1.transform.GetChild (0).GetComponent<BeamDamage_Script> ().damageAmount = Mathf.RoundToInt(currentIntensity);
		beam2.transform.GetChild (0).GetComponent<BeamDamage_Script> ().damageAmount = Mathf.RoundToInt(currentIntensity);
		beam3.transform.GetChild (0).GetComponent<BeamDamage_Script> ().damageAmount = Mathf.RoundToInt(currentIntensity);
		beam4.transform.GetChild (0).GetComponent<BeamDamage_Script> ().damageAmount = Mathf.RoundToInt(currentIntensity);
		beam5.transform.GetChild (0).GetComponent<BeamDamage_Script> ().damageAmount = Mathf.RoundToInt(currentIntensity);
		beam6.transform.GetChild (0).GetComponent<BeamDamage_Script> ().damageAmount = Mathf.RoundToInt(currentIntensity);
		beam7.transform.GetChild (0).GetComponent<BeamDamage_Script> ().damageAmount = Mathf.RoundToInt(currentIntensity);

		controllerObject.GetComponent<ShipSetup_Script> ().TakePower (controllerObject.GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.powerUse * Time.deltaTime);

	}
}