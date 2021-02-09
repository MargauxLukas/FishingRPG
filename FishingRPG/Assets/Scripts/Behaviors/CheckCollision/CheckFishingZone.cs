using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFishingZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            FishingManager.instance.isInFishingRod = true;
            if (!TutoManager.instance.chap2 && !PlayerManager.instance.playerInventory.inventory.tutoFini)
            {
                TutoManager.instance.Chap2Dialogue1();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            FishingManager.instance.isInFishingRod = false;
        }
    }
}
