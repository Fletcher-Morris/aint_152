using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GamePrefs_Script : MonoBehaviour {

    public GamePrefs gamePrefs = new GamePrefs();

    void Start()
    {
        gamePrefs.SavePrefs();
    }
}
