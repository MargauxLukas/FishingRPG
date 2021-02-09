using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFishingZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FishingManager.instance.isInFishingRod = true;
        Debug.Log("zone fishing");
        if(other.gameObject.name.Contains("Player") && !TutoManager.instance.chap2 && !PlayerManager.instance.playerInventory.inventory.tutoFini)
        {
            TutoManager.instance.Chap2Dialogue1();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FishingManager.instance.isInFishingRod = false;
    }
}
