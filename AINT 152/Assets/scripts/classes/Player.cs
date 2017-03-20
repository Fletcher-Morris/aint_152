using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;

[System.Serializable]
public class Player
{
    public string playerName;
    public int uniqueId;
    public int playerHealth;
    public int playerSkin;
    public string playerSpecialty;

    public Player()
    {
        playerName = "";
        uniqueId = GetID();
        playerSkin = 0;
        playerSpecialty = "";
    }

    public Player(string _playerName)
    {
        playerName = _playerName;
        uniqueId = GetID();
        playerSkin = 0;
        playerSpecialty = "";
    }

    public int GenerateNewID()
    {
        uniqueId = UnityEngine.Random.Range(1, 2147483647);
        if (!Network.isServer)
        {
            File.WriteAllText(Application.dataPath + "/Unique ID.txt", uniqueId.ToString());
            Debug.Log("Generating new unique player ID.");
        }
        return uniqueId;
    }

    public int GetID()
    {
        int value = 0;
        if (File.Exists(Application.dataPath + "/Unique ID.txt"))
        {
            value = Convert.ToInt32(File.ReadAllText(Application.dataPath + "/Unique ID.txt"));
        }
        else
        {
            value = GenerateNewID();
        }
        return value;
    }
}