using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GamePrefs_Script : MonoBehaviour
{
    public GamePrefs gamePrefs;

    void Start()
    {
        gamePrefs = gamePrefs.LoadPrefs();
    }

    public void GetPrefsFromUI()
    {
        gamePrefs.playerName = GameObject.Find("Player Name Field").GetComponent<InputField>().text;
        gamePrefs.playerDescrpition = GameObject.Find("Player Description Field").GetComponent<InputField>().text;
    }

    public void CancelChanges()
    {
        gamePrefs = gamePrefs.LoadPrefs();
    }

    public void SaveChanges()
    {
        gamePrefs.SavePrefs();
    }
}
