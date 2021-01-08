using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingLine : MonoBehaviour
{
    [Header("Tension Values")]
    public float fMax;
    public float fCritique;
    [HideInInspector] public float fCurrent;
    [HideInInspector] public float currentTension = 0;
    public float maxTension = 100;

    [Header("Etat Ligne")]
    [HideInInspector] public bool isBlocked = false;
    [HideInInspector] public bool isTaken = false;
    public CableComponent cableComponent;

    [Header("Texts/Jauge")]
    public Image tensionJauge;
    public Text textBlocked;
    public Text textTaken  ;

    public void LineIsBroken()
    {
        FishingManager.instance.CancelFishing();
        CameraManager.instance.ScreenShake(0.35f);
        currentTension = 0f;
        UpdateJaugeTension();
    }

    //Augmentation tension Blocked
    public void TensionUp()
    {
        if (!FishManager.instance.currentFishBehavior.isDead && !FishManager.instance.currentFishBehavior.exhausted)
        {
            currentTension += UtilitiesManager.instance.GetLossTensionNumber() / 60;
            UpdateJaugeTension();

            if (currentTension >= maxTension)
            {
                currentTension = maxTension;
                UpdateJaugeTension();
                LineIsBroken();
            }
        }
        else
        {
            Debug.Log("IsExhausted or Dead so Tension dont down");
        }
    }

    //Augmentation tension Take Line
    public void TensionUpTakingLine()
    {
        if (!FishManager.instance.currentFishBehavior.isDead && !FishManager.instance.currentFishBehavior.exhausted)
        {
            currentTension += UtilitiesManager.instance.GetLossTensionNumberTakingLine() / 60;
            UpdateJaugeTension();

            if (currentTension >= maxTension )
            {
                currentTension = maxTension;
                UpdateJaugeTension();
                LineIsBroken();
            }
        }
        else
        {
            Debug.Log("IsExhausted or Dead so Tension dont up");
        }
    }

    public void TensionDown()
    {
        if (currentTension > 0f)
        {
            currentTension -= 0.6f;
        }
        else
        {
            currentTension = 0f;
        }

        UpdateJaugeTension();
    }

    public void FCurrentDown()
    {
        fCurrent -= 0.05f;

        if(FishManager.instance.isAerial)
        {
            FishManager.instance.aerialEnterWaterZ -= 0.05f;
            FishManager.instance.UpdateAerial();
        }

    }

    public void GetFCurrent()
    {
        fCurrent = Vector3.Distance(FishingRodManager.instance.pointC.position, FishingRodManager.instance.bobber.transform.position) * 1.1f;
        FishingRodManager.instance.UpdateFCurrent();
    }

    public void isFCurrentAtMax()
    {
        isBlocked = true;
        fCurrent = fMax;
        textBlocked.color = Color.green;
    }

    public void UpdateJaugeTension()
    {
        tensionJauge.fillAmount = currentTension/maxTension;
    }
}
