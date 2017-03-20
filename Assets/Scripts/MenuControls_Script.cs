using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;

public class MenuControls_Script : MonoBehaviour
{

    public GameObject mainMenuObject;
    public GameObject newGameMenuObject;
    public GameObject joinGameMenuObject;
    public GameObject loadGameMenuObject;
    public GameObject preferencesMenuObject;

    public GameObject AddressField;
    public string adderss;

    public GameObject loadWorldUiPrefab;

    public void ShowMainMenu()
    {
        mainMenuObject.transform.localPosition = new Vector3(0, 0, 0);
        newGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        joinGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        loadGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        preferencesMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
    }

    public void ShowNewGameMenu()
    {
        mainMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        newGameMenuObject.transform.localPosition = new Vector3(0, 0, 0);
        joinGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        loadGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        preferencesMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
    }

    public void ShowJoinGameMenu()
    {
        mainMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        newGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        joinGameMenuObject.transform.localPosition = new Vector3(0, 0, 0);
        loadGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        preferencesMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
    }

    public void ShowLoadGameMenu()
    {
        GetSavedWorlds();

        mainMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        newGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        joinGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        loadGameMenuObject.transform.localPosition = new Vector3(0, 0, 0);
        preferencesMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
    }

    public void ShowPrefsMenu()
    {
        mainMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        newGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        joinGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        loadGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        preferencesMenuObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void SetJoinIp()
    {
        adderss = AddressField.GetComponent<InputField>().text;
        GameObject.Find("NM").GetComponent<NetworkManager>().serverBindAddress = adderss;
    }

    void Update()
    {
        adderss = AddressField.GetComponent<InputField>().text;
    }

    void Start()
    {

    }

    public void GetSavedWorlds()
    {
        foreach (Transform child in GameObject.Find("Load World Scroll Content").transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath + "/Saves");
        DirectoryInfo[] fileInfo = dirInfo.GetDirectories();

        Debug.Log(gameObject.name + ": Found " + fileInfo.Length + " world files.");

        foreach (DirectoryInfo _world in fileInfo)
        {
            GameObject worldUI = Instantiate(loadWorldUiPrefab) as GameObject;
            worldUI.transform.SetParent(GameObject.Find("Load World Scroll Content").transform);
            worldUI.transform.position = GameObject.Find("Load World Scroll Content").transform.position;
            worldUI.transform.localScale = new Vector3(1, 1, 1);
            worldUI.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = _world.Name;
            worldUI.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = _world.CreationTime.ToString();
            Debug.Log(_world);
        }
    }
}