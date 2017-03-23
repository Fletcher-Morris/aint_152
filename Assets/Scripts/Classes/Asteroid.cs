﻿using System.Collections;
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

    public Asteroid(Vector3 _asteroidPos)
    {
        asteroidSize = 1;
        asteroidVariation = 1;
        asteroidPos = _asteroidPos;
        asteroidRot = new Vector3(0, 0, 0);
    }

    public Asteroid(int _asteroidSize, int _asteroidVariation, Vector3 _asteroidPos, Vector3 _asteroidRot)
    {
        asteroidSize = _asteroidSize;
        asteroidVariation = _asteroidVariation;
        asteroidPos = _asteroidPos;
        asteroidRot = _asteroidRot;
    }
}