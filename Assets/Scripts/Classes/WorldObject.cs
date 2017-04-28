using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldObject
{
    public string objectName;
    public string objectType;
    public Vector3 objectPos;
    public Vector3 objectRot;

    public WorldObject()
    {
        objectName = "New World Object";
        objectType = "Station";
        objectPos = Vector3.zero;
        objectRot = Vector3.zero;
    }
}