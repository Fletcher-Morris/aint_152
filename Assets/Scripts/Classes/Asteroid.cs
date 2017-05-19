using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Asteroid
{
    public string asteroidSize;
    public int asteroidVariation;
    public Vector3 asteroidPos;
    public Vector3 asteroidRot;

    public Asteroid()
    {
        asteroidSize = "Large";
        asteroidVariation = 1;
        asteroidPos = new Vector3(0, 0, 0);
        asteroidRot = new Vector3(0, 0, 0);
    }

    public Asteroid(Vector3 _asteroidPos)
    {
        asteroidSize = "Large";
        asteroidVariation = 1;
        asteroidPos = _asteroidPos;
        asteroidRot = new Vector3(0, 0, 0);
    }

    public Asteroid(Vector3 _asteroidPos, Vector3 _asteroidRot)
    {
        asteroidSize = "Large";
        asteroidVariation = 1;
        asteroidPos = _asteroidPos;
        asteroidRot = _asteroidRot;
    }

    public Asteroid(string _asteroidsSize, Vector3 _asteroidPos, Vector3 _asteroidRot)
    {
        asteroidSize = _asteroidsSize;
        asteroidVariation = 1;
        asteroidPos = _asteroidPos;
        asteroidRot = _asteroidRot;
    }

    public Asteroid(string _asteroidSize, int _asteroidVariation, Vector3 _asteroidPos, Vector3 _asteroidRot)
    {
        asteroidSize = _asteroidSize;
        asteroidVariation = _asteroidVariation;
        asteroidPos = _asteroidPos;
        asteroidRot = _asteroidRot;
    }
}