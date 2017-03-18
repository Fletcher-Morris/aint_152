using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkLauncher_Script : MonoBehaviour {

    public GameObject nmObject;

	NetworkClient _client;
	public bool isAtStartup = true;

    public World _world;

    void Start()
    {

    }

	public void SetupClient(){
        nmObject.GetComponent<NetworkManager> ().StartClient ();
        Network.Connect(gameObject.GetComponent<NetworkManager>().networkAddress, gameObject.GetComponent<NetworkManager>().networkPort);
	}

    public void SetupHost(){
        TestSave();
        nmObject.GetComponent<NetworkManager> ().StartHost ();
	}

    public void TestSave()
    {
        //_world.worldName = "New World";
        _world.worldName = GameObject.Find("Save Name Field").GetComponent<InputField>().text;
        Ship ship1 = new Ship("Voyager", 500);
        ship1.shipPos = new Vector3(0, 0, 0);
        _world.playerShips.Add(ship1);
        _world.SaveWorld();
    }
}
