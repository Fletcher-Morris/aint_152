using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Asteroid
{
    public int asteroidSize;
    public int asteroidVariation;
    public Vector3 asteroidPos;
    public Vector3 asteroidRot;

    public Asteroid()
    {
        asteroidSize = 1;
        asteroidVariation = 1;
        asteroidPos = new Vector3(0, 0, 0);
        asteroidRot = new Vector3(0, 0, 0);
    }
}