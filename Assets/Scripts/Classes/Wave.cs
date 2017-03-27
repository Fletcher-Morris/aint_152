using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public List<Ship> ships;

    public Wave()
    {
        ships = new List<Ship>();
    }
}