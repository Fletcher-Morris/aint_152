﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Ship
{
    public string shipName;

    public bool playerShip;

    public int shipHealth;

    public Reactor shipReactor;
    public Engine shipEngine;
    public Shield shipShield;
    public Turret shipTurret;

    public Vector3 shipPos;
    public Vector3 shipRot;

    public Ship()
    {
        shipName = "New Ship";
        playerShip = false;
        shipHealth = 100;
        shipReactor = new Reactor("Magnox Fusion", 1000000000, 2000);
        shipEngine = new Engine();
        shipShield = new Shield();
        shipTurret = new Turret();

        shipPos = new Vector3(0, 0, 0);
        shipRot = new Vector3(0, 0, 0);
    }

    public Ship(string _name, int _health)
    {
        shipName = _name;
        playerShip = false;
        shipHealth = _health;
        shipReactor = new Reactor("Antimatter", 2000000000, 1500);
        shipEngine = new Engine();
        shipShield = new Shield();
        shipTurret = new Turret();

        shipPos = new Vector3(0, 0, 0);
        shipRot = new Vector3(0, 0, 0);
    }

    public Ship(string _name, int _health, bool _playerShip)
    {
        shipName = _name;
        playerShip = _playerShip;
        shipHealth = _health;
        shipReactor = new Reactor("Antimatter", 2000000000, 1500);
        shipEngine = new Engine();
        shipShield = new Shield();
        shipTurret = new Turret();

        shipPos = new Vector3(0, 0, 0);
        shipRot = new Vector3(0, 0, 0);
    }
}
