using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    private const string PLAYER_ID_PREFIX = "Player ";

    private static Dictionary<string, PlayerSetup_Script> playerDictionary = new Dictionary<string, PlayerSetup_Script>();

    public static void RegisterPlayer(int _uniqueId, PlayerSetup_Script _player)
    {
        string _playerId = PLAYER_ID_PREFIX + _uniqueId.ToString();
        playerDictionary.Add(_playerId, _player);
        _player.transform.name = _playerId;
    }

    public static void UnRegisterPlayer(string _playerID)
    {
        playerDictionary.Remove(_playerID);
    }

    public static PlayerSetup_Script GetPlayer(string _playerID)
    {
        return playerDictionary[_playerID];
    }
}