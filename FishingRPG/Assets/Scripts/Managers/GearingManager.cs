using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GearingManager : MonoBehaviour
{
    public List<Image> helmetList = new List<Image>(); 
    public List<Image> pauldronsList = new List<Image>();
    public List<Image> beltList = new List<Image>();
    public List<Image> bootsList = new List<Image>();
    public List<Image> fishingRodList = new List<Image>();
    public List<Image> gemRodList = new List<Image>();

    public int indice = 0;

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
                //Mettre scriptable
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
                //Mettre scriptable
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
                //Mettre scriptable
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
                //Mettre scriptable
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
                //Mettre scriptable
                fishingRodList[indice].gameObject.SetActive(true);
                indice++;
            }
        }

        //gems
        /*for (int i = 0; i < UIManager.instance.gemList.Count; i++)
        {
            UIManager.instance.inventory.CheckGems(UIManager.instance.gemList[0].ID);
        }*/
    }
}
