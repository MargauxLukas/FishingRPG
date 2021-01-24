using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GearingManager : MonoBehaviour
{
    public List<Image> helmetList = new List<Image>(); 
    public List<Image> pauldronsList = new List<Image>();
    public List<Image> beltList = new List<Image>();
    public List<Image> bootsList = new List<Image>();
    public List<Image> fishingRodList = new List<Image>();
    public List<Image> gemsList = new List<Image>();
    public List<GameObject> gridHighlights = new List<GameObject>();

    public Image helmetEquiped;
    public Image shoulderEquiped;
    public Image beltEquiped;
    public Image bootsEquiped;
    public Image fishingRodEquiped;
    public Image gem1Equiped;
    public Image gem2Equiped;
    public Image gem3Equiped;

    public Gem gemMovement;
    private ScriptablePointer spMovement;

    private int indice = 0;

    public Text gearInfoTitle;
    public Text gearInfoUpgrade;
    public Text gearInfoStats;
    public Text gearInfoDescription;

    private bool isLeaving;
    private float leavingTimer;
    private float leavingTime = 1.2f;

    public Image holdButtonImg;

    public Text statsText;
    public int strength;
    public int constitution;
    public int dexterity;
    public int intelligence;

    public bool advertise = false;
    public bool needToAccept = false;
    public GameObject advertissement;

    void Update()
    {
        if(Input.GetButton("A Button") && needToAccept)
        {
            advertise = false;
            advertissement.SetActive(false);
        }

        if (Input.GetButton("Y Button"))
        {
            isLeaving = true;

            leavingTimer += Time.deltaTime;

            if (leavingTimer < leavingTime)
            {
                holdButtonImg.fillAmount = leavingTimer / leavingTime;
            }

            if (leavingTimer >= leavingTime)
            {
                leavingTimer = 0;
                isLeaving = false;
                holdButtonImg.fillAmount = 0;

                AkSoundEngine.PostEvent("OnHubLeave", gameObject);

                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    if (PlayerManager.instance.playerInventory.inventory.fishTotal > 9)
                    {
                        PlayerManager.instance.playerInventory.inventory.fishTotal = 9;
                        PlayerManager.instance.playerInventory.inventory.currentFishOnMe = 0;
                        PlayerManager.instance.playerInventory.inventory.fishNumberOnStock = 0;
                    }
                    else
                    {
                        PlayerManager.instance.playerInventory.inventory.currentFishOnMe = 0;
                        PlayerManager.instance.playerInventory.inventory.fishNumberOnStock = 0;
                    }
                }

                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    if (UIManager.instance.inventory.fishTotal > 0 && !needToAccept)
                    {
                        advertise = true;
                    }
                }

                if (!advertise)
                {
                    if (SceneManager.GetActiveScene().buildIndex == 2)
                    {
                        UIManager.instance.inventory.fishTotal = 0;
                        needToAccept = false;
                        ChangeScene(3);
                    }
                    else
                    {
                        ChangeScene(1);
                    }
                }
                else
                {
                    advertissement.SetActive(true);
                    needToAccept = true;
                }
            }
        }

        if (Input.GetButtonUp("Y Button") && isLeaving)
        {
            Debug.Log("Release");
            leavingTimer = 0;
            isLeaving = false;
            holdButtonImg.fillAmount = 0;
        }

        if (Input.GetButtonDown("B Button"))
        {
            for (int i = 0; i < gridHighlights.Count; i++)
            {
                gridHighlights[i].SetActive(false);
            }

            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                PlayerManager.instance.LeaveChestMenu();
                
            }
            else
            {
                UIManager.instance.CloseMenu(gameObject);
                //Play Sound
                AkSoundEngine.PostEvent("OnBuildingLeft", gameObject);
            }
        }
    }

    public void SetFirstSelected(GameObject _go)
    {
        EventSystem.current.SetSelectedGameObject(_go);
    }

    public void UpdateGear()
    {
        DefaultStats();

        //Casque
        for (int i = 0; i < UIManager.instance.helmetList.Count; i++)
        {
            if(UIManager.instance.inventory.CheckHelmet(UIManager.instance.helmetList[i].ID))
            {
                helmetList[indice].sprite = UIManager.instance.helmetList[i].appearance;
                helmetList[indice].transform.parent.GetComponent<GearingInfos>().itemName = UIManager.instance.helmetList[i].itemName;
                helmetList[indice].transform.parent.GetComponent<GearingInfos>().upgrade = UIManager.instance.helmetList[i].upgradeState;
                helmetList[indice].transform.parent.GetComponent<GearingInfos>().strength = UIManager.instance.helmetList[i].strength;
                helmetList[indice].transform.parent.GetComponent<GearingInfos>().constitution = UIManager.instance.helmetList[i].constitution;
                helmetList[indice].transform.parent.GetComponent<GearingInfos>().dexterity = UIManager.instance.helmetList[i].dexterity;
                helmetList[indice].transform.parent.GetComponent<GearingInfos>().intelligence = UIManager.instance.helmetList[i].intelligence;
                helmetList[indice].transform.parent.GetComponent<GearingInfos>().description = UIManager.instance.helmetList[i].description;
                helmetList[indice].gameObject.GetComponent<ScriptablePointer>().armor = UIManager.instance.helmetList[i];
                helmetList[indice].gameObject.SetActive(true);

                if (UIManager.instance.inventory.equipedHelmet != null)
                {
                    if (UIManager.instance.inventory.equipedHelmet.ID == UIManager.instance.helmetList[i].ID)
                    {
                        helmetList[indice].transform.parent.GetChild(1).gameObject.SetActive(true);
                        UpdateStats(UIManager.instance.inventory.equipedHelmet);
                    }
                }

                indice++;
            }
        }

        indice = 0;
        //pauldrons
        for (int i = 0; i < UIManager.instance.pauldronsList.Count; i++)
        {
            if(UIManager.instance.inventory.CheckPauldrons(UIManager.instance.pauldronsList[i].ID))
            {
                pauldronsList[indice].sprite = UIManager.instance.pauldronsList[i].appearance;
                pauldronsList[indice].transform.parent.GetComponent<GearingInfos>().itemName = UIManager.instance.pauldronsList[i].itemName;
                pauldronsList[indice].transform.parent.GetComponent<GearingInfos>().upgrade = UIManager.instance.pauldronsList[i].upgradeState;
                pauldronsList[indice].transform.parent.GetComponent<GearingInfos>().strength = UIManager.instance.pauldronsList[i].strength;
                pauldronsList[indice].transform.parent.GetComponent<GearingInfos>().constitution = UIManager.instance.pauldronsList[i].constitution;
                pauldronsList[indice].transform.parent.GetComponent<GearingInfos>().dexterity = UIManager.instance.pauldronsList[i].dexterity;
                pauldronsList[indice].transform.parent.GetComponent<GearingInfos>().intelligence = UIManager.instance.pauldronsList[i].intelligence;
                pauldronsList[indice].transform.parent.GetComponent<GearingInfos>().description = UIManager.instance.pauldronsList[i].description;
                pauldronsList[indice].gameObject.GetComponent<ScriptablePointer>().armor = UIManager.instance.pauldronsList[i];
                pauldronsList[indice].gameObject.SetActive(true);

                if (UIManager.instance.inventory.equipedShoulders != null)
                {
                    if (UIManager.instance.inventory.equipedShoulders.ID == UIManager.instance.pauldronsList[i].ID)
                    {
                        pauldronsList[indice].transform.parent.GetChild(1).gameObject.SetActive(true);
                        UpdateStats(UIManager.instance.inventory.equipedShoulders);
                    }
                }

                indice++;
            }
        }

        indice = 0;
        //belt
        for (int i = 0; i < UIManager.instance.beltList.Count; i++)
        {
            if(UIManager.instance.inventory.CheckBelt(UIManager.instance.beltList[i].ID))
            {
                beltList[indice].sprite = UIManager.instance.beltList[i].appearance;
                beltList[indice].transform.parent.GetComponent<GearingInfos>().itemName = UIManager.instance.beltList[i].itemName;
                beltList[indice].transform.parent.GetComponent<GearingInfos>().upgrade = UIManager.instance.beltList[i].upgradeState;
                beltList[indice].transform.parent.GetComponent<GearingInfos>().strength = UIManager.instance.beltList[i].strength;
                beltList[indice].transform.parent.GetComponent<GearingInfos>().constitution = UIManager.instance.beltList[i].constitution;
                beltList[indice].transform.parent.GetComponent<GearingInfos>().dexterity = UIManager.instance.beltList[i].dexterity;
                beltList[indice].transform.parent.GetComponent<GearingInfos>().intelligence = UIManager.instance.beltList[i].intelligence;
                beltList[indice].transform.parent.GetComponent<GearingInfos>().description = UIManager.instance.beltList[i].description;
                beltList[indice].gameObject.GetComponent<ScriptablePointer>().armor = UIManager.instance.beltList[i];
                beltList[indice].gameObject.SetActive(true);

                if (UIManager.instance.inventory.equipedBelt != null)
                {
                    if (UIManager.instance.inventory.equipedBelt.ID == UIManager.instance.beltList[i].ID)
                    {
                        beltList[indice].transform.parent.GetChild(1).gameObject.SetActive(true);
                        UpdateStats(UIManager.instance.inventory.equipedBelt);
                    }
                }

                indice++;
            }
        }

        indice = 0;
        //boots
        for (int i = 0; i < UIManager.instance.bootsList.Count; i++)
        {
            if(UIManager.instance.inventory.CheckBoots(UIManager.instance.bootsList[i].ID))
            {
                bootsList[indice].sprite = UIManager.instance.bootsList[i].appearance;
                bootsList[indice].transform.parent.GetComponent<GearingInfos>().itemName = UIManager.instance.bootsList[i].itemName;
                bootsList[indice].transform.parent.GetComponent<GearingInfos>().upgrade = UIManager.instance.bootsList[i].upgradeState;
                bootsList[indice].transform.parent.GetComponent<GearingInfos>().strength = UIManager.instance.bootsList[i].strength;
                bootsList[indice].transform.parent.GetComponent<GearingInfos>().constitution = UIManager.instance.bootsList[i].constitution;
                bootsList[indice].transform.parent.GetComponent<GearingInfos>().dexterity = UIManager.instance.bootsList[i].dexterity;
                bootsList[indice].transform.parent.GetComponent<GearingInfos>().intelligence = UIManager.instance.bootsList[i].intelligence;
                bootsList[indice].transform.parent.GetComponent<GearingInfos>().description = UIManager.instance.bootsList[i].description;
                bootsList[indice].gameObject.GetComponent<ScriptablePointer>().armor = UIManager.instance.bootsList[i];
                bootsList[indice].gameObject.SetActive(true);

                if (UIManager.instance.inventory.equipedBoots != null)
                {
                    if (UIManager.instance.inventory.equipedBoots.ID == UIManager.instance.bootsList[i].ID)
                    {
                        bootsList[indice].transform.parent.GetChild(1).gameObject.SetActive(true);
                        UpdateStats(UIManager.instance.inventory.equipedBoots);
                    }
                }

                indice++;
            }
        }

        indice = 0;
        //fishingRod
        for (int i = 0; i < UIManager.instance.fishingRodList.Count; i++)
        {
            if(UIManager.instance.inventory.CheckFishingRod(UIManager.instance.fishingRodList[i].ID))
            {
                fishingRodList[indice].sprite = UIManager.instance.fishingRodList[i].appearance;
                fishingRodList[indice].transform.parent.GetComponent<GearingInfos>().itemName = UIManager.instance.fishingRodList[i].itemName;
                fishingRodList[indice].transform.parent.GetComponent<GearingInfos>().upgrade = UIManager.instance.fishingRodList[i].upgradeState;
                fishingRodList[indice].transform.parent.GetComponent<GearingInfos>().strength = UIManager.instance.fishingRodList[i].strength;
                fishingRodList[indice].transform.parent.GetComponent<GearingInfos>().constitution = UIManager.instance.fishingRodList[i].constitution;
                fishingRodList[indice].transform.parent.GetComponent<GearingInfos>().dexterity = UIManager.instance.fishingRodList[i].dexterity;
                fishingRodList[indice].transform.parent.GetComponent<GearingInfos>().intelligence = UIManager.instance.fishingRodList[i].intelligence;
                fishingRodList[indice].transform.parent.GetComponent<GearingInfos>().description = UIManager.instance.fishingRodList[i].description;
                fishingRodList[indice].gameObject.GetComponent<ScriptablePointer>().fishingRod = UIManager.instance.fishingRodList[i];
                fishingRodList[indice].gameObject.SetActive(true);

                if (UIManager.instance.inventory.equipedFishingRod != null)
                {
                    if (UIManager.instance.inventory.equipedFishingRod.ID == UIManager.instance.fishingRodList[i].ID)
                    {
                        fishingRodList[indice].transform.parent.GetChild(1).gameObject.SetActive(true);
                        UpdateStats(UIManager.instance.inventory.equipedFishingRod);
                    }
                }

                indice++;
            }
        }

        indice = 0;
        //gems
        for (int i = 0; i < UIManager.instance.gemList.Count; i++)
        {
            if (UIManager.instance.inventory.CheckGems(UIManager.instance.gemList[i].ID))
            {
                gemsList[indice].sprite = UIManager.instance.gemList[i].appearance;
                gemsList[indice].transform.parent.GetComponent<GearingInfos>().itemName = UIManager.instance.gemList[i].gemName;
                gemsList[indice].transform.parent.GetComponent<GearingInfos>().upgrade = UIManager.instance.gemList[i].upgradeState;
                gemsList[indice].transform.parent.GetComponent<GearingInfos>().gemStats = UIManager.instance.gemList[i].stats;
                gemsList[indice].transform.parent.GetComponent<GearingInfos>().description = UIManager.instance.gemList[i].description;
                gemsList[indice].gameObject.GetComponent<ScriptablePointer>().gem = UIManager.instance.gemList[i];
                gemsList[indice].gameObject.SetActive(true);

                if (UIManager.instance.inventory.equipedGem1 != null)
                {
                    if (UIManager.instance.inventory.equipedGem1.ID == UIManager.instance.gemList[i].ID)
                    {
                        gemsList[indice].transform.parent.GetChild(1).gameObject.SetActive(true);
                    }
                }

                if (UIManager.instance.inventory.equipedGem2 != null)
                {
                    if (UIManager.instance.inventory.equipedGem2.ID == UIManager.instance.gemList[i].ID)
                    {
                        gemsList[indice].transform.parent.GetChild(1).gameObject.SetActive(true);
                    }
                }

                if (UIManager.instance.inventory.equipedGem3 != null)
                {
                    if (UIManager.instance.inventory.equipedGem3.ID == UIManager.instance.gemList[i].ID)
                    {
                        gemsList[indice].transform.parent.GetChild(1).gameObject.SetActive(true);
                    }
                }

                indice++;
            }
        }

        UpdateEquipped();
        SetText();
    }

    public void UpdateEquipped()
    {
        if (UIManager.instance.inventory.equipedHelmet != null)
        {
            helmetEquiped.sprite = UIManager.instance.inventory.equipedHelmet.appearance;
            helmetEquiped.enabled = true;
        }

        if (UIManager.instance.inventory.equipedShoulders != null)
        {
            shoulderEquiped.sprite = UIManager.instance.inventory.equipedShoulders.appearance;
            shoulderEquiped.enabled = true;
        }

        if (UIManager.instance.inventory.equipedBelt != null)
        {
            beltEquiped.sprite = UIManager.instance.inventory.equipedBelt.appearance;
            beltEquiped.enabled = true;
        }

        if (UIManager.instance.inventory.equipedBoots != null)
        {
            bootsEquiped.sprite = UIManager.instance.inventory.equipedBoots.appearance;
            bootsEquiped.enabled = true;
        }

        if (UIManager.instance.inventory.equipedFishingRod != null)
        {
            fishingRodEquiped.sprite = UIManager.instance.inventory.equipedFishingRod.appearance;
            fishingRodEquiped.enabled = true;
        }

        if (UIManager.instance.inventory.equipedGem1 != null)
        {
            gem1Equiped.sprite = UIManager.instance.inventory.equipedGem1.appearance;
            gem1Equiped.enabled = true;
        }

        if (UIManager.instance.inventory.equipedGem2 != null)
        {
            gem2Equiped.sprite = UIManager.instance.inventory.equipedGem2.appearance;
            gem2Equiped.enabled = true;
        }

        if (UIManager.instance.inventory.equipedGem3 != null)
        {
            gem3Equiped.sprite = UIManager.instance.inventory.equipedGem3.appearance;
            gem3Equiped.enabled = true;
        }
    }

    public void DefaultStats()
    {
        strength = 3;
        constitution = 7;
        dexterity = 3;
        intelligence = 3;
    }

    public void UpdateStats(ArmorSet armor)
    {
        strength     += armor.strength;
        constitution += armor.constitution;
        dexterity    += armor.dexterity;
        intelligence += armor.intelligence;
    }

    public void UpdateStats(FishingRod fishingRod)
    {
        strength += fishingRod.strength;
        constitution += fishingRod.constitution;
        dexterity += fishingRod.dexterity;
        intelligence += fishingRod.intelligence;
    }

    public void SetText()
    {
        statsText.text = strength + "\n" + constitution + "\n" + dexterity + "\n" + intelligence;
    }


    public void EquipArmor(ScriptablePointer sp)
    {
        switch(sp.armor.itemType)
        {
            case "Helmet":
                if(UIManager.instance.inventory.equipedHelmet != null)
                {
                    strength -= UIManager.instance.inventory.equipedHelmet.strength;
                    constitution -= UIManager.instance.inventory.equipedHelmet.constitution;
                    dexterity -= UIManager.instance.inventory.equipedHelmet.dexterity;
                    intelligence -= UIManager.instance.inventory.equipedHelmet.intelligence;
                }
                helmetEquiped.sprite = sp.armor.appearance;
                helmetEquiped.enabled = true;
                UIManager.instance.inventory.equipedHelmet = sp.armor;
                break;
            case "Shoulders":
                if (UIManager.instance.inventory.equipedShoulders != null)
                {
                    strength -= UIManager.instance.inventory.equipedShoulders.strength;
                    constitution -= UIManager.instance.inventory.equipedShoulders.constitution;
                    dexterity -= UIManager.instance.inventory.equipedShoulders.dexterity;
                    intelligence -= UIManager.instance.inventory.equipedShoulders.intelligence;
                }
                shoulderEquiped.sprite = sp.armor.appearance;
                shoulderEquiped.enabled = true;
                UIManager.instance.inventory.equipedShoulders = sp.armor;
                break;
            case "Belt":
                if (UIManager.instance.inventory.equipedBelt != null)
                {
                    strength -= UIManager.instance.inventory.equipedBelt.strength;
                    constitution -= UIManager.instance.inventory.equipedBelt.constitution;
                    dexterity -= UIManager.instance.inventory.equipedBelt.dexterity;
                    intelligence -= UIManager.instance.inventory.equipedBelt.intelligence;
                }
                beltEquiped.sprite = sp.armor.appearance;
                beltEquiped.enabled = true;
                UIManager.instance.inventory.equipedBelt = sp.armor;
                break;
            case "Boots":
                if (UIManager.instance.inventory.equipedBoots != null)
                {
                    strength -= UIManager.instance.inventory.equipedBoots.strength;
                    constitution -= UIManager.instance.inventory.equipedBoots.constitution;
                    dexterity -= UIManager.instance.inventory.equipedBoots.dexterity;
                    intelligence -= UIManager.instance.inventory.equipedBoots.intelligence;
                }
                bootsEquiped.sprite = sp.armor.appearance;
                bootsEquiped.enabled = true;
                UIManager.instance.inventory.equipedBoots = sp.armor;
                break;

                
        }

        //Play Sound
        AkSoundEngine.PostEvent("OnStuffEquipped", gameObject);
        UpdateStats(sp.armor);
        SetText();
        sp.gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
    }

    public void EquipFishingRod(ScriptablePointer sp)
    {
        if (UIManager.instance.inventory.equipedFishingRod != null)
        {
            strength -= UIManager.instance.inventory.equipedFishingRod.strength;
            constitution -= UIManager.instance.inventory.equipedFishingRod.constitution;
            dexterity -= UIManager.instance.inventory.equipedFishingRod.dexterity;
            intelligence -= UIManager.instance.inventory.equipedFishingRod.intelligence;
        }
        fishingRodEquiped.sprite = sp.fishingRod.appearance;
        fishingRodEquiped.enabled = true;
        UIManager.instance.inventory.equipedFishingRod = sp.fishingRod;
        UpdateStats(sp.fishingRod);
        SetText();
        sp.gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);

        //Play Sound
        AkSoundEngine.PostEvent("OnStuffEquipped", gameObject);


    }

    public void EquipGems(Image im)
    {
        im.sprite = gemMovement.appearance;
        im.enabled = true;

        if (im.gameObject.transform.parent.name.Contains("1"))
        {
            UIManager.instance.inventory.equipedGem1 = gemMovement;
        }
        if (im.gameObject.transform.parent.name.Contains("2"))
        {
            UIManager.instance.inventory.equipedGem2 = gemMovement;
        }
        if (im.gameObject.transform.parent.name.Contains("3"))
        {
            UIManager.instance.inventory.equipedGem3 = gemMovement;
        }
        //Play Sound
        AkSoundEngine.PostEvent("OnGemEquipped", gameObject);
        spMovement.gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
    }

    public void GetGemInMovement(ScriptablePointer sp)
    {
        gemMovement = sp.gem;
        spMovement = sp;
    }

    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void SetLootInfos(GearingInfos _infos)
    {
        switch (_infos.upgrade)
        {
            case 1:
                gearInfoTitle.color = new Color32(0, 180, 85, 255);
                break;

            case 2:
                gearInfoTitle.color = new Color32(0, 112, 221, 255);
                break;

            case 3:
                gearInfoTitle.color = new Color32(163, 53, 238, 255);
                break;

            case 4:
                gearInfoTitle.color = new Color32(255, 128, 0, 255);
                break;
        }

        gearInfoTitle.text = _infos.itemName;

        if(_infos.upgrade > 0)
        {
            gearInfoUpgrade.text = "Rank " + _infos.upgrade;
        }
        else
        {
            gearInfoUpgrade.text = string.Empty;
        }

        gearInfoStats.text = string.Empty;

        if (_infos.strength > 0)
        {
            gearInfoStats.text += "+" + _infos.strength + " strength";
        }
        if(_infos.constitution > 0)
        {
            gearInfoStats.text += "\n+" + _infos.constitution + " weight";
        }
        if(_infos.dexterity > 0)
        {
            gearInfoStats.text += "\n+" + _infos.dexterity + " agility";
        }
        if(_infos.intelligence > 0)
        {
            gearInfoStats.text += "\n+" + _infos.intelligence + " intelligence";
        }

        gearInfoDescription.text = _infos.description;
    }

    public void SetGemInfos(GearingInfos _infos)
    {
        Debug.Log(_infos.gemStats);
        gearInfoTitle.text = _infos.itemName;
        gearInfoUpgrade.text = "Rank " + _infos.upgrade;
        gearInfoStats.text = _infos.gemStats;
        gearInfoDescription.text = _infos.description;
    }
}
