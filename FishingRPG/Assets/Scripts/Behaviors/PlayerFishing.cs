using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishing : MonoBehaviour
{
    public bool isReadyToFish = false;

    

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            FishingManager.instance.CancelFishing();
        }

        if (isReadyToFish)
        {
            if (Input.GetKey(KeyCode.A))   //BlockLine
            {
                PlayerManager.instance.IsBlockingLine();
            }
            else if (Input.GetKey(KeyCode.E))   //Pull
            {
                PlayerManager.instance.IsPullingTowards();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerManager.instance.IsAerial();
            }
            else
            {
                PlayerManager.instance.NothingPushed();
            }
        }
    }
}
