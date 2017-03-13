using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkLauncher_Script : MonoBehaviour {

	NetworkClient _client;
	public bool isAtStartup = true;

    public World _world;

	public void SetupClient(){
		gameObject.GetComponent<NetworkManager> ().StartClient ();
        Network.Connect(gameObject.GetComponent<NetworkManager>().networkAddress, gameObject.GetComponent<NetworkManager>().networkPort);
	}

    public void SetupHost(){
        TestSave();
        gameObject.GetComponent<NetworkManager> ().StartHost ();
	}

    public void TestSave()
    {
        _world.worldName = "New World";
        //_world.worldName = GameObject.Find("Save Name Field").GetComponent<InputField>().text;
        Ship ship1 = new Ship("Voyager", 500);
        Ship ship2 = new Ship("Normandy", 200);
        Ship ship3 = new Ship("Home 1", 1000);
        ship1.shipPos = new Vector3(-3, 0, 0);
        ship2.shipPos = new Vector3(0, 0, 0);
        ship3.shipPos = new Vector3(3, 0, 0);
        _world.ships.Add(ship1);
        _world.ships.Add(ship2);
        _world.ships.Add(ship3);
        _world.SaveWorld();
    }
}
