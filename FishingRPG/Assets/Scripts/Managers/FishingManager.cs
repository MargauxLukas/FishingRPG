using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class FishingManager : MonoBehaviour
{
    public static FishingManager instance;
    public ActivateSwirls swirlsScript;

    private float timer      = 0f;
    private float needToWait = 0f;

    public bool readyToFish = false;
    public bool isOnWater = false;

    public GameObject snapPrefab;
    public GameObject reefPrefab;
    public Transform dynamics;

    public GameObject currentFish;
    public GameObject finishFishDestination;
    public GameObject midFishDestination;

    //Chance Spawn Fish
    public int snapChance = 0;
    public int reefChance = 0;
    private int totalChanceFish;
    private int randomNumber;

    public bool isInFishingRod = false;
    public bool isOnSwirl = false;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        if (isOnWater && !readyToFish && isOnSwirl)
        {
            if(needToWait == 0f){needToWait = SetTimer();}

            timer += Time.fixedDeltaTime;

            if(timer > needToWait)
            {
                FishingRodManager.instance.fishingLine.CheckWaterLevel();
                readyToFish = true;

                SpawnFish();
                //CatchSomething();
            }
        }
    }

    public float SetTimer()
    {
        FishingRodManager.instance.fishingLine.GetFCurrent();
        return Random.Range(2f, 5f);
    }

    public void SpawnFish()
    {
        //Enlever Swirls
        swirlsScript.DesactivateSwirl();

        totalChanceFish = snapChance + reefChance;

        randomNumber = Random.Range(0, totalChanceFish);

        if(randomNumber <= snapChance)
        {
            //Fish Instantiate
            currentFish = Instantiate(snapPrefab,
                        new Vector3(FishingRodManager.instance.bobber.transform.position.x - 2f,
                                    FishingRodManager.instance.bobber.transform.position.y - 6f,
                                    FishingRodManager.instance.bobber.transform.position.z),
                        Quaternion.identity,
                        dynamics);
        }
        else
        {
            //Fish Instantiate
            currentFish = Instantiate(reefPrefab,
                        new Vector3(FishingRodManager.instance.bobber.transform.position.x - 2f,
                                    FishingRodManager.instance.bobber.transform.position.y - 6f,
                                    FishingRodManager.instance.bobber.transform.position.z),
                        Quaternion.identity,
                        dynamics);
        }

        FishManager.instance.currentFish = currentFish;
        FishManager.instance.currentFishBehavior = currentFish.GetComponent<FishBehavior>();
        FishManager.instance.currentFishBehavior.spawnPoint = new Vector3(FishingRodManager.instance.bobber.transform.position.x - 2f,
                                                                          FishingRodManager.instance.bobber.transform.position.y - 6f,
                                                                          FishingRodManager.instance.bobber.transform.position.z);
        FishManager.instance.hasJustSpawned = true;
    }

    public void CatchSomething()
    {
        GamePad.SetVibration(0, 0.5f, 0.5f);
        StartCoroutine("TimerVibration");
        FishManager.instance.SetAerialEnterWater();
        CameraManager.instance.CameraLookAtGameObject(currentFish);
        FishManager.instance.isAerial = false;
        FishingRodManager.instance.fishDistanceCP.gameObject.SetActive(true);
        PlayerManager.instance.cfvz.fishCheck = currentFish.transform;
        FishManager.instance.lifeJauge.transform.parent.gameObject.SetActive(true);
        FishManager.instance.staminaJauge.transform.parent.gameObject.SetActive(true);
        PlayerManager.instance.FishingCanStart();
        //FishingRodManager.instance.fishingLine.cableComponent.InitCableParticles();
    }

    IEnumerator TimerVibration()
    {
        yield return new WaitForSeconds(0.2f);
        GamePad.SetVibration(0, 0f, 0f);
    }

    public void CancelFishing()
    {
        //Swirl Activate
        swirlsScript.DesactivateSwirl();
        swirlsScript.ActivateSwirl();

        //DesactivateLine() 
        needToWait = 0f;
        timer      = 0f;
        FishingRodManager.instance.animFishingRod.SetFloat("SpeedMultiplier", 0);

        if (readyToFish)
        {
            FishManager.instance.lifeJauge.transform.parent.gameObject.SetActive(false);
            FishManager.instance.staminaJauge.transform.parent.gameObject.SetActive(false);
            FishManager.instance.currentFishBehavior.fishPattern.ResetOncePlay();
            if (FishManager.instance.currentFishBehavior.canCollectTheFish)
            {
                PlayerManager.instance.playerInventory.AddThisFishToInventory(FishManager.instance.currentFishBehavior.fishyFiche.ID);
                FishManager.instance.currentFishBehavior.canCollectTheFish = false;
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
