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

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("B Button"))
        {
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
}
