using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDamage_Script : MonoBehaviour
{

	public int damageAmount;
	public bool damagePlayer;
	public bool damageEnemies;
	public bool damageOther;

	private void OnTriggerStay2D(Collider2D collision)
	{
		var hit = collision.gameObject;

		if (hit.gameObject.tag == "Player" && damagePlayer)
		{
			var health = hit.GetComponent<ShipSetup_Script>();
			if (health != null)
			{
				health.TakeDamage(damageAmount);
			} 
		}
		else if (hit.gameObject.tag == "Enemy" && damageEnemies)
		{
			var health = hit.GetComponent<ShipSetup_Script>();
			if (health != null)
			{
				health.TakeDamage(damageAmount);

				GameObject.Find ("Player Ship").GetComponent<ShipSetup_Script> ().shipDetails.shipTurret.AddExperience ();
			}
		}
		else if (hit.GetComponent<GenericHealth_Script>())
		{
			var health = hit.GetComponent<GenericHealth_Script>();
			if (health != null)
			{
				health.TakeDamage(damageAmount);
			}
		}
	}

}