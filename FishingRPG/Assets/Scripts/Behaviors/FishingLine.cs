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
        currentTension = 0f;
        UpdateJaugeTension();
    }

    public void TensionDown()
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

    public void TensionDownTakingLine()
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
            Debug.Log("IsExhausted or Dead so Tension dont down");
        }
    }

    public void TensionUp()
    {
        if (currentTension > 0f)
        {
            currentTension -= 0.3f;
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
