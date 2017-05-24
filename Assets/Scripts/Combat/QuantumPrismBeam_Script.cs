using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumPrismBeam_Script : MonoBehaviour
{
	public float beamLength;
	public float maxIntensity = 1;
    public int damageFrequency = 10;
	public float speed = 10;
	public float sinSpeed = 5;

    public AudioClip chargeUpAudio;
    public AudioClip loopAudio;

	public GameObject controllerObject;
	public float timeSpentShooting;
	public float currentIntensity = 0;
	public float startAngle = 45;
	private float angleProgress;
	public float minAngle = 10;
	public float progressPercent = 0;

	public GameObject beam1;
	public GameObject beam2;
	public GameObject beam3;
	public GameObject beam4;
	public GameObject beam5;
	public GameObject beam6;
	public GameObject beam7;
	public GameObject whiteBeam;

	void Start(){

        StartCoroutine(SwapAudio());

		Vector3 beamScale = transform.localScale;
		angleProgress = startAngle;
		currentIntensity = 0;

		transform.eulerAngles = new Vector3 (transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, transform.parent.rotation.eulerAngles.z - 90);

		beam1.transform.GetChild (0).transform.localScale = new Vector3 (beamLength, 0.3f, beamScale.z);
		beam2.transform.GetChild (0).transform.localScale = new Vector3 (beamLength, 0.3f, beamScale.z);
		beam3.transform.GetChild (0).transform.localScale = new Vector3 (beamLength, 0.3f, beamScale.z);
		beam4.transform.GetChild (0).transform.localScale = new Vector3 (beamLength, 0.3f, beamScale.z);
		beam5.transform.GetChild (0).transform.localScale = new Vector3 (beamLength, 0.3f, beamScale.z);
		beam6.transform.GetChild (0).transform.localScale = new Vector3 (beamLength, 0.3f, beamScale.z);
		beam7.transform.GetChild (0).transform.localScale = new Vector3 (beamLength, 0.3f, beamScale.z);
		whiteBeam.transform.GetChild (0).transform.localScale = new Vector3 (beamLength, 2f, beamScale.z);

		beam1.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + ((1f/7f) * 4f * (Mathf.PI / 2)))));
		beam2.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + ((2f/7f) * 4f * (Mathf.PI / 2)))));
		beam3.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + ((3f/7f) * 4f * (Mathf.PI / 2)))));
		beam4.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + ((4f/7f) * 4f * (Mathf.PI / 2)))));
		beam5.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + ((5f/7f) * 4f * (Mathf.PI / 2)))));
		beam6.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + ((6f/7f) * 4f * (Mathf.PI / 2)))));
		beam7.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + ((7f/7f) * 4f * (Mathf.PI / 2)))));

		beam1.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,-Mathf.Sin(sinSpeed * Time.time + ((1f/7f) * 4f * (Mathf.PI / 2))) / 2, -Mathf.Sin(sinSpeed * Time.time + ((1f / 7f) * 4f * (Mathf.PI / 2))) / 2);
		beam2.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,-Mathf.Sin(sinSpeed * Time.time + ((2f/7f) * 4f * (Mathf.PI / 2))) / 2, -Mathf.Sin(sinSpeed * Time.time + ((2f / 7f) * 4f * (Mathf.PI / 2))) / 2);
		beam3.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,-Mathf.Sin(sinSpeed * Time.time + ((3f/7f) * 4f * (Mathf.PI / 2))) / 2, -Mathf.Sin(sinSpeed * Time.time + ((3f / 7f) * 4f * (Mathf.PI / 2))) / 2);
		beam4.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,-Mathf.Sin(sinSpeed * Time.time + ((4f/7f) * 4f * (Mathf.PI / 2))) / 2, -Mathf.Sin(sinSpeed * Time.time + ((4f / 7f) * 4f * (Mathf.PI / 2))) / 2);
		beam5.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,-Mathf.Sin(sinSpeed * Time.time + ((5f/7f) * 4f * (Mathf.PI / 2))) / 2, -Mathf.Sin(sinSpeed * Time.time + ((5f / 7f) * 4f * (Mathf.PI / 2))) / 2);
		beam6.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,-Mathf.Sin(sinSpeed * Time.time + ((6f/7f) * 4f * (Mathf.PI / 2))) / 2, -Mathf.Sin(sinSpeed * Time.time + ((6f / 7f) * 4f * (Mathf.PI / 2))) / 2);
		beam7.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,-Mathf.Sin(sinSpeed * Time.time + ((7f/7f) * 4f * (Mathf.PI / 2))) / 2, -Mathf.Sin(sinSpeed * Time.time + ((7f / 7f) * 4f * (Mathf.PI / 2))) / 2);
		whiteBeam.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,0,0);

		beam1.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color(beam1.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.r, beam1.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.b,beam1.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.g, 3/angleProgress);
		beam2.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color(beam2.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.r, beam2.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.b,beam2.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.g, 3/angleProgress);
		beam3.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color(beam3.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.r, beam3.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.b,beam3.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.g, 3/angleProgress);
		beam4.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color(beam4.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.r, beam4.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.b,beam4.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.g, 3/angleProgress);
		beam5.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color(beam5.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.r, beam5.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.b,beam5.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.g, 3/angleProgress);
		beam6.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color(beam6.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.r, beam6.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.b,beam6.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.g, 3/angleProgress);
		beam7.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color(beam7.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.r, beam7.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.b,beam7.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.g, 3/angleProgress);
		whiteBeam.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color (0,0,0,0);

        beam1.transform.GetChild(0).gameObject.GetComponent<BeamDamage_Script>().damageSync = damageFrequency;
        beam2.transform.GetChild(0).gameObject.GetComponent<BeamDamage_Script>().damageSync = damageFrequency;
        beam3.transform.GetChild(0).gameObject.GetComponent<BeamDamage_Script>().damageSync = damageFrequency;
        beam4.transform.GetChild(0).gameObject.GetComponent<BeamDamage_Script>().damageSync = damageFrequency;
        beam5.transform.GetChild(0).gameObject.GetComponent<BeamDamage_Script>().damageSync = damageFrequency;
        beam6.transform.GetChild(0).gameObject.GetComponent<BeamDamage_Script>().damageSync = damageFrequency;
        beam7.transform.GetChild(0).gameObject.GetComponent<BeamDamage_Script>().damageSync = damageFrequency;
    }

	void Update()
	{
		transform.eulerAngles = new Vector3 (transform.parent.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, transform.parent.rotation.eulerAngles.z - 90);

		timeSpentShooting += Time.deltaTime;

		if (angleProgress > minAngle) {
			angleProgress -= Time.deltaTime * speed;
		} else {
			angleProgress = minAngle;
			whiteBeam.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
		}

		progressPercent = (startAngle - angleProgress) / startAngle;

		if (currentIntensity < maxIntensity) {
			//currentIntensity = maxIntensity * progressPercent;
            currentIntensity = maxIntensity;

        } else {
			currentIntensity = maxIntensity;
		}

		beam1.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + ((1f/7f) * 4f * (Mathf.PI / 2)))));
		beam2.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + ((2f/7f) * 4f * (Mathf.PI / 2)))));
		beam3.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + ((3f/7f) * 4f * (Mathf.PI / 2)))));
		beam4.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + ((4f/7f) * 4f * (Mathf.PI / 2)))));
		beam5.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + ((5f/7f) * 4f * (Mathf.PI / 2)))));
		beam6.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + ((6f/7f) * 4f * (Mathf.PI / 2)))));
		beam7.transform.localEulerAngles = new Vector3 (0, 0, transform.parent.transform.rotation.z + (angleProgress * Mathf.Sin(sinSpeed * Time.time + ((7f/7f) * 4f * (Mathf.PI / 2)))));

		beam1.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,-Mathf.Sin(sinSpeed * Time.time + ((1f/7f) * 4f * (Mathf.PI / 2))) / 2,0);
		beam2.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,-Mathf.Sin(sinSpeed * Time.time + ((2f/7f) * 4f * (Mathf.PI / 2))) / 2,0);
		beam3.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,-Mathf.Sin(sinSpeed * Time.time + ((3f/7f) * 4f * (Mathf.PI / 2))) / 2,0);
		beam4.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,-Mathf.Sin(sinSpeed * Time.time + ((4f/7f) * 4f * (Mathf.PI / 2))) / 2,0);
		beam5.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,-Mathf.Sin(sinSpeed * Time.time + ((5f/7f) * 4f * (Mathf.PI / 2))) / 2,0);
		beam6.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,-Mathf.Sin(sinSpeed * Time.time + ((6f/7f) * 4f * (Mathf.PI / 2))) / 2,0);
		beam7.transform.GetChild (0).transform.localPosition = new Vector3 (-beamLength / 6.25f,-Mathf.Sin(sinSpeed * Time.time + ((7f/7f) * 4f * (Mathf.PI / 2))) / 2,0);

		beam1.transform.GetChild (0).GetComponent<BeamDamage_Script> ().damageAmount = currentIntensity;
		beam2.transform.GetChild (0).GetComponent<BeamDamage_Script> ().damageAmount = currentIntensity;
		beam3.transform.GetChild (0).GetComponent<BeamDamage_Script> ().damageAmount = currentIntensity;
		beam4.transform.GetChild (0).GetComponent<BeamDamage_Script> ().damageAmount = currentIntensity;
		beam5.transform.GetChild (0).GetComponent<BeamDamage_Script> ().damageAmount = currentIntensity;
		beam6.transform.GetChild (0).GetComponent<BeamDamage_Script> ().damageAmount = currentIntensity;
		beam7.transform.GetChild (0).GetComponent<BeamDamage_Script> ().damageAmount = currentIntensity;

		beam1.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color(beam1.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.r, beam1.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.b,beam1.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.g, 3/angleProgress);
		beam2.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color(beam2.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.r, beam2.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.b,beam2.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.g, 3/angleProgress);
		beam3.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color(beam3.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.r, beam3.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.b,beam3.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.g, 3/angleProgress);
		beam4.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color(beam4.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.r, beam4.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.b,beam4.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.g, 3/angleProgress);
		beam5.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color(beam5.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.r, beam5.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.b,beam5.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.g, 3/angleProgress);
		beam6.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color(beam6.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.r, beam6.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.b,beam6.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.g, 3/angleProgress);
		beam7.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color(beam7.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.r, beam7.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.b,beam7.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color.g, 3/angleProgress);


		controllerObject.GetComponent<ShipSetup_Script> ().TakePower (controllerObject.GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.powerUse * Time.deltaTime);

	}

    public IEnumerator SwapAudio()
    {
        AudioSource audio = GetComponent<AudioSource>();

        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = loopAudio;
        audio.Play();
    }
}