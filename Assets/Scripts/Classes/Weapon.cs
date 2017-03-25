using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string weaponName;
    public float bulletSpeed;
    public float bulletRange;
    public string bulletType;
    public bool auto;
    public int clipSize;
    public float reloadTime;
    public float shootDelay;

    public Weapon()
    {
        weaponName = "New Weapon";
        bulletSpeed = 10f;
        bulletRange = 100;
        bulletType = "projectile";
        auto = false;
        clipSize = 10;
        reloadTime = 3f;
        shootDelay = .2f;
    }
}