using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffectController_Script : MonoBehaviour {

    public GameObject paralaxFocus;

    public void SetFocusObject(GameObject _object)
    {
        this.paralaxFocus = _object;
    }
}
