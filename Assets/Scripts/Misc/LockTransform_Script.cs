using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTransform_Script : MonoBehaviour
{
    public bool lockXPos = false;
    public bool lockYPos = false;
    public bool lockZPos = false;
    public Vector3 position;
    public bool lockXRot = false;
    public bool lockYRot = false;
    public bool lockZRot = false;
    public bool lockWRot = false;
    public Quaternion rotation;

    public bool lockPosToTarget = false;
    public bool lockRotToTarget = false;
    public GameObject targetObject;

    float xPos;
    float yPos;
    float zPos;
    Vector3 newPos;
    float xRot;
    float yRot;
    float zRot;
    float wRot;
    Quaternion newRot;

    private void FixedUpdate()
    {
        if (lockXPos)
        {
            if (lockPosToTarget && targetObject)
            {
                xPos = targetObject.transform.position.x;
            }
            else
            {
                xPos = position.x;
            }
        }
        else
        {
            xPos = transform.position.x;
        }

        if (lockYPos)
        {
            if (lockPosToTarget && targetObject)
            {
                yPos = targetObject.transform.position.y;
            }
            else
            {
                yPos = position.y;
            }
        }
        else
        {
            yPos = transform.position.y;
        }

        if (lockZPos)
        {
            if (lockPosToTarget && targetObject)
            {
                zPos = targetObject.transform.position.z;
            }
            else
            {
                zPos = position.z;
            }
        }
        else
        {
            zPos = transform.position.z;
        }

        if (lockXRot)
        {
            if (lockRotToTarget && targetObject)
            {
                xRot = targetObject.transform.rotation.x;
            }
            else
            {
                xRot = rotation.x;
            }
        }
        if (lockYRot)
        {
            if (lockRotToTarget && targetObject)
            {
                yRot = targetObject.transform.rotation.y;
            }
            else
            {
                yRot = rotation.y;
            }
        }
        if (lockZRot)
        {
            if (lockRotToTarget && targetObject)
            {
                zRot = targetObject.transform.rotation.z;
            }
            else
            {
                zRot = rotation.z;
            }
        }
        if (lockWRot)
        {
            if (lockRotToTarget && targetObject)
            {
                wRot = targetObject.transform.rotation.w;
            }
            else
            {
                wRot = rotation.w;
            }
        }

        newPos = new Vector3(xPos, yPos, zPos);
        newRot = new Quaternion(xRot, yRot, zRot, wRot);

        transform.position = newPos;
        transform.rotation = newRot;
    }
}