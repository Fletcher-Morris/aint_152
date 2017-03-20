using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Turret
{
    public string turretName;
    public Weapon turretWeapon;
    public float rotationSpeed;

    public Turret()
    {
        turretName = "New Turret";
        turretWeapon = new Weapon();
        rotationSpeed = 120f;
    }
}
