using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FeinteDuFourbe : MonoBehaviour
{
    static bool playOnce = false;
    static int speedModifier = 4;

    static float timer = 50f;

    public static void Play(float dotDuration, float energyCost, bool costEnergyOverTime)
    {
        Debug.Log("Feinte du Fourbe");

        if (!playOnce)
        {
            if (costEnergyOverTime && (FishManager.instance.currentFishBehavior.currentStamina - energyCost) > 0) { FishManager.instance.currentFishBehavior.currentStamina -= energyCost; }

            FishManager.instance.currentFishBehavior.baseSpeed += speedModifier;
            playOnce = true;
        }

        timer += Time.fixedDeltaTime;

        if(timer > dotDuration)
        {
            if (!costEnergyOverTime && (FishManager.instance.currentFishBehavior.currentStamina - energyCost) > 0) {FishManager.instance.currentFishBehavior.currentStamina -= energyCost;}

            FishManager.instance.currentFishBehavior.ChooseDirection();
            timer = 0f;
        }

        FishManager.instance.currentFishBehavior.Idle();
    }
}
