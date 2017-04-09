using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string weaponName;
	public string weaponDescription;
    public float bulletSpeed;
    public float bulletRange;
    public int bulletDamage;
    public string weaponType;
    public bool auto;
    public float shootDelay;
    public int powerUse;
	public int weaponLevel;

	public Item weaponItem;

    public Weapon()
    {
        weaponName = "New Weapon";
		weaponDescription = "This peashooter of a gun is so reliable that it even works without a power source. Frankly, our scientists are baffled.";
        bulletSpeed = 10f;
        bulletDamage = 10;
        bulletRange = 30;
        weaponType = "Ion Blaster";
        auto = true;
        shootDelay = 0.2f;
        powerUse = 20;

		weaponItem = new Item (weaponName, "Weapon", Mathf.RoundToInt((bulletSpeed) * (bulletDamage * 3) * (1/shootDelay)), "Dmg: " + bulletDamage.ToString() + ", Speed: " + (1/shootDelay).ToString() + ", Velocity: " + bulletSpeed.ToString());
    }

	public Weapon(string _null)
	{
		if (_null == "null") {
			weaponName = "null";
			weaponDescription = "null";
			bulletSpeed = 0;
			bulletDamage = 0;
			bulletRange = 0;
			weaponType = "null";
			auto = false;
			shootDelay = 0;
			powerUse = 0;
		}
	}

	public void RandomizeWeapon(int minValue, int maxValue)
	{
		this.bulletDamage = Mathf.RoundToInt(Random.Range (1, 500));
		this.shootDelay = (Mathf.RoundToInt(Random.Range (5, 500)))/100;
		this.bulletSpeed = Mathf.RoundToInt(Random.Range (5, 100));

		this.GenerateWeaponItem ();

		if (this.weaponItem.itemValue < minValue || this.weaponItem.itemValue > maxValue) {
			this.RandomizeWeapon (minValue, maxValue);
		}

		Debug.LogFormat ("Generated Weapon: " + this.weaponItem.itemValue.ToString ());

		this.GenerateWeaponItem ();
	}

	public void GenerateWeaponItem()
	{
		weaponItem = new Item (weaponName, "Weapon", Mathf.RoundToInt(bulletSpeed * bulletDamage * (1/shootDelay)), "Dmg: " + bulletDamage.ToString() + ", Speed: " + (1/shootDelay).ToString());
	}
}