using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Ship
{
    public string shipName;

    public bool isPlayer;

    public int shipHealth;
    
    public Engine shipEngine;
    public Shield shipShield;
    public Turret shipTurret;

    public Vector3 shipPos;
    public Vector3 shipRot;

    public Ship()
    {
        shipName = "New Ship";
        isPlayer = false;
        shipHealth = 100;
        shipEngine = new Engine();
        shipShield = new Shield();
        shipTurret = new Turret();

        shipPos = new Vector3(0, 0, 0);
        shipRot = new Vector3(0, 0, 0);
    }

    public Ship(string _name, int _health)
    {
        shipName = _name;
        isPlayer = false;
        shipHealth = _health;
        shipEngine = new Engine();
        shipShield = new Shield();
        shipTurret = new Turret();

        shipPos = new Vector3(0, 0, 0);
        shipRot = new Vector3(0, 0, 0);
    }

    public Ship(string _name, int _health, Vector3 _pos, Vector3 _rot)
    {
        shipName = _name;
        isPlayer = false;
        shipHealth = _health;
        shipEngine = new Engine();
        shipShield = new Shield();
        shipTurret = new Turret();

        shipPos = _pos;
        shipRot = _rot;
    }

    public Ship(string _name, int _health, bool _isPlayer, Vector3 _pos, Vector3 _rot)
    {
        shipName = _name;
        isPlayer = _isPlayer;
        shipHealth = _health;
        shipEngine = new Engine();
        shipShield = new Shield();
        shipTurret = new Turret();

        shipPos = _pos;
        shipRot = _rot;
    }

    public Ship(string _name, int _health, Engine _engine, Shield _shield, Turret _turret)
    {
        shipName = _name;
        isPlayer = false;
        shipHealth = _health;
        shipEngine = _engine;
        shipShield = _shield;
        shipTurret = new Turret();

        shipPos = new Vector3(0, 0, 0);
        shipRot = new Vector3(0, 0, 0);
    }

    public Ship(string _name, int _health, Engine _engine, Shield _shield, Turret _turret, Vector3 _pos, Vector3 _rot)
    {
        shipName = _name;
        isPlayer = false;
        shipHealth = _health;
        shipEngine = _engine;
        shipShield = _shield;
        shipTurret = new Turret();

        shipPos = _pos;
        shipRot = _rot;
    }
}
