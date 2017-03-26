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

    public GameObject titleTextObject;
    public float titleColourTimer = 1f;
    float titletimer = 1f;

    public void ShowMainMenu()
    {
        GameObject.Find("GM").GetComponent<GamePrefs_Script>().getPrefsFromUi = false;

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
        GameObject.Find("GM").GetComponent<GamePrefs_Script>().SetPrefsToUI();
        GameObject.Find("GM").GetComponent<GamePrefs_Script>().getPrefsFromUi = true;

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

        titletimer = titletimer - 1 * Time.deltaTime;

        if(titletimer <= 0)
        {
            titleTextObject.GetComponent<Text>().color = SwapColour(titleTextObject.GetComponent<Text>().color);
            titletimer = titleColourTimer;
        }
    }

    public void SavePreferencesChanges()
    {
        GameObject.Find("GM").GetComponent<GamePrefs_Script>().SaveChanges();
    }
    public void CancelPreferencesChanges()
    {
        GameObject.Find("GM").GetComponent<GamePrefs_Script>().CancelChanges();
    }

    Color SwapColour(Color _colour)
    {
        if(_colour == Color.white)
        {
            _colour = Color.blue;
        }
        else if (_colour == Color.blue)
        {
            _colour = Color.green;
        }
        else if (_colour == Color.green)
        {
            _colour = Color.yellow;
        }
        else if (_colour == Color.yellow)
        {
            _colour = Color.red;
        }
        else if (_colour == Color.red)
        {
            _colour = Color.magenta;
        }
        else if (_colour == Color.magenta)
        {
            _colour = Color.white;
        }

        return _colour;
    }

    void Start()
    {
        titletimer = titleColourTimer;
    }

    public void GetSavedWorlds()
    {
        foreach (Transform child in GameObject.Find("Load World Scroll Content").transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        if (Directory.Exists(Application.dataPath + "/Saves"))
        {
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
        else
        {
            Directory.CreateDirectory(Application.dataPath + "/Saves");
            Debug.LogWarning(gameObject.name + ": Saves Directory does not exist, creating a new one.");
        }
    }


    public void CreateWorldRelay()
    {
        GameObject.Find("WM").GetComponent<WorldGenerator_Script>().CreateWorld();
    }

    public void GenerateWorldRelay()
    {
        GameObject.Find("WM").GetComponent<WorldLoader_Script>().GenerateWorld();
    }

    public void SetupHostRelay()
    {
        GameObject.Find("WM").GetComponent<NetworkLauncher_Script>().SetupHost();
    }

    public void SetupClientRelay()
    {
        GameObject.Find("WM").GetComponent<NetworkLauncher_Script>().SetupClient();
    }
}