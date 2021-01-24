using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject player;
    public GameObject playerView;
    public PlayerStats playerStats;
    public PlayerGem playerGem;
    public PlayerInventory playerInventory;
    public bool canMove = true;

    [Space]

    public GameObject chestGUI;
    public GameObject inventoryGUI;
    public GameObject fishStockGUI;
    public GameObject combatGUI;
    public GameObject questGUI;

    public CheckBox cb;
    public CheckHub ch;
    public CheckFishVictoryZone cfvz;

    public GameObject canvas;
    private bool dataCheat = false;
    public bool isOnMenu = false;
    public bool isPause = false;
    public bool isFishStock = false;
    public bool isQuestMenu = false;


    public float speed = 5.5f;
    public bool isPressingRT = false;

    public GameObject firstChestSelected;

    public GearingManager gearingManager;
    public Text statsText;
    private int strength;
    private int constitution;
    private int dexterity;
    private int intelligence;

    //Inventaire
    public GameObject fish1;
    public GameObject fish2;
    public GameObject fish3;

    public GameObject selectedRef;

    //Gem
    public Image cooldownGem1;
    public Image cooldownGem2;
    public Image cooldownGem3;
    public Image durationGem1;
    public Image durationGem2;
    public Image durationGem3;

    public Image helmetEquiped;
    public Image shoulderEquiped;
    public Image beltEquiped;
    public Image bootsEquiped;
    public Image fishingRodEquiped;
    public Image gem1Equiped;
    public Image gem2Equiped;
    public Image gem3Equiped;

    public bool MoulinetOnce =false;



    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        /*
        strength = 3;
        constitution = 7;
        dexterity = 3;
        intelligence = 3;
        */

        UpdateStats();
        UpdateGem();
    }

    public virtual void Init()
    {
        instance = this;
    }

    public void DisablePlayerMovement()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        canMove = false;
    }

    public void EnablePlayerMovement()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        canMove = true;
    }

    public void DisableFishMovement()
    {
        player.GetComponent<PlayerFishing>().isReadyToFish = false;
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
        gearingManager.UpdateGear();
        chestGUI.SetActive(true);
        combatGUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstChestSelected);
        isOnMenu = true;
        //Play Sound
        AkSoundEngine.PostEvent("OnChestOpen", gameObject);
    }

    public void LeaveChestMenu()
    {
        chestGUI.SetActive(false);
        combatGUI.SetActive(true);
        UpdateStats();
        UpdateGem();
        isOnMenu = false;
        //Play Sound
        AkSoundEngine.PostEvent("OnChestClosed", gameObject);
    }

    public void OpenInventoryMenu()
    {
        inventoryGUI.SetActive(true);
        combatGUI.SetActive(false);
        UpdateStatsInventory();
        UpdateInventoryFish(playerInventory.inventory.currentFishOnMe);
        isPause = true;
    }

    public void LeaveInventoryMenu()
    {
        inventoryGUI.SetActive(false);
        combatGUI.SetActive(true);
        isPause = false;
    }

    public void OpenQuestMenu()
    {
        questGUI.SetActive(true);
        isQuestMenu = true;
    }

    public void LeaveQuestMenu()
    {
        questGUI.SetActive(false);
        isQuestMenu = false;
    }

    public void OpenFishingStockMenu()
    {
        fishStockGUI.SetActive(true);
        combatGUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(selectedRef);
        isFishStock = true;
    }

    public void LeaveFishingStockMenu()
    {
        fishStockGUI.SetActive(false);
        combatGUI.SetActive(true);
        isFishStock = false;
    }

    public void UpdateInventoryStats()
    {
        statsText.text = strength + "\n" + constitution + "\n" + dexterity + "\n" + intelligence;
    }

    public void StockOneFish(Image im)
    {
        if (playerInventory.inventory.currentFishOnMe > 0)
        {
            playerInventory.inventory.currentFishOnMe--;
            playerInventory.inventory.fishNumberOnStock++;

            im.enabled = true;
            im.transform.parent.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void UpdateInventoryFish(int fishTotal)
    {
        switch(fishTotal)
        {
            case 0:
                fish1.SetActive(false);
                fish2.SetActive(false);
                fish3.SetActive(false);
                break;
            case 1:
                fish1.SetActive(true);
                fish2.SetActive(false);
                fish3.SetActive(false);
                break;
            case 2:
                fish1.SetActive(true);
                fish2.SetActive(true);
                fish3.SetActive(false);
                break;
            case 3:
                fish1.SetActive(true);
                fish2.SetActive(true);
                fish3.SetActive(true);
                break;
        }
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
        //Play Sound -> Moulinet sound
        if (!MoulinetOnce) 
        {
            Debug.Log("Je joue le son du moulinet");
            AkSoundEngine.PostEvent("OnMoulinetOn", gameObject);
            MoulinetOnce = true;
        }  
    }

    public void IsTakingLineBobber()
    {
        FishingRodManager.instance.animFishingRod.SetFloat("SpeedMultiplier", 1);
        FishingRodManager.instance.bobber.transform.LookAt(new Vector3(FishingRodManager.instance.pointC.position.x, FishingRodManager.instance.bobber.transform.position.y, FishingRodManager.instance.pointC.position.z));
        FishingRodManager.instance.bobber.transform.position += FishingRodManager.instance.bobber.transform.forward * 3f * Time.deltaTime;

        
    }

    public void IsAerial()
    {
        FishingRodManager.instance.fishingRodPivot.GetComponent<Rotate>().AerialRotation();
        StartCoroutine(WaitBeforeAerial());
        //FishManager.instance.IsExhausted();
    }

    IEnumerator WaitBeforeAerial()
    {
        yield return new WaitForSeconds(0.2f);
        FishManager.instance.IsExhausted();
    }

    public void FellingFish()
    {
        FishingRodManager.instance.fishingRodPivot.GetComponent<Rotate>().FellRotation();
        FishManager.instance.FellAerial();
    }

    public void CheckDistanceWithWater()
    {
        Debug.Log("!!!!!! Max Time Aerial : " + FishManager.instance.currentFishBehavior.maxTimeAerial + " // " + FishManager.instance.currentFishBehavior.timerAerial + " > " + (FishManager.instance.currentFishBehavior.maxTimeAerial - UtilitiesManager.instance.GetTimingForMoreAerial()));

        FishingRodManager.instance.fishingRodPivot.GetComponent<Rotate>().AerialRotation();


        if (FishManager.instance.currentFishBehavior.timerAerial > FishManager.instance.currentFishBehavior.maxTimeAerial - UtilitiesManager.instance.GetTimingForMoreAerial())
        {
            FishManager.instance.MoreAerial();
        }
        else
        {
            //FishingRodManager.instance.fishingRodPivot.GetComponent<Rotate>().AerialRotation();
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
            //Stop Sound -> Moulinet sound
            AkSoundEngine.PostEvent("STOP_MoulinetOn", gameObject);
            MoulinetOnce = false;
        }
    }

    public void UseGemFirstSlot()
    {
        if (FishingRodManager.instance.slot1.gem)
        {
            playerGem.PlayGem(FishingRodManager.instance.slot1.gem, 1);
        }
        else
        {
            Debug.Log("Pas de gemme équipé en Slot 1");
        }
    }

    public void UseGemSecondSlot()
    {
        if (FishingRodManager.instance.slot2.gem)
        {
            playerGem.PlayGem(FishingRodManager.instance.slot2.gem, 2);
        }
        else
        {
            Debug.Log("Pas de gemme équipé en Slot 2");
        }
    }

    public void UseGemThirdSlot()
    {
        if (FishingRodManager.instance.slot3.gem)
        {
            playerGem.PlayGem(FishingRodManager.instance.slot3.gem, 3);
        }
        else
        {
            Debug.Log("Pas de gemme équipé en Slot 3");
        }
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
        playerStats.constitution = 7;
        playerStats.dexterity    = 3;
        playerStats.intelligence = 3;
    }

    public void ResetStatsInventory()
    {
        strength = 3;
        constitution = 7;
        dexterity = 3;
        intelligence = 3;
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
            helmetEquiped.sprite = playerInventory.inventory.equipedHelmet.appearance;
        }
        else
        {
            helmetEquiped.enabled = false;
        }

        if (playerInventory.inventory.equipedShoulders != null)
        {
            playerStats.strenght     += playerInventory.inventory.equipedShoulders.strength;
            playerStats.constitution += playerInventory.inventory.equipedShoulders.constitution;
            playerStats.dexterity    += playerInventory.inventory.equipedShoulders.dexterity;
            playerStats.intelligence += playerInventory.inventory.equipedShoulders.intelligence;
            shoulderEquiped.sprite = playerInventory.inventory.equipedShoulders.appearance;
        }
        else
        {
            shoulderEquiped.enabled = false;
        }

        if (playerInventory.inventory.equipedBelt != null)
        {
            playerStats.strenght     += playerInventory.inventory.equipedBelt.strength;
            playerStats.constitution += playerInventory.inventory.equipedBelt.constitution;
            playerStats.dexterity    += playerInventory.inventory.equipedBelt.dexterity;
            playerStats.intelligence += playerInventory.inventory.equipedBelt.intelligence;
            beltEquiped.sprite = playerInventory.inventory.equipedBelt.appearance;
        }
        else
        {
            beltEquiped.enabled = false;
        }

        if (playerInventory.inventory.equipedBoots != null)
        {
            playerStats.strenght     += playerInventory.inventory.equipedBoots.strength;
            playerStats.constitution += playerInventory.inventory.equipedBoots.constitution;
            playerStats.dexterity    += playerInventory.inventory.equipedBoots.dexterity;
            playerStats.intelligence += playerInventory.inventory.equipedBoots.intelligence;
            bootsEquiped.sprite = playerInventory.inventory.equipedBoots.appearance;
        }
        else
        {
            bootsEquiped.enabled = false;
        }

        if (playerInventory.inventory.equipedFishingRod != null)
        {
            playerStats.strenght     += playerInventory.inventory.equipedFishingRod.strength;
            playerStats.constitution += playerInventory.inventory.equipedFishingRod.constitution;
            playerStats.dexterity    += playerInventory.inventory.equipedFishingRod.dexterity;
            playerStats.intelligence += playerInventory.inventory.equipedFishingRod.intelligence;
            fishingRodEquiped.sprite = playerInventory.inventory.equipedFishingRod.appearance;
        }
        else
        {
            fishingRodEquiped.enabled = false;
        }

        if (playerInventory.inventory.equipedGem1 != null)
        {
            gem1Equiped.sprite = playerInventory.inventory.equipedGem1.appearance;
        }
        else
        {
            gem1Equiped.enabled = false;
        }

        if (playerInventory.inventory.equipedGem2 != null)
        {
            gem2Equiped.sprite = playerInventory.inventory.equipedGem2.appearance;
        }
        else
        {
            gem2Equiped.enabled = false;
        }

        if (playerInventory.inventory.equipedGem3 != null)
        {
            gem3Equiped.sprite = playerInventory.inventory.equipedGem3.appearance;
        }
        else
        {
            gem3Equiped.enabled = false;
        }
    }

    public void UpdateStatsInventory()
    {
        ResetStatsInventory();

        if (playerInventory.inventory.equipedHelmet != null)
        {
            strength += playerInventory.inventory.equipedHelmet.strength;
            constitution += playerInventory.inventory.equipedHelmet.constitution;
            dexterity += playerInventory.inventory.equipedHelmet.dexterity;
            intelligence += playerInventory.inventory.equipedHelmet.intelligence;
        }

        if (playerInventory.inventory.equipedShoulders != null)
        {
            strength += playerInventory.inventory.equipedShoulders.strength;
            constitution += playerInventory.inventory.equipedShoulders.constitution;
            dexterity += playerInventory.inventory.equipedShoulders.dexterity;
            intelligence += playerInventory.inventory.equipedShoulders.intelligence;
        }

        if (playerInventory.inventory.equipedBelt != null)
        {
            strength += playerInventory.inventory.equipedBelt.strength;
            constitution += playerInventory.inventory.equipedBelt.constitution;
            dexterity += playerInventory.inventory.equipedBelt.dexterity;
            intelligence += playerInventory.inventory.equipedBelt.intelligence;
        }

        if (playerInventory.inventory.equipedBoots != null)
        {
            strength += playerInventory.inventory.equipedBoots.strength;
            constitution += playerInventory.inventory.equipedBoots.constitution;
            dexterity += playerInventory.inventory.equipedBoots.dexterity;
            intelligence += playerInventory.inventory.equipedBoots.intelligence;
        }

        if (playerInventory.inventory.equipedFishingRod != null)
        {
            strength += playerInventory.inventory.equipedFishingRod.strength;
            constitution += playerInventory.inventory.equipedFishingRod.constitution;
            dexterity += playerInventory.inventory.equipedFishingRod.dexterity;
            intelligence += playerInventory.inventory.equipedFishingRod.intelligence;
        }

        UpdateInventoryStats();
    }

    public void UpdateGem()
    {
        if(playerInventory.inventory.equipedGem1 != null)
        {
            FishingRodManager.instance.slot1.gem = playerInventory.inventory.equipedGem1;
            FishingRodManager.instance.slot1.visual.SetActive(true);
        }

        if (playerInventory.inventory.equipedGem2 != null)
        {
            FishingRodManager.instance.slot2.gem = playerInventory.inventory.equipedGem2;
            FishingRodManager.instance.slot2.visual.SetActive(true);
        }

        if (playerInventory.inventory.equipedGem3 != null)
        {
            FishingRodManager.instance.slot2.gem = playerInventory.inventory.equipedGem3;
            FishingRodManager.instance.slot2.visual.SetActive(true);
        }
    }

    public void UpdateUIGem(int i, float timer, float max)
    {
        if (i == 1)
        {
            durationGem1.fillAmount = timer / max;
        }

        if (i == 2)
        {
            durationGem2.fillAmount = timer / max;
        }

        if (i == 3)
        {
            durationGem3.fillAmount = timer / max;
        }
    }

    public void UpdateUIGemCD(int i, float timer, float max)
    {
        if (i == 1)
        {
            cooldownGem1.fillAmount = timer / max;
        }

        if (i == 2)
        {
            cooldownGem2.fillAmount = timer / max;
        }

        if (i == 3)
        {
            cooldownGem3.fillAmount = timer / max;
        }
    }
}
