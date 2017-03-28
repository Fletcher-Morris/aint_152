using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string weaponName;
    public float bulletSpeed;
    public float bulletRange;
    public int bulletDamage;
    public string bulletType;
    public bool auto;
    public float shootDelay;
    public int powerUse;

    public Weapon()
    {
        weaponName = "New Weapon";
        bulletSpeed = 10f;
        bulletDamage = 10;
        bulletRange = 30;
        bulletType = "projectile";
        auto = true;
        shootDelay = 0.3f;
        powerUse = 2;
    }
}