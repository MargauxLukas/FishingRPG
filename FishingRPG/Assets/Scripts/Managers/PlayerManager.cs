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
    public PlayerGem playerGem;
    public PlayerInventory playerInventory;

    public GameObject hubGUI;
    public GameObject inventoryGUI;
    public GameObject aventureGUI;

    public CheckBox cb;
    public CheckFishVictoryZone cfvz;

    public GameObject canvas;
    private bool dataCheat = false;
    public bool isOnMenu = false;

    public float speed = 9f;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        UpdateStats();
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

    public void OpenChestMenu()
    {
        hubGUI.SetActive(true);
        aventureGUI.SetActive(false);
        PlayerManager.instance.isOnMenu = true;
    }

    public void LeaveChestMenu()
    {
        hubGUI.SetActive(false);
        aventureGUI.SetActive(true);
        PlayerManager.instance.isOnMenu = false;
    }

    public void OpenInventoryMenu()
    {
        inventoryGUI.SetActive(true);
        aventureGUI.SetActive(false);
        PlayerManager.instance.isOnMenu = true;
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
        FishManager.instance.IsExhausted();
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
        playerGem.PlayGem(FishingRodManager.instance.slot1.gem);
    }

    public void UseGemSecondSlot()
    {
        playerGem.PlayGem(FishingRodManager.instance.slot2.gem);
    }

    public void UseGemThirdSlot()
    {
        playerGem.PlayGem(FishingRodManager.instance.slot3.gem);
    }

    public void CHEAT_SetFishToExhausted()
    {
        FishManager.instance.currentFishBehavior.currentStamina = 0f;
        FishManager.instance.currentFishBehavior.CheckStamina();
    }

    public void CHEAT_SetFishToDead()
    {
        FishManager.instance.currentFishBehavior.currentLife = 0f;
        FishManager.instance.currentFishBehavior.CheckLife();
    }

    public void CHEAT_ShowData()
    {
        if (!dataCheat)
        { 
            canvas.SetActive(true);
            dataCheat = true;
        }
        else 
        { 
            canvas.SetActive(false);
            dataCheat = false;
        }
    }

    public void ResetStats()
    {
        playerStats.strenght     = 3;
        playerStats.constitution = 3;
        playerStats.dexterity    = 3;
        playerStats.intelligence = 3;
    }

    public void UpdateStats()
    {
        ResetStats();

        if(playerInventory.inventory.equipedHelmet != null)
        {
            playerStats.strenght     += playerInventory.inventory.equipedHelmet.strength;
            playerStats.constitution += playerInventory.inventory.equipedHelmet.constitution;
            playerStats.dexterity    += playerInventory.inventory.equipedHelmet.dexterity;
            playerStats.intelligence += playerInventory.inventory.equipedHelmet.intelligence;
        }

        if (playerInventory.inventory.equipedShoulders != null)
        {
            playerStats.strenght     += playerInventory.inventory.equipedShoulders.strength;
            playerStats.constitution += playerInventory.inventory.equipedShoulders.constitution;
            playerStats.dexterity    += playerInventory.inventory.equipedShoulders.dexterity;
            playerStats.intelligence += playerInventory.inventory.equipedShoulders.intelligence;
        }

        if (playerInventory.inventory.equipedBelt != null)
        {
            playerStats.strenght     += playerInventory.inventory.equipedBelt.strength;
            playerStats.constitution += playerInventory.inventory.equipedBelt.constitution;
            playerStats.dexterity    += playerInventory.inventory.equipedBelt.dexterity;
            playerStats.intelligence += playerInventory.inventory.equipedBelt.intelligence;
        }

        if (playerInventory.inventory.equipedBoots != null)
        {
            playerStats.strenght     += playerInventory.inventory.equipedBoots.strength;
            playerStats.constitution += playerInventory.inventory.equipedBoots.constitution;
            playerStats.dexterity    += playerInventory.inventory.equipedBoots.dexterity;
            playerStats.intelligence += playerInventory.inventory.equipedBoots.intelligence;
        }

        if (playerInventory.inventory.equipedFishingRod != null)
        {
            playerStats.strenght     += playerInventory.inventory.equipedFishingRod.strength;
            playerStats.constitution += playerInventory.inventory.equipedFishingRod.constitution;
            playerStats.dexterity    += playerInventory.inventory.equipedFishingRod.dexterity;
            playerStats.intelligence += playerInventory.inventory.equipedFishingRod.intelligence;
        }
    }
}
