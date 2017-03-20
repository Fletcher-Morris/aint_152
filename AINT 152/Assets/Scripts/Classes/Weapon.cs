using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string weaponName;
    public float weaponDamage;
    public float weaponRange;

    public Weapon()
    {
        weaponName = "New Weapon";
        weaponDamage = 10f;
        weaponRange = 100;
    }
}