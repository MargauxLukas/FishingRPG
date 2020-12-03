using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButcherManager : MonoBehaviour
{
    bool fishReadyToCut = false;
    bool cuttedFish = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("A Button") && !fishReadyToCut && !cuttedFish)
        {
            Debug.Log("NewFish");
            //Move fish on cutting table
        }
        else if(Input.GetButtonDown("A Button") && cuttedFish)
        {
            Debug.Log("Get component");
            //Add selected component in inventory
        }

        if(Input.GetButton("A Button") && fishReadyToCut)
        {
            Debug.Log("Cut Fish");
            //Decrement a timer (1.2f secs for test)
            //If timer = 0
            //Display components (if > 3 display line2)
        }

        if(Input.GetButtonDown("Y Button") && cuttedFish)
        {
            //Add all components to inventory
        }

        if (Input.GetButtonDown("B Button"))
        {
            UIManager.instance.CloseMenu(gameObject);
        }
    }
}
