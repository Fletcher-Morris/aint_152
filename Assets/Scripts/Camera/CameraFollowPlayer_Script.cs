using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraFollowPlayer_Script : MonoBehaviour
{
    public GameObject cameraObject;

    void Start()
    {
        cameraObject = Camera.main.gameObject;
    }

    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        cameraObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
    }
}
