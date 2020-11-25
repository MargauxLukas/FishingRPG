using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextColor : MonoBehaviour
{
    public void SelectedColor()
    {
        gameObject.GetComponent<Text>().color = new Color32(66, 41, 36, 255);
    }
    
    public void DeselectedColor()
    {
        gameObject.GetComponent<Text>().color = new Color32(254, 242, 184, 255);
    }
}
