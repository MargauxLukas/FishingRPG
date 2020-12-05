using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFishPile : MonoBehaviour
{
    void Update()
    {
        if(UIManager.instance.inventory.fishTotal > 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
