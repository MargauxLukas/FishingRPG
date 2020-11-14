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
            if (Input.GetButton("Left Bumper"))  
            {
                
            }
            else if (Input.GetButton("Right Bumper"))   //Pull
            {
                if (!FishManager.instance.isAerial){PlayerManager.instance.IsAerial()              ;}
                else                               {PlayerManager.instance.CheckDistanceWithWater();}
            }

            if (Input.GetAxis("Right Trigger") > 0.1f)
            {
                PlayerManager.instance.IsTakingLine();
            }
            if (Input.GetAxis("Left Trigger") > 0.1f)
            {
                PlayerManager.instance.IsBlockingLine();
            }
            
            if(Input.GetAxis("Right Trigger") < 0.1f)
            {
                PlayerManager.instance.IsNotTakingLine();
            }

            if (Input.GetAxis("Left Trigger") < 0.1f)
            {
                PlayerManager.instance.IsNotBlockingLine();
            }

            if(Input.GetKey(KeyCode.E))
            {
                PlayerManager.instance.CHEAT_SetFishToExhausted();
            }

            FishingRodManager.instance.SetFishingRodPosition(Input.GetAxis("Right Stick (Horizontal)"));
        }
    }
}
