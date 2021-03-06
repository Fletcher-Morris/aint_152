﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WaveManager_Script : MonoBehaviour
{
    public int currentRound = 0;
    public int remainingEnemies = 0;

    public int numberOfRounds;

    public float roundCountdown = 0;

    public GameObject enemyShipPrefab;

    public WaveList waveData;

    public bool doSpawn = false;

    public void RoundCountDown()
    {
        roundCountdown = roundCountdown - 1 * Time.deltaTime;
    }

    private void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            if (doSpawn)
            {
                currentRound++;
                NextRound(); 
            }
        }
    }

    void NextRound()
    {
        if(currentRound >= numberOfRounds)
        {
            currentRound = 0;
        }

        if (waveData.waveList.Count > 0)
        {
            SpawnWave(currentRound); 
        }
    }

    public void SpawnWave(int _waveNumber)
    {
        foreach(Ship _shipData in waveData.waveList[_waveNumber].ships)
        {
            if (_shipData.randomPosition)
            {
                _shipData.shipPos = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0);
            }

            GetComponent<WorldLoader_Script>().SpawnNewShip(_shipData);
        }
    }

    private void Start()
    {
        SaveDefaultWaves();

        waveData = LoadWaveData();

        numberOfRounds = waveData.waveList.Count;

        currentRound = numberOfRounds;
    }

    public void SaveDefaultWaves()
    {
        string jsonString = JsonUtility.ToJson(waveData);
        try
        {
            File.WriteAllText(Application.dataPath + "/Data/Waves.json", jsonString.ToString());
			Debug.Log(System.DateTime.Now.ToString() + "   Trying To Save Waves File.");
        }
        catch (System.Exception)
        {
			Debug.LogWarning(System.DateTime.Now.ToString() + "   COULD NOT SAVE WAVES FILE, TRYING AGAIN.");
            Directory.CreateDirectory(Application.dataPath + "/Data");
            SaveDefaultWaves();
        }

		Debug.Log (System.DateTime.Now.ToString() + "   Saved Waves File.");
    }

    public WaveList LoadWaveData()
    {
        WaveList _waveData = new WaveList();

        try
        {
			Debug.Log(System.DateTime.Now.ToString() + "   Trying To Load Waves File.");
            string jsonString = File.ReadAllText(Application.dataPath + "/Data/Waves.json");
            _waveData = JsonUtility.FromJson<WaveList>(jsonString);
        }
        catch (System.Exception)
        {
			Debug.LogWarning(System.DateTime.Now.ToString() + "   COULD NOT LOAD WAVES FILE, MAKING A NEW ONE.");
            SaveDefaultWaves();
        }

		Debug.Log(System.DateTime.Now.ToString() + "   Loaded Waves File.");

        return _waveData;
    }
}