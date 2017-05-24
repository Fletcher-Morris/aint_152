using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Ship
{
    public string shipName;

    public int maxShipHealth;
    public int shipHealth;
    public bool invincible;
    public bool randomPosition;
    
    public Engine shipEngine;
    public Shield shipShield;
    public Turret shipTurret;
    public Reactor shipReactor;

    public Vector3 shipPos;
    public Vector3 shipRot;

    public Ship()
    {
        shipName = "New Ship";
        maxShipHealth = 100;
        shipHealth = 100;
        invincible = false;
        randomPosition = false;
        shipEngine = new Engine();
        shipShield = new Shield();
        shipTurret = new Turret();
        shipReactor = new Reactor();

        shipPos = new Vector3(0, 0, 0);
        shipRot = new Vector3(0, 0, 0);
    }

    public Ship(string _name, int _health)
    {
        shipName = _name;
        maxShipHealth = _health;
        shipHealth = _health;
        invincible = false;
        randomPosition = false;
        shipEngine = new Engine();
        shipShield = new Shield();
        shipTurret = new Turret();

        shipPos = new Vector3(0, 0, 0);
        shipRot = new Vector3(0, 0, 0);
    }

    public Ship(Vector3 _pos)
    {
        shipName = "New Ship";
        maxShipHealth = 100;
        shipHealth = 100;
        invincible = false;
        randomPosition = false;
        shipEngine = new Engine();
        shipShield = new Shield();
        shipTurret = new Turret();

        shipPos = _pos;
        shipRot = new Vector3(0, 0, 0);
    }

    public Ship(string _name, int _health, Vector3 _pos, Vector3 _rot)
    {
        shipName = _name;
        maxShipHealth = _health;
        shipHealth = _health;
        invincible = false;
        randomPosition = false;
        shipEngine = new Engine();
        shipShield = new Shield();
        shipTurret = new Turret();

        shipPos = _pos;
        shipRot = _rot;
    }

    public Ship(string _name, int _health, Engine _engine, Shield _shield, Turret _turret)
    {
        shipName = _name;
        maxShipHealth = _health;
        shipHealth = _health;
        invincible = false;
        randomPosition = false;
        shipEngine = _engine;
        shipShield = _shield;
        shipTurret = new Turret();

        shipPos = new Vector3(0, 0, 0);
        shipRot = new Vector3(0, 0, 0);
    }

    public Ship(string _name, int _health, Engine _engine, Shield _shield, Turret _turret, Vector3 _pos, Vector3 _rot)
    {
        shipName = _name;
        maxShipHealth = _health;
        shipHealth = _health;
        invincible = false;
        randomPosition = false;
        shipEngine = _engine;
        shipShield = _shield;
        shipTurret = new Turret();

        shipPos = _pos;
        shipRot = _rot;
    }
}
