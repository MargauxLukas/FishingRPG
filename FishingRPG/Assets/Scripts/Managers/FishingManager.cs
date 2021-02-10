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
    public GameObject snapTuto;
    public GameObject reefPrefab;
    public Transform dynamics;

    public GameObject staminaAndLifeJauge;

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

    public bool isSnap = false;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    private void Start()
    {
        //isInFishingRod = false;
    }

    private void FixedUpdate()
    {
        if (isOnWater && !readyToFish && isOnSwirl)
        {
            if(needToWait == 0f)
            {
                //CameraManager.instance.CameraLookAtGameObject(FishingRodManager.instance.bobber);
                needToWait = SetTimer();
            }

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

        if (!PlayerManager.instance.playerInventory.inventory.tutoFini)
        {
            currentFish = Instantiate(snapTuto,
                           new Vector3(FishingRodManager.instance.bobber.transform.position.x - 2f,
                                       FishingRodManager.instance.bobber.transform.position.y - 6f,
                                       FishingRodManager.instance.bobber.transform.position.z),
                           Quaternion.identity,
                           dynamics);

            isSnap = true;
            //Set Current Fish to SnapSnack
            AkSoundEngine.SetSwitch("CurrentFishInCombat", "SnapSnack", FishManager.instance.currentFish.gameObject);
            //Play Sound
            AkSoundEngine.PostEvent("MSCCombatMusic", FishManager.instance.currentFish.gameObject);
        }
        else
        {
            if (randomNumber <= snapChance)
            {
                //Fish Instantiate
                currentFish = Instantiate(snapPrefab,
                            new Vector3(FishingRodManager.instance.bobber.transform.position.x - 2f,
                                        FishingRodManager.instance.bobber.transform.position.y - 6f,
                                        FishingRodManager.instance.bobber.transform.position.z),
                            Quaternion.identity,
                            dynamics);
                isSnap = true;
                //Set Current Fish to SnapSnack
                AkSoundEngine.SetSwitch("CurrentFishInCombat", "SnapSnack", FishManager.instance.currentFish.gameObject);
                //Play Sound
                AkSoundEngine.PostEvent("MSCCombatMusic", FishManager.instance.currentFish.gameObject);
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
                isSnap = false;
                //Set Current Fish to ReefCrusher
                AkSoundEngine.SetSwitch("CurrentFishInCombat", "ReefCrusher", FishManager.instance.currentFish.gameObject);
                //Play Sound
                AkSoundEngine.PostEvent("MSCCombatMusic", FishManager.instance.currentFish.gameObject);
            }
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
        if (!isSnap)
        {
            FishManager.instance.currentFish.transform.position = new Vector3(FishingRodManager.instance.bobber.transform.position.x,
                                                                          FishingRodManager.instance.bobber.transform.position.y - 2f,
                                                                          FishingRodManager.instance.bobber.transform.position.z);
        }

        GamePad.SetVibration(0, 0.5f, 0.5f);
        StartCoroutine("TimerVibration");
        FishManager.instance.SetAerialEnterWater();
        CameraManager.instance.CameraLookAtGameObject(currentFish);
        FishManager.instance.isAerial = false;
        FishingRodManager.instance.fishDistanceCP.gameObject.SetActive(true);
        PlayerManager.instance.cfvz.fishCheck = currentFish.transform;
        staminaAndLifeJauge.SetActive(true);
        PlayerManager.instance.FishingCanStart();

        //FishingRodManager.instance.fishingLine.cableComponent.InitCableParticles();
    }

    IEnumerator TimerVibration()
    {
        yield return new WaitForSeconds(0.2f);
        GamePad.SetVibration(0, 0f, 0f);

        if (!PlayerManager.instance.playerInventory.inventory.tutoFini)
        {
            TutoManager.instance.maskUI.SetActive(true);
            TutoManager.instance.Chap3Dialogue1();
        }
    }

    public void CancelFishing()
    {
        //MoulinetOnce = true;
        

        //Swirl Activate
        swirlsScript.DesactivateSwirl();
        swirlsScript.ActivateSwirl();

        //DesactivateLine() 
        needToWait = 0f;
        timer      = 0f;
        FishingRodManager.instance.animFishingRod.SetFloat("SpeedMultiplier", 0);

        if (readyToFish)
        {
            staminaAndLifeJauge.SetActive(false);
            FishManager.instance.currentFishBehavior.fishPattern.ResetOncePlay();

            if (FishManager.instance.currentFishBehavior.canCollectTheFish)
            {
                AkSoundEngine.PostEvent("OnFishPickUp", gameObject);
                PlayerManager.instance.playerInventory.AddThisFishToInventory(FishManager.instance.currentFishBehavior.fishyFiche.ID);
                FishManager.instance.currentFishBehavior.canCollectTheFish = false;
            }

            Destroy(currentFish);
            //currentFish.gameObject.SetActive(false);
        }

        readyToFish = false;

        FishingRodManager.instance.fishingLine.isTaken = false;
        FishingRodManager.instance.BobberBack();
        FishingRodManager.instance.fishingLine.fCurrent = 0f;
        FishingRodManager.instance.distanceCP = 0f;

        PlayerManager.instance.MoulinetOnce = false; 
        //Stop sound -> Combat music
        AkSoundEngine.PostEvent("STOP_MSCCombatMusic", gameObject);
        //Stop -> Moulinet Sound
        AkSoundEngine.PostEvent("STOP_MoulinetOn", PlayerManager.instance.gameObject);
        //Stop -> Tension Sound
        AkSoundEngine.PostEvent("STOP_FilTendu", FishingRodManager.instance.fishingLine.gameObject);
    }
}
