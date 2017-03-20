using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad_Script : MonoBehaviour {

	void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
