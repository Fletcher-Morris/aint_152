using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipSetup_Script : NetworkBehaviour
{
    [SerializeField]
    [SyncVar]
    public Ship shipDetails;

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
    }

    void AssignRemoteLayer()
    {
        gameObject.transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer(remoteInsideLayerName);   //  Inside_Sprite
        gameObject.transform.GetChild(2).gameObject.layer = LayerMask.NameToLayer(remoteLayerName);         //  Turret_Sprite
        gameObject.transform.GetChild(3).gameObject.layer = LayerMask.NameToLayer(remoteInsideLayerName);   //  Cockpit_Trigger
        gameObject.transform.GetChild(4).gameObject.layer = LayerMask.NameToLayer(remoteInsideLayerName);   //  Turret_Trigger
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }    
}
