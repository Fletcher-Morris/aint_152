using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    public float paralaxDepth = 1;

    public GameObject focusObject;

    private void Update()
    {
        focusObject = Camera.main.gameObject.GetComponent<ParalaxEffectController_Script>().paralaxFocus;
        if(focusObject)
            gameObject.transform.position = focusObject.transform.position * paralaxDepth;
    }
}