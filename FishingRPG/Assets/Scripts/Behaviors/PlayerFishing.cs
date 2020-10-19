using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishing : MonoBehaviour
{
    public bool isReadyToFish = false;

    private void Update()
    {
        /*if (Input.GetButtonUp("B Button"))
        {
            FishingManager.instance.CancelFishing();
        }*/

        if (isReadyToFish)
        {
            if (Input.GetButton("Left Bumper"))   //BlockLine
            {
                //PlayerManager.instance.IsBlockingLine();
            }
            else if (Input.GetButton("Right Bumper"))   //Pull
            {
                PlayerManager.instance.IsAerial();
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


            if (Input.GetAxis("Right Stick (Horizontal)") < -0.2f)
            {
                FishingRodManager.instance.LeftFishingRod();
            }
            else if (Input.GetAxis("Right Stick (Horizontal)") > 0.2f)
            {
                FishingRodManager.instance.RightFishingRod();
            }
        }
    }
}
