using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy_Script : MonoBehaviour
{
    public float destroyTime = 5f;

    float timer;

    private void Start()
    {
        timer = destroyTime;
    }

    private void Update()
    {
        if(timer <= 0)
        {
            GameObject.Destroy(gameObject);
        }

        timer = timer - 1 * Time.deltaTime;
    }
}