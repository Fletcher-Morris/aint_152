using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretController_Script : MonoBehaviour
{

    public float rotateSpeed = 1;
    public GameObject turretObject;
    public bool canRotate = false;

	public Sprite ionBlasterTurretSprite;
    public Sprite fusionMineTurretSprite;
    public Sprite hunterLauncherTurretSprite;
    public Sprite quantumPrismTurretSprite;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            canRotate = !canRotate;
        }

        if (canRotate)
        {
            LookAtMousePod();
        }

		if (GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.weaponType == "Ion Blaster") {
			turretObject.GetComponent<SpriteRenderer> ().sprite = ionBlasterTurretSprite;
			turretObject.transform.GetChild (0).gameObject.transform.localPosition = new Vector3 (0,0.2f,0);
		}
        else if (GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.weaponType == "Fusion Mine")
        {
            turretObject.GetComponent<SpriteRenderer>().sprite = fusionMineTurretSprite;
            turretObject.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (GetComponent<ShipSetup_Script>().shipDetails.shipTurret.turretWeapon.weaponType == "Hunter Launcher")
        {
            turretObject.GetComponent<SpriteRenderer>().sprite = hunterLauncherTurretSprite;
            turretObject.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.turretWeapon.weaponType == "Quantum Prism")
        {
			turretObject.GetComponent<SpriteRenderer> ().sprite = quantumPrismTurretSprite;
			turretObject.transform.GetChild (0).gameObject.transform.localPosition = new Vector3 (0,0,0);
		}
    }

    void LookAtMousePod()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * rotateSpeed * Mathf.Rad2Deg;
        turretObject.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}