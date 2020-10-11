using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public Camera mainCamera;

    public GameObject target;

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
        if(!mainCamera.GetComponent<PlayerView>().freeCamera && !FishingRodManager.instance.bobber.GetComponent<CheckWater>().isWater)
        {
            mainCamera.transform.LookAt(target.transform);
        }
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
}
