using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GamePrefs{

    public string userName;
    public int sensitivity;
    public int volumeLevel;
    public string description;
    public int style;
    public int fpsCap;
    public bool fullscreen;
    public bool vsync;
    public bool msaa;

    public GamePrefs(string _userName, int _sensitivity, int _volumeLevel, string _description, int _style, int _fpsCap, bool _vsync, bool _fullscreen, bool _msaa)
    {
        userName = _userName;
        sensitivity = _sensitivity;
        volumeLevel = _volumeLevel;
        description = _description;
        style = _style;
        fpsCap = _fpsCap;
        vsync = _vsync;
        fullscreen = _fullscreen;
        msaa = _msaa;
    }

    public GamePrefs()
    {
        userName = "The Name The Player Uses";
        sensitivity = 10;
        volumeLevel = 10;
        description = "A Description Of The Player";
        style = 1;
        fpsCap = 60;
        vsync = false;
        fullscreen = false;
        msaa = false;
    }

    public void SavePrefs(GamePrefs _prefs)
    {
        string jsonString = JsonUtility.ToJson(_prefs);
        try
        {
            File.WriteAllText(Application.dataPath + "/Preferences.json", jsonString.ToString());
            Debug.Log("Saving preferences file.");
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Cannot find preferences file. Creating a new one.");
            Directory.CreateDirectory(Application.dataPath + "/Preferences.json");
        }
    }

    public void SavePrefs()
    {
        string jsonString = JsonUtility.ToJson(this);
        try
        {
            File.WriteAllText(Application.dataPath + "/Preferences.json", jsonString.ToString());
            Debug.Log("Saving preferences file.");
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Cannot find preferences file. Creating a new one.");
            Directory.CreateDirectory(Application.dataPath + "/Preferences.json");
        }
    }

    public GamePrefs LoadPrefs()
    {
        GamePrefs _gamePrefs = new GamePrefs();
        try
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/Preferences.json");
            _gamePrefs = JsonUtility.FromJson<GamePrefs>(jsonString);
        }
        catch (System.Exception)
        {
            SavePrefs(_gamePrefs);
        }
        Debug.Log("Loading preferences file.");
        return _gamePrefs;
    }
}
