using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanobotItem_Script : MonoBehaviour {

    public float rotationSpeed = 100;

    public GameObject rotor1;
    public GameObject rotor2;
    public GameObject rotor3;
    public GameObject rotor4;

    float random1;
    float random2;
    float random3;
    float random4;

    private void Start()
    {
        random1 = Random.Range(-50, 50);
        random2 = Random.Range(-50, 50);
        random3 = Random.Range(-50, 50);
        random4 = Random.Range(-50, 50);
    }

    private void Update()
    {
        rotor1.transform.eulerAngles += new Vector3(0, (rotationSpeed + random1) * Time.deltaTime, 0);
        rotor2.transform.eulerAngles += new Vector3(0, (rotationSpeed + random2) * Time.deltaTime, 0);
        rotor3.transform.eulerAngles += new Vector3(0, 0, (rotationSpeed + random3) * Time.deltaTime);
        rotor4.transform.eulerAngles += new Vector3(0, 0, (rotationSpeed + random4) * Time.deltaTime);
    }
}
