using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class Player
{
    public string playerName;
    public int playerHealth;
    public int playerSkin;

    public Player()
    {
        playerName = "";
        playerSkin = 0;
    }

    public Player(string _playerName)
    {
        playerName = _playerName;
        playerSkin = 0;
    }
}