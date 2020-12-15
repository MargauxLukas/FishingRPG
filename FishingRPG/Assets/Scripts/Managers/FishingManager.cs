using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager instance;

    private float timer      = 0f;
    private float needToWait = 0f;

    public bool readyToFish = false;
    public bool isOnWater = false;

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
        if (isOnWater && !readyToFish)
        {
            if(needToWait == 0f){needToWait = SetTimer();}

            timer += Time.fixedDeltaTime;

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
        FishManager.instance.isAerial = false;
        FishingRodManager.instance.fishDistanceCP.gameObject.SetActive(true);
        CameraManager.instance.CameraLookAtGameObject(currentFish);
        PlayerManager.instance.cfvz.fishCheck = currentFish.transform;
        PlayerManager.instance.FishingCanStart();
    }

    public void CancelFishing()
    {
        needToWait = 0f;
        timer      = 0f;
        FishingRodManager.instance.SetBobberMaterialToFail();

        if (readyToFish)
        {
            if(FishManager.instance.currentFishBehavior.canCollectTheFish)
            {
                PlayerManager.instance.playerInventory.AddThisFishToInventory(FishManager.instance.currentFishBehavior.fishyFiche.ID);
            }

            Destroy(currentFish);
            //currentFish.gameObject.SetActive(false);
        }

        readyToFish = false;
        FishingRodManager.instance.BobberBack();
        FishingRodManager.instance.fishingLine.fCurrent = 0f;
        FishingRodManager.instance.distanceCP = 0f;
    }
}
