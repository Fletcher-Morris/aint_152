using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuControls_Script : MonoBehaviour
{
    public GameObject mainPanelObject;
    public GameObject prefsPanelObject;

    public void ShowMainPanel()
    {
        mainPanelObject.SetActive(true);
        prefsPanelObject.SetActive(false);
    }

    public void ShowPrefsPanel()
    {
        mainPanelObject.SetActive(false);
        prefsPanelObject.SetActive(true);
    }
}
