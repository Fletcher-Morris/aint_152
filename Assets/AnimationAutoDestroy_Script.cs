using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAutoDestroy_Script : MonoBehaviour
{
    public float delay = 0f;

    private void Start()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}