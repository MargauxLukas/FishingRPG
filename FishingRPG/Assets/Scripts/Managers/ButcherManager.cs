using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButcherManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("A Button"))
        {
            Debug.Log("Cut");
            //Move fish on cutting table
            //Display Drops
            //Add components to inventory
        }

        if (Input.GetButtonDown("B Button"))
        {
            UIManager.instance.CloseMenu(gameObject);
        }
    }
}
