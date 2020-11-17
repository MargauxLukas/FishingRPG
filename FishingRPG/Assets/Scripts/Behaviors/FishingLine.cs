using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingLine : MonoBehaviour
{
    public float fCurrent;
    public float fMax;
    public float fCritique;

    public float currentTension = 100;
    public float maxTension = 100;
    public Text textInt;

    public bool isBlocked = false;
    public bool isTaken = false;
    public Text textBlocked;
    public Text textTaken  ;

    private float timer = 0f;
    private float maxTime = 1f;
    private bool isTensionJustDown = false;

    private void Update()
    {
        /*if(isTensionJustDown)
        {
            timer += Time.fixedDeltaTime;

            if(timer >= maxTime)
            {
                isTensionJustDown = false;
                timer = 0f;
            }
        }*/
    }

    public void LineIsBroken()
    {
        FishingManager.instance.CancelFishing();
        currentTension = maxTension;
    }

    public void TensionDown()
    {
        if (!FishManager.instance.currentFishBehavior.isDead && !FishManager.instance.currentFishBehavior.exhausted)
        {
            currentTension -= UtilitiesManager.instance.GetLossTensionNumber() / 60;
            ChangeText();

            if (currentTension <= 0)
            {
                currentTension = 0;
                ChangeText();
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
            currentTension -= UtilitiesManager.instance.GetLossTensionNumberTakingLine() / 60;
            ChangeText();

            if (currentTension <= 0)
            {
                currentTension = 0;
                ChangeText();
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
        if (currentTension <= maxTension)
        {
            currentTension += 0.3f;
        }
        else
        {
            currentTension = 100f;
        }

        ChangeText();
    }

    public void FCurrentDown()
    {
        fCurrent -= 0.05f;
    }

    public void GetFCurrent()
    {
        fCurrent = Vector3.Distance(FishingRodManager.instance.pointC.position, FishingRodManager.instance.bobber.transform.position) * 1.1f;
        FishingRodManager.instance.ChangeTextFCurrent();
    }

    public void isFCurrentAtMax()
    {
        isBlocked = true;
        fCurrent = fMax;
        textBlocked.color = Color.green;
    }

    public void ChangeText()
    {
        textInt.text = currentTension.ToString();
    }
}
