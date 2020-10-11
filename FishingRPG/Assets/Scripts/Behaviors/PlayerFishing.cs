using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishing : MonoBehaviour
{

    public bool blockLine;
    public bool pullTowards;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            FishingManager.instance.CancelFishing();
        }
    }
}
