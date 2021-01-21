using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFishingZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FishingManager.instance.isInFishingRod = true;
    }

    private void OnTriggerExit(Collider other)
    {
        FishingManager.instance.isInFishingRod = false;
    }
}
