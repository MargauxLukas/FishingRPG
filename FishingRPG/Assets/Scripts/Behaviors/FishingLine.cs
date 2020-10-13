using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingLine : MonoBehaviour
{
    public float currentTension = 100;
    public float maxTension = 100;
    public Text textInt;

    public void LineIsBroken()
    {
        FishingManager.instance.CancelFishing();
    }

    public void TensionDown()
    {
        Debug.Log("heyTension");
        currentTension -= 0.5f;
        ChangeText();

        if(currentTension <= 0)
        {
            LineIsBroken();
        }
    }

    public void ChangeText()
    {
        textInt.text = currentTension.ToString();
    }
}
