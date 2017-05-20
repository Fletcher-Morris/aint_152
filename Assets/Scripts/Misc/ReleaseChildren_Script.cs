using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseChildren_Script : MonoBehaviour {

    public bool releaseOnStart = true;

    private void Start()
    {
        if (releaseOnStart)
        {
            ReleaseChildren();
        }
    }

    public void ReleaseChildren()
    {
        gameObject.transform.DetachChildren();
    }
}
