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

    WaveList defaultWaveData;

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
            GameObject thisShip = GameObject.Instantiate(enemyShipPrefab, _shipData.shipPos, Quaternion.Euler(_shipData.shipRot));
            Debug.Log(gameObject.name + ": Spawned an enemy ship (" + thisShip.name + ") at (" + thisShip.transform.position + ").");
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
        defaultWaveData = new WaveList();
        Wave wave1 = new Wave();
        wave1.ships.Add(new Ship(new Vector3(-20, 20, 0)));
        wave1.ships.Add(new Ship(new Vector3(20, -20, 0)));
        defaultWaveData.waveList.Add(wave1);

        Wave wave2 = new Wave();
        wave2.ships.Add(new Ship(new Vector3(20, 20, 0)));
        wave2.ships.Add(new Ship(new Vector3(-20, -20, 0)));
        wave2.ships.Add(new Ship(new Vector3(-20, 20, 0)));
        wave2.ships.Add(new Ship(new Vector3(20, -20, 0)));
        defaultWaveData.waveList.Add(wave2);

        string jsonString = JsonUtility.ToJson(defaultWaveData);
        try
        {
            File.WriteAllText(Application.dataPath + "/Wave Editor/Waves.json", jsonString.ToString());
            Debug.Log("Saving waves file.");
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Cannot find waves file. Creating a new one.");
            Directory.CreateDirectory(Application.dataPath + "/Wave Editor");
            SaveDefaultWaves();
        }
    }

    public WaveList LoadWaveData()
    {
        WaveList _waveData = new WaveList();

        try
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/Wave Editor/Waves.json");
            _waveData = JsonUtility.FromJson<WaveList>(jsonString);
        }
        catch (System.Exception)
        {
            SaveDefaultWaves();
        }
        Debug.Log("Loading wave data file.");

        return _waveData;
    }
}