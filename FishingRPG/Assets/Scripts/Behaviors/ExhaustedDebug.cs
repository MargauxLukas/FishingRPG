using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExhaustedDebug : MonoBehaviour
{
    public Image image;
    public Color redColor;
    public Color greenColor;

    public void SetToGreen()
    {
        image.color = greenColor;
    }

    public void SetToRed()
    {
        image.color = redColor;
    }
}
