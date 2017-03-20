using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup_Script : NetworkBehaviour
{
    public Player _player;

    [SerializeField]
    [SyncVar]
    public int _uniqueID;
    [SerializeField]
    [SyncVar]
    public string _playerName;

    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "Remote Player";

    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }

        if (isLocalPlayer)
        {
            GetPlayerDetails();
        }
    }

    void GetPlayerDetails()
    {
        _player = new Player();
        _uniqueID = _player.uniqueId;
        _playerName = _player.playerName;
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
        Debug.Log("Set player layer to " + LayerMask.LayerToName(gameObject.layer));
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }
}
