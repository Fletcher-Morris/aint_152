using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation_Script : MonoBehaviour {

    public bool onStartup = true;
    public bool onUpdate = false;

    private void Start()
    {
        if (onStartup)
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360)); 
        }
    }

    private void Update()
    {
        if (onUpdate)
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));

        }
    }
}
