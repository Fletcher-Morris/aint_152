﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuControls_Script : MonoBehaviour
{
    public GameObject backgroundPanelObject;
    public GameObject mainPanelObject;
    public GameObject prefsPanelObject;

    bool isShowingPauseMenu = false;

    public void ResumeGame()
    {
        GameObject.Find("GM").GetComponent<GameState_Script>().UnPause();
    }

    public void ShowMainPanel()
    {
        backgroundPanelObject.SetActive(true);
        mainPanelObject.SetActive(true);
        prefsPanelObject.SetActive(false);
    }

    public void ShowPrefsPanel()
    {
        backgroundPanelObject.SetActive(true);
        mainPanelObject.SetActive(false);
        prefsPanelObject.SetActive(true);

        GameObject.Find("GM").GetComponent<GamePrefs_Script>().SetPrefsToUI();
        GameObject.Find("GM").GetComponent<GamePrefs_Script>().getPrefsFromUi = true;
    }
    public void SavePreferencesChanges()
    {
        GameObject.Find("GM").GetComponent<GamePrefs_Script>().SaveChanges();
    }
    public void CancelPreferencesChanges()
    {
        GameObject.Find("GM").GetComponent<GamePrefs_Script>().CancelChanges();
    }

    public void HidePauseMenu()
    {
        backgroundPanelObject.SetActive(false);
        mainPanelObject.SetActive(false);
        prefsPanelObject.SetActive(false);

		GameObject.Find ("GM").GetComponent<GameState_Script> ().SetState ("Normal");
    }

    public void LeaveGame()
    {
        SceneManager.LoadScene("Menu_Scene");
		GameObject.Find("GM").GetComponent<GameState_Script>().SetState ("Normal");
    }

    public void SaveGameRelay()
    {
        GameObject.Find("WM").GetComponent<WorldLoader_Script>().SaveTheWorld();
    }

    void Update()
    {
        if (GameObject.Find("GM").GetComponent<GameState_Script>().GetState() == "Paused")
        {
            if (isShowingPauseMenu == false)
            {
                ShowMainPanel();
                isShowingPauseMenu = true;
            }
        }
        else
        {
            if(isShowingPauseMenu == true)
            {
                HidePauseMenu();
                isShowingPauseMenu = false;
            }
        }
    }
}