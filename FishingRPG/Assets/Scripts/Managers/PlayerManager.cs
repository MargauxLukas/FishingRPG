﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject player;

    public bool blockLine;
    public bool pullTowards;

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
    }

    public void IsPullingTowards()
    {
        pullTowards = true;
    }

    public void NothingPushed()
    {
        blockLine = false;
        pullTowards = false;
    }
}
