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
        //Poisson s'enfuit
        //Bobber back
    }

    public void TensionDown()
    {
        currentTension -= 0.5f;
        ChangeText();
    }

    public void ChangeText()
    {
        textInt.text = currentTension.ToString();
    }
}
