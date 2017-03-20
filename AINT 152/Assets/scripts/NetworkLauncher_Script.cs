using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkLauncher_Script : MonoBehaviour {

	NetworkClient _client;
	public bool isAtStartup = true;

    void Start()
    {

    }

	public void SetupClient(){
        GameObject.Find("NM").GetComponent<NetworkManager>().networkAddress = GameObject.Find("Address Field").GetComponent<InputField>().text;
        GameObject.Find("NM").GetComponent<NetworkManager> ().StartClient ();
        Network.Connect(GameObject.Find("NM").GetComponent<NetworkManager>().networkAddress, GameObject.Find("NM").GetComponent<NetworkManager>().networkPort);
	}

    public void SetupHost(){
        GameObject.Find("NM").GetComponent<NetworkManager> ().StartHost ();
	}
}
