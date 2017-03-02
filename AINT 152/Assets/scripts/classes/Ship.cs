using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Ship : MonoBehaviour {

    public string shipName;
    public int health;

    public Vector3 pos;
    public Vector3 rot;

    public Ship()
    {
        name = "New Ship";

        health = 100;

        pos = new Vector3(0, 0, 0);
        rot = new Vector3(0, 0, 0);
    }
}
