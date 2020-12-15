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

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButton("Y Button"))
        {
            Debug.Log("Cut Fish");
            isLeaving = true;

            leavingTimer += Time.fixedDeltaTime;

            if (leavingTimer < leavingTime)
            {
                holdButtonImg.fillAmount = leavingTimer / leavingTime;
            }

            if (leavingTimer >= leavingTime)
            {
                leavingTimer = 0;
                isLeaving = false;
                holdButtonImg.fillAmount = 0;

                ChangeScene(0);
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

            UIManager.instance.CloseMenu(gameObject);
        }
    }

    public void SetFirstSelected(GameObject _go)
    {
        EventSystem.current.SetSelectedGameObject(_go);
    }

    public void UpdateGear()
    {
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
                indice++;
            }
        }
    }

    public void EquipArmor(ScriptablePointer sp)
    {
        switch(sp.armor.itemType)
        {
            case "Helmet":
                helmetEquiped.sprite = sp.armor.appearance;
                helmetEquiped.enabled = true;
                UIManager.instance.inventory.equipedHelmet = sp.armor;
                break;
            case "Shoulders":
                shoulderEquiped.sprite = sp.armor.appearance;
                shoulderEquiped.enabled = true;
                UIManager.instance.inventory.equipedShoulders = sp.armor;
                break;
            case "Belt":
                beltEquiped.sprite = sp.armor.appearance;
                beltEquiped.enabled = true;
                UIManager.instance.inventory.equipedBelt = sp.armor;
                break;
            case "Boots":
                bootsEquiped.sprite = sp.armor.appearance;
                bootsEquiped.enabled = true;
                UIManager.instance.inventory.equipedBoots = sp.armor;
                break;
        }

        sp.gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
    }

    public void EquipFishingRod(ScriptablePointer sp)
    {
        fishingRodEquiped.sprite = sp.fishingRod.appearance;
        fishingRodEquiped.enabled = true;
        UIManager.instance.inventory.equipedFishingRod = sp.fishingRod;

        sp.gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
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
