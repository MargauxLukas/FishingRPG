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
        if(isTensionJustDown)
        {
            timer += Time.fixedDeltaTime;

            if(timer >= maxTime)
            {
                isTensionJustDown = false;
                timer = 0f;
            }
        }
    }

    public void LineIsBroken()
    {
        FishingManager.instance.CancelFishing();
        currentTension = maxTension;
    }

    public void TensionDown()
    {
        if (!isTensionJustDown)
        {
            currentTension -= UtilitiesManager.instance.GetLossTensionNumber();
            ChangeText();

            if (currentTension <= 0)
            {
                currentTension = 0;
                ChangeText();
                LineIsBroken();
            }

            isTensionJustDown = true;
        }
    }

    public void TensionDownTakingLine()
    {
        if (!isTensionJustDown)
        {
            currentTension -= UtilitiesManager.instance.GetLossTensionNumberTakingLine();
            ChangeText();

            if (currentTension <= 0)
            {
                currentTension = 0;
                ChangeText();
                LineIsBroken();
            }

            isTensionJustDown = true;
        }
    }

    public void TensionUp()
    {
        if (currentTension <= maxTension)
        {
            currentTension += 0.2f;
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
