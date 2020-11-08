using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishing : MonoBehaviour
{
    public bool isReadyToFish = false;

    private void Update()
    {
        if (isReadyToFish)
        {
            if (Input.GetButton("Left Bumper"))   //BlockLine
            {
                
            }
            else if (Input.GetButton("Right Bumper"))   //Pull
            {
                if (!FishManager.instance.isAerial){PlayerManager.instance.IsAerial()              ;}
                else                               {PlayerManager.instance.CheckDistanceWithWater();}
            }
            else if (Input.GetAxis("Right Trigger") > 0.1f)
            {
                PlayerManager.instance.IsPullingTowards();
            }
            else if (Input.GetAxis("Left Trigger") > 0.1f)
            {
                PlayerManager.instance.IsBlockingLine();
            }
            else
            {
                PlayerManager.instance.NothingPushed();
            }

            FishingRodManager.instance.SetFishingRodPosition(Input.GetAxis("Right Stick (Horizontal)"));
        }
    }
}
