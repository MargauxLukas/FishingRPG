using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager instance;

    public float timer = 0f;
    public float needToWait = 0f;

    public bool readyToFish = false;

    public GameObject fishPrefab;
    public Transform dynamics;

    public GameObject currentFish;
    public bool isOnWater;
    public bool fishIsGoingRight;
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
            if(needToWait == 0f)
            {
                needToWait = SetTimer();
            }
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
        return Random.Range(2f, 5f);
    }

    public void CatchSomething()
    {
        FishingRodManager.instance.SetBobberMaterialToSucces();
        currentFish = Instantiate(fishPrefab, 
                    new Vector3(FishingRodManager.instance.bobber.transform.position.x, 
                                FishingRodManager.instance.bobber.transform.position.y - 1f,
                                FishingRodManager.instance.bobber.transform.position.z),
                    Quaternion.identity,
                    dynamics          );
        currentFish.GetComponent<FixedJoint>().connectedBody = FishingRodManager.instance.bobber.GetComponent<Rigidbody>();
        FishManager.instance.currentFish = currentFish;
        FishManager.instance.currentFishBehavior = currentFish.GetComponent<FishBehavior>();
        FishManager.instance.ChangeText();
        PlayerManager.instance.FishingCanStart();
    }

    public void CancelFishing()
    {
        needToWait = 0f;
        timer = 0f;
        FishingRodManager.instance.SetBobberMaterialToFail();
        if (readyToFish)
        {
            readyToFish = false;
            Destroy(currentFish);
        }

        FishingRodManager.instance.BobberBack();
    }
}
