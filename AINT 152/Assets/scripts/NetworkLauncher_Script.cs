using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkLauncher_Script : MonoBehaviour {

	NetworkClient _client;
	public bool isAtStartup = true;

    public World _world;

	public void SetupClient(){
		gameObject.GetComponent<NetworkManager> ().StartClient ();
	}

    public void SetupHost(){
		gameObject.GetComponent<NetworkManager> ().StartHost ();
        TestSave();
	}

    public void TestSave()
    {
        Ship ship1 = new Ship("Voyager", 500);
        Ship ship2 = new Ship("Normandy", 200);
        Ship ship3 = new Ship("Home 1", 1000);
        _world.ships.Add(ship1);
        _world.ships.Add(ship2);
        _world.ships.Add(ship3);
        _world.SaveWorld();
    }
}
