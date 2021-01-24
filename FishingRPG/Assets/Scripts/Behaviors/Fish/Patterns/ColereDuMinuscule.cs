using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColereDuMinuscule : MonoBehaviour
{
    static int strentghModifier = 2;

    public static void Play(float energyCost)
    {
        Debug.Log("Colere du Minuscule !");

        if (FishingManager.instance.isSnap)
        {
            AkSoundEngine.SetSwitch("CurrentFishInCombat", "SnapSnack", FishManager.instance.currentFish.gameObject);
            //Play Sound
            AkSoundEngine.PostEvent("OnRage", FishManager.instance.currentFish.gameObject);
        }
        else
        {
            AkSoundEngine.SetSwitch("CurrentFishInCombat", "ReefCrusher", FishManager.instance.currentFish.gameObject);
            //Play Sound
            AkSoundEngine.PostEvent("OnRage", FishManager.instance.currentFish.gameObject);
        }

        FishManager.instance.currentFishBehavior.strength += 2;
        FishManager.instance.currentFishBehavior.currentStamina -= energyCost;
        FishManager.instance.currentFishBehavior.isRage = true;
        FishManager.instance.currentFishBehavior.animator.SetBool("isRage", true);

        FishManager.instance.currentFish.GetComponent<FishPatterns>().ResetPattern();
    }
}
