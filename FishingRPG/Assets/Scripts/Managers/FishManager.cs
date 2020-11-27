﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishManager : MonoBehaviour
{
    public static FishManager instance;

    [Header("Fish")]
    [HideInInspector] public GameObject currentFish;
    [HideInInspector] public FishBehavior currentFishBehavior;
    public Transform savePos;

    [Header("Material Aerial")]
    public Material canAerialMat;
    public Material normalMat;

    [Header("Text")]
    public Text enduText;
    public Text speedText;
    public Text lifeText;

    [Header("Aerial variables")]
    [HideInInspector] public bool isAerial = false;
    [HideInInspector] public bool isFelling = false;
    [HideInInspector] public float aerialExitWaterX  = 0f;
    [HideInInspector] public float aerialX;
    [HideInInspector] public float aerialEnterWaterX = 0f;
    [HideInInspector] public float  aerialExitWaterY  = 0f;
    [HideInInspector] public float  aerialY;
    [HideInInspector] public float  aerialEnterWaterY = 0f;
    [HideInInspector] public float aerialExitWaterZ  = 0f;
    [HideInInspector] public float aerialZ;
    [HideInInspector] public float aerialEnterWaterZ = 0f;

    [Header("Rotation Percent")]
    public int sPercent;
    public int sePercent;
    public int esPercent;
    public int ePercent;
    public int enPercent;
    public int nePercent;
    public int nPercent;
    public int noPercent;
    public int onPercent;
    public int oPercent;
    public int osPercent;
    public int soPercent;

    [HideInInspector]public List<int> directionPercentList = new List<int>(11);
    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    public void Start()
    {
        directionPercentList = new List<int>(new int[] { sPercent, sePercent, esPercent, ePercent, enPercent, nePercent, nPercent, noPercent, onPercent, oPercent, osPercent, soPercent });
    }

    public void IsExtenued()
    {
        if(currentFishBehavior.exhausted)
        {
            isAerial = true;
            currentFishBehavior.maxTimeAerial = UtilitiesManager.instance.GetTimeAerial(currentFishBehavior.JumpHeight, currentFishBehavior.nbRebond);
            aerialExitWaterX = currentFish.transform.position.x;
            aerialExitWaterY = currentFish.transform.position.y;
            aerialExitWaterZ = currentFish.transform.position.z;

            aerialEnterWaterX = currentFish.transform.position.x;
            aerialEnterWaterY = currentFish.transform.position.y;
            aerialEnterWaterZ = currentFish.transform.position.z;

            aerialX = currentFish.transform.position.x;
            aerialY = currentFish.transform.position.y + UtilitiesManager.instance.GetHeightMaxForAerial(currentFishBehavior.JumpHeight);
            aerialZ = currentFish.transform.position.z;
        }
    }

    public void MoreAerial()
    {
        Debug.Log("Boing Again");
        currentFishBehavior.nbRebond++;
        currentFishBehavior.maxTimeAerial = UtilitiesManager.instance.GetTimeAerial(currentFishBehavior.JumpHeight, currentFishBehavior.nbRebond);
        aerialExitWaterX = currentFish.transform.position.x;
        aerialExitWaterY = currentFish.transform.position.y;
        aerialExitWaterZ = currentFish.transform.position.z;

        aerialX = currentFish.transform.position.x;
        aerialY = currentFish.transform.position.y + UtilitiesManager.instance.GetHeightMaxForAerial(currentFishBehavior.JumpHeight);
        aerialZ = currentFish.transform.position.z;

        isFelling = false;
        currentFishBehavior.isFellDown = false;
        currentFishBehavior.timerAerial = 0f;
    }

    public void FellAerial()
    {
        isFelling = true;
        currentFishBehavior.fellingFreeze = true;
        
        StartCoroutine(FellingFreeze());
    }

    IEnumerator FellingFreeze()
    {
        yield return new WaitForSeconds(0.2f);

        Debug.Log("Abattage");

        currentFishBehavior.maxTimeAerial = UtilitiesManager.instance.GetTimeFellingAerial(currentFishBehavior.maxTimeAerial, currentFish.transform.position.y - aerialExitWaterY, aerialY);

        aerialExitWaterX = currentFish.transform.position.x;
        aerialExitWaterY = currentFish.transform.position.y;
        aerialExitWaterZ = currentFish.transform.position.z;

        currentFishBehavior.isFellDown = true;
        currentFishBehavior.fellingFreeze = false;
        currentFishBehavior.timerAerial = 0f;

        yield return new WaitForSeconds(currentFishBehavior.maxTimeAerial);
        AerialDamage();
    }

    public void ExtenuedChange()
    {
        currentFish.GetComponent<MeshRenderer>().material = canAerialMat;
    }

    public void NotExtenued()
    {
        currentFish.GetComponent<MeshRenderer>().material = normalMat;
    }

    public void FishRecuperation()
    {
        currentFishBehavior.exhausted = false;
        currentFishBehavior.currentStamina = 50f;
        currentFishBehavior.nbRebond = 1;
        isAerial = false;
        isFelling = false;
        currentFishBehavior.isFellDown = false;
        NotExtenued();
        ChangeEnduranceText();
    }

    public void DownEndurance()
    {
        if (currentFishBehavior.currentStamina > 0)
        {
            currentFishBehavior.currentStamina -= UtilitiesManager.instance.GetLossEnduranceNumber()/60;
            ChangeEnduranceText();
        }

        currentFishBehavior.CheckEndurance();
    }

    public void DownEnduranceTakingLine()
    {
        if (currentFishBehavior.currentStamina > 0)
        {
            currentFishBehavior.currentStamina -= UtilitiesManager.instance.GetLossEnduranceNumberTakingLine()/60;
            ChangeEnduranceText();
        }

        currentFishBehavior.CheckEndurance();
    }

    public void UpEndurance()
    {
        if (currentFishBehavior.currentStamina < currentFishBehavior.fishyFiche.stamina)
        {
            currentFishBehavior.currentStamina += 0.25f;
            ChangeEnduranceText();
        }

        if (currentFishBehavior.currentStamina > 50f)
        {
            currentFishBehavior.currentStamina = 50f;
            DebugManager.instance.vz.DesactivateZone();
            currentFishBehavior.exhausted = false;
            NotExtenued();
        }
    }

    public void AerialDamage()
    {
        if (currentFishBehavior.currentLife > 0f)
        {
            currentFishBehavior.currentLife -= UtilitiesManager.instance.GetFellingDamage();
            ChangeLifeText();
        }

        currentFishBehavior.CheckLife();
    }

    public void SetFinishPoint()
    {
        aerialExitWaterX = currentFish.transform.position.x;
        aerialExitWaterY = currentFish.transform.position.y;
        aerialExitWaterZ = currentFish.transform.position.z;

        aerialEnterWaterX = FishingManager.instance.finishFishDestination.transform.position.x;
        aerialEnterWaterY = FishingManager.instance.finishFishDestination.transform.position.y;
        aerialEnterWaterZ = FishingManager.instance.finishFishDestination.transform.position.z;

        aerialX = FishingManager.instance.midFishDestination.transform.position.x;
        aerialY = FishingManager.instance.midFishDestination.transform.position.y;
        aerialZ = FishingManager.instance.midFishDestination.transform.position.z;

        currentFishBehavior.inVictoryZone = true;
        isAerial = true;
    }

    public void ChangeEnduranceText()
    {
        enduText.text = currentFishBehavior.currentStamina.ToString();
    }

    public void ChangeSpeedText(float speed)
    {
        speedText.text = speed.ToString();
    }

    public void ChangeLifeText()
    {
        lifeText.text = currentFishBehavior.currentLife.ToString();
    }
}
