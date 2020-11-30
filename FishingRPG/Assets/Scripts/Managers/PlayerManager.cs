using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject player;
    public GameObject playerView;
    public PlayerStats playerStats;

    public float speed = 9f;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    public void DisablePlayerMovement()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
    }

    public void EnablePlayerMovement()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    public void DisableFishMovement()
    {
        player.GetComponent<PlayerFishing>().enabled = false;
    }

    public void EnableFishMovement()
    {
        player.GetComponent<PlayerFishing>().enabled = true;
    }

    public void FishingCanStart()
    {
        player.GetComponent<PlayerFishing>().isReadyToFish = true;
    }

    public void IsBlockingLine()
    {
        FishingRodManager.instance.fishingLine.isBlocked = true;
        FishingRodManager.instance.fishingLine.textBlocked.color = Color.green;
    }

    public void IsTakingLine()
    {
        FishingRodManager.instance.fishingLine.isTaken = true;
        FishingRodManager.instance.fishingLine.textTaken.color = Color.green;
    }

    public void IsAerial()
    {
        FishManager.instance.IsExtenued();
    }

    public void FellingFish()
    {
        FishManager.instance.FellAerial();
    }

    public void CheckDistanceWithWater()
    {
        //Debug.Log(FishManager.instance.currentFishBehavior.timerAerial + " > " + (FishManager.instance.currentFishBehavior.maxTimeAerial - UtilitiesManager.instance.GetTimingForMoreAerial()));
        if(FishManager.instance.currentFishBehavior.timerAerial > FishManager.instance.currentFishBehavior.maxTimeAerial - UtilitiesManager.instance.GetTimingForMoreAerial())
        {
            FishManager.instance.MoreAerial();
        }
    }

    public void IsNotBlockingLine()
    {
        FishingRodManager.instance.fishingLine.isBlocked = false;
        FishingRodManager.instance.fishingLine.textBlocked.color = Color.red;
    }

    public void IsNotTakingLine()
    {
        if (FishingRodManager.instance.fishingLine.fCurrent < FishingRodManager.instance.fishingLine.fMax)
        {
            FishingRodManager.instance.fishingLine.isTaken = false;
            FishingRodManager.instance.fishingLine.textTaken.color = Color.red;
        }
    }

    public void UseGemFirstSlot()
    {
        
    }

    public void UseGemSecondSlot()
    {

    }

    public void UseGemThirdSlot()
    {

    }

    public void CHEAT_SetFishToExhausted()
    {
        FishManager.instance.currentFishBehavior.currentStamina = 0f;
        FishManager.instance.currentFishBehavior.CheckEndurance();
    }

    public void CHEAT_SetFishToDead()
    {
        FishManager.instance.currentFishBehavior.currentLife = 0f;
        FishManager.instance.currentFishBehavior.CheckLife();
    }
}
