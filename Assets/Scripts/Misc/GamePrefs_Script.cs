using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GamePrefs_Script : MonoBehaviour
{
    public GamePrefs gamePrefs;
    public bool getPrefsFromUi = false;
    public Behaviour netHud;

    void Start()
    {
        gamePrefs = gamePrefs.LoadPrefs();
        SetPrefsToUI();
    }

    public void SetPrefsToUI()
    {
        GameObject.Find("Player Name Field").GetComponent<InputField>().text = gamePrefs.playerName;
        GameObject.Find("Player Description Field").GetComponent<InputField>().text = gamePrefs.playerDescrpition;
        GameObject.Find("Volume Slider").GetComponent<Slider>().value = gamePrefs.volumeLevel;
    }

    public void GetPrefsFromUI()
    {
        gamePrefs = gamePrefs.LoadPrefs();
        gamePrefs.playerName = GameObject.Find("Player Name Field").GetComponent<InputField>().text;
        gamePrefs.playerDescrpition = GameObject.Find("Player Description Field").GetComponent<InputField>().text;
        gamePrefs.volumeLevel = GameObject.Find("Volume Slider").GetComponent<Slider>().value;
    }

    public void CancelChanges()
    {
        getPrefsFromUi = false;
        gamePrefs = gamePrefs.LoadPrefs();
    }

    public void SaveChanges()
    {
        getPrefsFromUi = false;
        gamePrefs.SavePrefs();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftCommand))
            {
                netHud.enabled = !netHud.enabled; 
            }
        }

        if (getPrefsFromUi)
        {
            GetPrefsFromUI(); 
        }
    }
}
