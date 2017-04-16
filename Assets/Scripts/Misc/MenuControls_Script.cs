using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Text;

public class MenuControls_Script : MonoBehaviour
{

    public GameObject mainMenuObject;
    public GameObject newGameMenuObject;
    public GameObject loadGameMenuObject;
    public GameObject preferencesMenuObject;

	public float cinematicTime = 12f;
	float cineTimer;
	bool skippedIntro = false;

    public GameObject loadWorldUiPrefab;

    public GameObject titleTextObject;
    public float titleColourTimer = 1f;
    float titletimer = 1f;

    public void ShowMainMenu()
    {
        GameObject.Find("GM").GetComponent<GamePrefs_Script>().getPrefsFromUi = false;

        mainMenuObject.transform.localPosition = new Vector3(0, 0, 0);
        newGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        loadGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        preferencesMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
    }

    public void ShowNewGameMenu()
    {
        mainMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        newGameMenuObject.transform.localPosition = new Vector3(0, 0, 0);
        loadGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        preferencesMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
    }

    public void ShowLoadGameMenu()
    {
        GetSavedWorlds();

        mainMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        newGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        loadGameMenuObject.transform.localPosition = new Vector3(0, 0, 0);
        preferencesMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
    }

    public void ShowPrefsMenu()
    {
        GameObject.Find("GM").GetComponent<GamePrefs_Script>().SetPrefsToUI();
        GameObject.Find("GM").GetComponent<GamePrefs_Script>().getPrefsFromUi = true;

        mainMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        newGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        loadGameMenuObject.transform.localPosition = new Vector3(10000, 0, 0);
        preferencesMenuObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    void Update()
    {
        titletimer = titletimer - 1 * Time.deltaTime;

        if(titletimer <= 0)
        {
            titleTextObject.GetComponent<Text>().color = SwapColour(titleTextObject.GetComponent<Text>().color);
            titletimer = titleColourTimer;
        }

		cineTimer = cineTimer - 1 * Time.deltaTime;

		if (cineTimer <= 0) {
			cineTimer = 0;
			GetComponent<CanvasGroup> ().alpha += 0.5f * Time.deltaTime;
			GetComponent<Canvas> ().enabled = false;
			GameObject.Destroy (GameObject.Find ("Press Any Key Canvas"));
		}
		if (GetComponent<CanvasGroup> ().alpha >= 1) {
			GetComponent<CanvasGroup> ().alpha = 1;
			GetComponent<Canvas> ().enabled = true;
		}

		if (Input.anyKeyDown && skippedIntro == false) {
			SkipIntro ();
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
		GetComponent<CanvasGroup> ().alpha = 0f;
		GetComponent<Canvas> ().enabled = false;

		if (GameObject.Find ("Pause Menu Canvas") && skippedIntro == false) {
			SkipIntro ();
		}

        titletimer = titleColourTimer;
		cineTimer = cinematicTime;
    }
		
	public void SkipIntro()
	{
		GameObject.Find ("Theif 1").SetActive(false);
		GameObject.Find ("Theif 2").SetActive(false);
		GameObject.Find ("Police Ship").SetActive(false);
		GameObject.Find ("Police Ship 2").SetActive(false);
		GameObject.Find ("Main Camera").GetComponent<Animator>().enabled = false;
		GameObject.Find ("Main Camera").transform.position = new Vector3 (-33.3f, 0f, -10f);
		GameObject.Destroy (GameObject.Find ("Pause Menu Canvas"));
		GameObject.Destroy (GameObject.Find ("Press Any Key Canvas"));
		GetComponent<Canvas> ().enabled = true;
		GetComponent<CanvasGroup> ().alpha = 1;
		skippedIntro = true;
	}

    public void GetSavedWorlds()
    {
        foreach (Transform child in GameObject.Find("Load World Scroll Content").transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        if (Directory.Exists(Application.dataPath + "/Data/Saves"))
        {
			DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath + "/Data/Saves");
			FileInfo[] fileInfo = dirInfo.GetFiles();

            Debug.Log(gameObject.name + ": Found " + fileInfo.Length + " world files.");


			foreach (FileInfo _world in fileInfo)
            {
				StringBuilder sb = new StringBuilder();

				if (_world.Name.EndsWith(".json")) {
					GameObject worldUI = Instantiate (loadWorldUiPrefab) as GameObject;
					worldUI.transform.SetParent (GameObject.Find ("Load World Scroll Content").transform);
					worldUI.transform.position = GameObject.Find ("Load World Scroll Content").transform.position;
					worldUI.transform.localScale = new Vector3 (1, 1, 1);
					worldUI.transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = sb.Append(Path.GetFileNameWithoutExtension(_world.Name)).ToString();
					worldUI.transform.GetChild (0).GetChild (1).GetComponent<Text> ().text = _world.CreationTime.ToString ();
					Debug.Log (_world);
				}
            }
        }
        else
        {
			Directory.CreateDirectory(Application.dataPath + "/Data/Saves");
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
}