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

    public bool blockLine;
    public bool pullTowards;

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
        blockLine = true;
        FishingRodManager.instance.fishingLine.isBlocked = true;
    }

    public void IsPullingTowards()
    {
        pullTowards = true;
    }

    public void IsAerial()
    {
        FishManager.instance.IsExtenued();
    }

    public void CheckDistanceWithWater()
    {
        if(FishManager.instance.currentFishBehavior.timer > FishManager.instance.currentFishBehavior.maxTime * FishManager.instance.currentFishBehavior.percentOfMaxTime)
        {
            FishManager.instance.MoreAerial();
        }
    }

    public void NothingPushed()
    {
        blockLine = false;
        pullTowards = false;

        if (FishingRodManager.instance.fishingLine.fCurrent < FishingRodManager.instance.fishingLine.fMax)
        {
            FishingRodManager.instance.fishingLine.isBlocked = false;
        }
    }
}
