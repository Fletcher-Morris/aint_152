using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        GameObject.Find("GM").GetComponent<GameState_Script>().SetStatePaused();
    }

    public void HidePauseMenu()
    {
        backgroundPanelObject.SetActive(false);
        mainPanelObject.SetActive(false);
        prefsPanelObject.SetActive(false);

        GameObject.Find("GM").GetComponent<GameState_Script>().SetStateNormal();
    }

    void Update()
    {
        if (GameObject.Find("GM").GetComponent<GameState_Script>().GetState() == "paused")
        {
            if (isShowingPauseMenu == false)
            {
                ShowMainPanel();
                isShowingPauseMenu = true;
            }
        }
        else if (GameObject.Find("GM").GetComponent<GameState_Script>().GetState() == "normal")
        {
            if(isShowingPauseMenu == true)
            {
                HidePauseMenu();
                isShowingPauseMenu = false;
            }
        }
    }
}
