using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveList
{
    public List<Wave> waveList;

    public WaveList()
    {
        waveList = new List<Wave>();
    }
}