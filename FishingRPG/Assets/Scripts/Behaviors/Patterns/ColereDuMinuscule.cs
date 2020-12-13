using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColereDuMinuscule : MonoBehaviour
{
    static int strentghModifier = 2;

    public static void Play(float energyCost)
    {
        Debug.Log("Colere du Minuscule !");

        FishManager.instance.currentFishBehavior.strength += 2;
        FishManager.instance.currentFishBehavior.currentStamina -= energyCost;
        FishManager.instance.currentFishBehavior.isRage = true;
        FishManager.instance.currentFishBehavior.animator.SetBool("isRage", true);

        FishManager.instance.currentFish.GetComponent<FishPatterns>().ResetPattern();
    }
}
