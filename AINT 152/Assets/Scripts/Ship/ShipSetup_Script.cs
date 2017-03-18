using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipSetup_Script : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "Remote Spaceship";

    [SerializeField]
    string remoteInsideLayerName = "Remote Spaceship Inside";

    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }

        RegisterShip();
    }

    void RegisterShip()
    {
        string _ID = "Spaceship " + GetComponent<NetworkIdentity>().netId;
        transform.name = _ID;
    }

    void AssignRemoteLayer()
    {
        gameObject.transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer(remoteInsideLayerName);
        gameObject.transform.GetChild(2).gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    [SyncVar]
    public string m_playerName;
    [SyncVar]
    public int m_playerNumber;
    [SyncVar]
    public int m_localID;
}
