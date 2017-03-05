using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MenuControls_Script : MonoBehaviour {

    public GameObject mainMenuObject;
    public GameObject newGameMenuObject;
    public GameObject joinGameMenuObject;
    public GameObject loadGameMenuObject;
    public GameObject preferencesMenuObject;

    public GameObject AddressField;

    public void ShowMainMenu()
    {
        mainMenuObject.transform.localPosition = new Vector3(0, 0, 0);
        newGameMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
        joinGameMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
        loadGameMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
        preferencesMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
    }

    public void ShowNewGameMenu()
    {
        mainMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
        newGameMenuObject.transform.localPosition = new Vector3(0, 0, 0);
        joinGameMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
        loadGameMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
        preferencesMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
    }

    public void ShowJoinGameMenu()
    {
        mainMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
        newGameMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
        joinGameMenuObject.transform.localPosition = new Vector3(0, 0, 0);
        loadGameMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
        preferencesMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
    }

    public void ShowLoadGameMenu()
    {
        mainMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
        newGameMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
        joinGameMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
        loadGameMenuObject.transform.localPosition = new Vector3(0, 0, 0);
        preferencesMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
    }

    public void ShowPrefsMenu()
    {
        mainMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
        newGameMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
        joinGameMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
        loadGameMenuObject.transform.localPosition = new Vector3(1000, 0, 0);
        preferencesMenuObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void SetJoinIp()
    {
        GameObject.Find("NM").GetComponent<NetworkManager>().serverBindAddress = AddressField.GetComponent<InputField>().text;
    }
}
