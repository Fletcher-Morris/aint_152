using UnityEngine;
using System.Collections;

public class Singleton_Script : MonoBehaviour
{
    string objectTag;
    GameObject[] objectsOfTag;


    public bool destroyOther = true;

    void Start()
    {
        objectTag = gameObject.tag;
        objectsOfTag = GameObject.FindGameObjectsWithTag(objectTag);

        if (objectsOfTag.Length >= 2)
        {
            if (destroyOther == true)
            {
                Destroy(objectsOfTag[1]);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}