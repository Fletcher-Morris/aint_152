using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PauseMenuControls_Script : MonoBehaviour
{
    public GameObject backgroundPanelObject;
    public GameObject mainPanelObject;
    public GameObject prefsPanelObject;

    bool isShowingPauseMenu = false;

    public void ShowMainPanel()
    {
        backgroundPanelObject.SetActive(true);
        mainPanelObject.SetActive(true);
        prefsPanelObject.SetActive(false);

        GameObject.Find("GM").GetComponent<GameState_Script>().SetStatePaused();
    }

    public void ShowPrefsPanel()
    {
        backgroundPanelObject.SetActive(true);
        mainPanelObject.SetActive(false);
        prefsPanelObject.SetActive(true);

        GameObject.Find("GM").GetComponent<GamePrefs_Script>().SetPrefsToUI();
        GameObject.Find("GM").GetComponent<GamePrefs_Script>().getPrefsFromUi = true;

        GameObject.Find("GM").GetComponent<GameState_Script>().SetStatePaused();
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

        GameObject.Find("GM").GetComponent<GameState_Script>().SetStateNormal();
    }

    public void LeaveGame()
    {
        GameObject.Find("NM").GetComponent<NetworkManager>().StopHost();
        GameObject.Find("NM").GetComponent<NetworkManager>().StopClient();
        GameObject.Find("GM").GetComponent<GameState_Script>().SetStateNormal();
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
        else if (GameObject.Find("GM").GetComponent<GameState_Script>().GetState() == "Normal")
        {
            if(isShowingPauseMenu == true)
            {
                HidePauseMenu();
                isShowingPauseMenu = false;
            }
        }
    }
}
