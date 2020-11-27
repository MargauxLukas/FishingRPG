﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager instance;

    private float timer      = 0f;
    private float needToWait = 0f;

    private bool readyToFish = false;

    public GameObject fishPrefab;
    public Transform dynamics;

    public GameObject currentFish;
    public GameObject finishFishDestination;
    public GameObject midFishDestination;

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
        if (FishingRodManager.instance.bobber.GetComponent<CheckWater>().isWater && !readyToFish)
        {
            if(needToWait == 0f){needToWait = SetTimer();}

            timer += Time.deltaTime;

            if(timer > needToWait)
            {
                readyToFish = true;
                CatchSomething();
            }
        }
    }

    public float SetTimer()
    {
        FishingRodManager.instance.fishingLine.GetFCurrent();
        return Random.Range(2f, 5f);
    }

    public void CatchSomething()
    {
        FishingRodManager.instance.SetBobberMaterialToSucces();
        currentFish = Instantiate(fishPrefab, 
                    new Vector3(FishingRodManager.instance.bobber.transform.position.x, 
                                FishingRodManager.instance.bobber.transform.position.y - 0.6f,
                                FishingRodManager.instance.bobber.transform.position.z),
                    Quaternion.identity,
                    dynamics          );
        FishManager.instance.currentFish         = currentFish;
        FishManager.instance.currentFishBehavior = currentFish.GetComponent<FishBehavior>();
        CameraManager.instance.CameraLookAtGameObject(currentFish);
        PlayerManager.instance.FishingCanStart();
    }

    public void CancelFishing()
    {
        needToWait = 0f;
        timer      = 0f;
        FishingRodManager.instance.SetBobberMaterialToFail();

        if (readyToFish)
        {
            readyToFish = false;
            Destroy(currentFish);
        }

        FishingRodManager.instance.BobberBack();
    }
}
