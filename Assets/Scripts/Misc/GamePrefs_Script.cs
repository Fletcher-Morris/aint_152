using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GamePrefs_Script : MonoBehaviour
{
    public GamePrefs gamePrefs;
    public bool getPrefsFromUi = false;

    void Start()
    {
        gamePrefs = gamePrefs.LoadPrefs();
        ApplyChangesLive();
        SetPrefsToUI();
    }

    public void SetPrefsToUI()
    {
        GameObject.Find("Player Name Field").GetComponent<InputField>().text = gamePrefs.playerName;
        GameObject.Find("Music Volume Slider").GetComponent<Slider>().value = gamePrefs.musicVolumeLevel;
        GameObject.Find("Effects Volume Slider").GetComponent<Slider>().value = gamePrefs.effectVolumeLevel;
        GameObject.Find("Fullscreen Toggle").GetComponent<Toggle>().isOn = gamePrefs.fullscreen;
		GameObject.Find("MSAA Toggle").GetComponent<Toggle>().isOn = gamePrefs.msaa;
    }

    public void GetPrefsFromUI()
    {
        gamePrefs = gamePrefs.LoadPrefs();
        gamePrefs.playerName = GameObject.Find("Player Name Field").GetComponent<InputField>().text;
        gamePrefs.musicVolumeLevel = GameObject.Find("Music Volume Slider").GetComponent<Slider>().value;
        gamePrefs.effectVolumeLevel = GameObject.Find("Effects Volume Slider").GetComponent<Slider>().value;
        gamePrefs.fullscreen = GameObject.Find("Fullscreen Toggle").GetComponent<Toggle>().isOn;
		gamePrefs.msaa = GameObject.Find("MSAA Toggle").GetComponent<Toggle>().isOn;
    }

    public void CancelChanges()
    {
        getPrefsFromUi = false;
        gamePrefs = gamePrefs.LoadPrefs();
    }

    public void SaveChanges()
    {
        getPrefsFromUi = false;
        ApplyChangesLive();
        gamePrefs.SavePrefs();
    }

    public void ApplyChangesLive()
    {
        if (gamePrefs.fullscreen)
        {
            Screen.SetResolution(Screen.resolutions[Screen.resolutions.Length - 1].width, Screen.resolutions[Screen.resolutions.Length - 1].height, true);
        }
        else
        {
            Screen.SetResolution(Screen.resolutions[Screen.resolutions.Length - 1].width / 2, Screen.resolutions[Screen.resolutions.Length - 1].height / 2, false);
        }

		if (gamePrefs.msaa) {
			QualitySettings.antiAliasing = 8;
		} else {
			QualitySettings.antiAliasing = 0;
		}

		if (gamePrefs.vsync) {
			QualitySettings.vSyncCount = 1;
		} else {
			QualitySettings.vSyncCount = 0;
		}
    }

    void Update()
    {
        if (getPrefsFromUi && GameObject.Find("Preferences Panel"))
        {
            GetPrefsFromUI(); 
        }
    }
}
