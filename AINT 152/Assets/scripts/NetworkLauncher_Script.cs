using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkLauncher_Script : MonoBehaviour {

    public GameObject nmObject;

	NetworkClient _client;
	public bool isAtStartup = true;

    void Start()
    {

    }

	public void SetupClient(){
        nmObject.GetComponent<NetworkManager> ().StartClient ();
        Network.Connect(gameObject.GetComponent<NetworkManager>().networkAddress, gameObject.GetComponent<NetworkManager>().networkPort);
	}

    public void SetupHost(){
        nmObject.GetComponent<NetworkManager> ().StartHost ();
	}
}
