﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public Camera mainCamera;

    public GameObject target;
    public Quaternion baseRotation;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    private void Update()
    {
        if(!mainCamera.GetComponent<PlayerView>().freeCamera)
        {
            mainCamera.transform.LookAt(target.transform);
        }
        /*else
        {
            mainCamera.transform.rotation = baseRotation;
        }*/
    }

    public void CameraLookAtGameObject(GameObject go)
    {
        target = go;
        mainCamera.GetComponent<PlayerView>().freeCamera = false;
    }

    public void FreeCameraEnable()
    {
        mainCamera.GetComponent<PlayerView>().freeCamera = true;
    }

    public void SaveBaseRotation()
    {
        baseRotation = mainCamera.transform.rotation;
    }

    public void SetOriginPoint()
    {
        mainCamera.transform.rotation = baseRotation;
    }
}
