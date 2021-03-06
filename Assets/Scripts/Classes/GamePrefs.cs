﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GamePrefs
{

    public string playerName;
    public int sensitivity;
    public float musicVolumeLevel;
    public float effectVolumeLevel;
    public int style;
    public bool fullscreen;
    public bool vsync;
    public bool msaa;

    public GamePrefs(string _playerName, int _sensitivity, int _musicVolumeLevel, int _effectVolumeLevel, int _style, bool _vsync, bool _fullscreen, bool _msaa)
    {
        playerName = _playerName;
        sensitivity = _sensitivity;
        musicVolumeLevel = _musicVolumeLevel;
        effectVolumeLevel = _effectVolumeLevel;
        style = _style;
        vsync = _vsync;
        fullscreen = _fullscreen;
        msaa = _msaa;
    }

    public GamePrefs()
    {
        playerName = "Ace";
        sensitivity = 10;
        musicVolumeLevel = 5;
        effectVolumeLevel = 5;
        style = 1;
        vsync = true;
        fullscreen = false;
        msaa = false;
    }

    public void SavePrefs(GamePrefs _prefs)
    {
        string jsonString = JsonUtility.ToJson(_prefs);
        try
        {
            File.WriteAllText(Application.dataPath + "/Preferences.json", jsonString.ToString());
			Debug.Log(System.DateTime.Now.ToString() + "   Trying To Save Preferences File.");
        }
        catch (System.Exception)
        {
			Debug.LogWarning(System.DateTime.Now.ToString() + "   COULD NOT SAVE PREFERENCES FILE, TRYING AGAIN.");
            Directory.CreateDirectory(Application.dataPath + "/Preferences.json");
        }

		Debug.Log(System.DateTime.Now.ToString() + "  Saved Preferences File.");
    }

    public void SavePrefs()
    {
        string jsonString = JsonUtility.ToJson(this);
        try
        {
            File.WriteAllText(Application.dataPath + "/Preferences.json", jsonString.ToString());
			Debug.Log(System.DateTime.Now.ToString() + "   Trying To Save Preferences File.");
        }
        catch (System.Exception)
        {
			Debug.LogWarning(System.DateTime.Now.ToString() + "   COULD NOT SAVE PREFERENCES FILE, TRYING AGAIN.");
            Directory.CreateDirectory(Application.dataPath + "/Preferences.json");
        }

		Debug.Log(System.DateTime.Now.ToString() + "   Saved Preferences File.");
    }

    public GamePrefs LoadPrefs()
    {
        GamePrefs _gamePrefs = new GamePrefs();
        try
        {
			Debug.Log(System.DateTime.Now.ToString() + "   Trying To Load Preferences File.");
            string jsonString = File.ReadAllText(Application.dataPath + "/Preferences.json");
            _gamePrefs = JsonUtility.FromJson<GamePrefs>(jsonString);
        }
        catch (System.Exception)
        {
			Debug.LogWarning(System.DateTime.Now.ToString() + "   COULD NOT LOAD PREFERENCES FILE, MAKING A NEW ONE.");
            SavePrefs(_gamePrefs);
        }
		Debug.Log(System.DateTime.Now.ToString() + "   Loaded Preferences File.");
        return _gamePrefs;
    }
}
