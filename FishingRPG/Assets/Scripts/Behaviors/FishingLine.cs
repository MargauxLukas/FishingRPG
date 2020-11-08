using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingLine : MonoBehaviour
{
    public float fCurrent;
    public float fMax;

    public float currentTension = 100;
    public float maxTension = 100;
    public Text textInt;

    public void LineIsBroken()
    {
        FishingManager.instance.CancelFishing();
        currentTension = 100;
    }

    public void TensionDown()
    {
        currentTension -= 0.5f;
        ChangeText();

        if(currentTension <= 0)
        {
            currentTension = 0;
            ChangeText();
            LineIsBroken();
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

    public void ChangeText()
    {
        textInt.text = currentTension.ToString();
    }
}
