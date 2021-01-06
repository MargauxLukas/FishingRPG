using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArtisanManager : MonoBehaviour
{
    public GameObject firstSelectedTab;
    GameObject currentSelectedTab;

    public List<Image> componentsImages = new List<Image>();
    public List<Text> componentsList = new List<Text>();
    public List<Text> componentsQuantityList = new List<Text>();
    public Text title;
    public Image image;
    public Text bonusStats;
    public Text description;

    private string tempoString;
    private bool canCraft = true;

    private bool isCrafting = false;
    private float craftingTimer;
    private float craftingTime = 1.2f;

    public Image holdButtonImg;


    void Start()
    {
        Debug.Log("Start");
        currentSelectedTab = firstSelectedTab;
        SetSelectedTabColor(currentSelectedTab);
        SetSelectedTabChilds(currentSelectedTab.GetComponent<TabNeighbours>().tabsTexts, currentSelectedTab.GetComponent<TabNeighbours>().selectedChild);
    }    

    void Update()
    {
        Debug.Log(currentSelectedTab);

        if(Input.GetKeyUp(KeyCode.C))
        {
            UIManager.instance.inventory.PST_C += 10;
            UIManager.instance.inventory.PSC_C += 10;
            UIManager.instance.inventory.PFL_R += 10;
            UIManager.instance.inventory.PFI_E += 10;
            UIManager.instance.inventory.PBH_L += 10;
        }

        if(Input.GetButtonDown("Left Bumper"))
        {
            Debug.Log("Left");
            ResetTabColor(currentSelectedTab);
            ResetSelectChilds(currentSelectedTab.GetComponent<TabNeighbours>().tabsTexts);

            //Select tab
            currentSelectedTab = currentSelectedTab.GetComponent<TabNeighbours>().selectedOnLeft;
            SetSelectedTabColor(currentSelectedTab);

            //Display items list
            SetSelectedTabChilds(currentSelectedTab.GetComponent<TabNeighbours>().tabsTexts, currentSelectedTab.GetComponent<TabNeighbours>().selectedChild);
        }
        else if (Input.GetButtonDown("Right Bumper"))
        {
            Debug.Log("Right");
            ResetTabColor(currentSelectedTab);
            ResetSelectChilds(currentSelectedTab.GetComponent<TabNeighbours>().tabsTexts);

            //Select tab
            currentSelectedTab = currentSelectedTab.GetComponent<TabNeighbours>().selectedOnRight;
            SetSelectedTabColor(currentSelectedTab);

            //Display items list
            SetSelectedTabChilds(currentSelectedTab.GetComponent<TabNeighbours>().tabsTexts, currentSelectedTab.GetComponent<TabNeighbours>().selectedChild);
        }

        if (Input.GetButton("Submit"))
        {
            Debug.Log("Cut Fish");
            isCrafting = true;

            craftingTimer += Time.fixedDeltaTime;

            if (craftingTimer < craftingTime)
            {
                holdButtonImg.fillAmount = craftingTimer / craftingTime;
            }

            if (craftingTimer >= craftingTime)
            {
                Debug.Log(EventSystem.current);
                ScriptablePointer sp = EventSystem.current.currentSelectedGameObject.GetComponent<ScriptablePointer>();
                CraftObject(sp);
              
                craftingTimer = 0;
                isCrafting = false;
                holdButtonImg.fillAmount = 0;
            }
        }

        if (Input.GetButtonUp("Submit") && isCrafting)
        {
            Debug.Log("Release");
            craftingTimer = 0;
            isCrafting = false;
            holdButtonImg.fillAmount = 0;
        }

        if (Input.GetButtonDown("B Button"))
        {
            UIManager.instance.CloseMenu(gameObject);
        }
    }

    void SetSelectedTabColor(GameObject _tab)
    {
        _tab.GetComponent<Image>().color = new Color32(254, 242, 184, 255);
        _tab.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color32(66, 41, 36, 255);
    }

    void ResetTabColor(GameObject _tab)
    {
        _tab.GetComponent<Image>().color = new Color32(66, 41, 36, 255);
        _tab.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color32(254, 242, 184, 255);
    }

    void SetSelectedTabChilds(GameObject _list, GameObject _first)
    {
        _list.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_first);
    }

    void ResetSelectChilds(GameObject _list)
    {
        _list.SetActive(false);
    }

    //////Event trigger functs\\\\\\
    public void SelectedColor(Text _txt)
    {
        _txt.color = new Color32(201, 148, 111, 255);
    }

    public void DeselectedColor(Text _txt)
    {
        _txt.color = new Color32(66, 41, 36, 255);
    }

    public void UpdateText(ScriptablePointer sp)
    {
        for(int i = 0; i < sp.armor.components.Length; i++)
        {
            if (UIManager.instance.inventory.GetVariable(sp.armor.components[i].ID) < sp.armor.componentsQty[i])
            {
                componentsList[i].color = new Color32(255, 0, 56, 255);
                componentsQuantityList[i].color = new Color32(255, 0, 56, 255);
            }
            else
            {
                componentsList[i].color = new Color32(62, 40, 31, 255);
                componentsQuantityList[i].color = new Color32(62, 40, 31, 255);
            }

            componentsImages[i].transform.GetChild(0).GetComponent<Image>().sprite = sp.armor.components[i].appearance;
            componentsList[i].text = sp.armor.components[i].type;

            componentsQuantityList[i].text = UIManager.instance.inventory.GetVariable(sp.armor.components[i].ID) + "/" + sp.armor.componentsQty[i].ToString();

            componentsImages[i].gameObject.SetActive(true);
            componentsList[i].gameObject.SetActive(true);
            componentsQuantityList[i].gameObject.SetActive(true);
        }

        for (int i = 3; i >= sp.armor.components.Length; i--)
        {
            componentsImages[i].gameObject.SetActive(false);
            componentsImages[i].gameObject.SetActive(false);
            componentsList[i].gameObject.SetActive(false);
            componentsQuantityList[i].gameObject.SetActive(false);
        }

        title.text = sp.armor.itemName;
        image.sprite = sp.armor.appearance;


        if(sp.armor.strength != 0)
        {
            tempoString = sp.armor.strength.ToString() + " strength\n"; 
        }
        if (sp.armor.constitution != 0)
        {
            tempoString += sp.armor.constitution.ToString() + " constitution\n";
        }
        if (sp.armor.dexterity != 0)
        {
            tempoString += sp.armor.dexterity.ToString() + " dexterity\n";
        }
        if (sp.armor.intelligence != 0)
        {
            tempoString += sp.armor.intelligence.ToString() + " intelligence\n";
        }

        bonusStats.text = tempoString;
        description.text = sp.armor.description;
    }

    public void UpdateTextRod(ScriptablePointer sp)
    {
        for (int i = 0; i < sp.fishingRod.components.Length; i++)
        {
            if (UIManager.instance.inventory.GetVariable(sp.fishingRod.components[i].ID) < sp.fishingRod.componentsQty[i])
            {
                componentsList[i].color = new Color32(255, 0, 56, 255);
                componentsQuantityList[i].color = new Color32(255, 0, 56, 255);
            }
            else
            {
                componentsList[i].color = new Color32(62, 40, 31, 255);
                componentsQuantityList[i].color = new Color32(62, 40, 31, 255);
            }

            componentsImages[i].transform.GetChild(0).GetComponent<Image>().sprite = sp.fishingRod.components[i].appearance;
            componentsList[i].text = sp.fishingRod.components[i].type;

            componentsQuantityList[i].text = UIManager.instance.inventory.GetVariable(sp.fishingRod.components[i].ID) + "/" + sp.fishingRod.componentsQty[i].ToString();

            componentsImages[i].gameObject.SetActive(true);
            componentsList[i].gameObject.SetActive(true);
            componentsQuantityList[i].gameObject.SetActive(true);
        }

        for (int i = 3; i >= sp.fishingRod.components.Length; i--)
        {
            componentsImages[i].gameObject.SetActive(false);
            componentsList[i].gameObject.SetActive(false);
            componentsQuantityList[i].gameObject.SetActive(false);
        }

        title.text = sp.fishingRod.itemName;
        image.sprite = sp.fishingRod.appearance;


        if (sp.fishingRod.strength != 0)
        {
            tempoString = "+" + sp.fishingRod.strength.ToString() + " strength\n";
        }
        if (sp.fishingRod.constitution != 0)
        {
            tempoString += "+" + sp.fishingRod.constitution.ToString() + " constitution\n";
        }
        if (sp.fishingRod.dexterity != 0)
        {
            tempoString += "+" + sp.fishingRod.dexterity.ToString() + " dexterity\n";
        }
        if (sp.fishingRod.intelligence != 0)
        {
            tempoString += "+" + sp.fishingRod.intelligence.ToString() + " intelligence\n";
        }

        bonusStats.text = tempoString;
        description.text = sp.fishingRod.description;
    }

    public void UpdateTextGem(ScriptablePointer sp)
    {
        for (int i = 0; i < sp.gem.components.Length; i++)
        {
            if (UIManager.instance.inventory.GetVariable(sp.gem.components[i].ID) < sp.gem.componentsQty[i])
            {
                componentsList[i].color = new Color32(255, 0, 56, 255);
                componentsQuantityList[i].color = new Color32(255, 0, 56, 255);
            }
            else
            {
                componentsList[i].color = new Color32(62, 40, 31, 255);
                componentsQuantityList[i].color = new Color32(62, 40, 31, 255);
            }

            componentsImages[i].transform.GetChild(0).GetComponent<Image>().sprite = sp.gem.components[i].appearance;
            componentsList[i].text = sp.gem.components[i].type;

            componentsQuantityList[i].text = UIManager.instance.inventory.GetVariable(sp.gem.components[i].ID) + "/" + sp.gem.componentsQty[i].ToString();

            componentsImages[i].gameObject.SetActive(true);
            componentsList[i].gameObject.SetActive(true);
            componentsQuantityList[i].gameObject.SetActive(true);
        }

        for(int i = 3; i >= sp.gem.components.Length; i--)
        {
            componentsImages[i].gameObject.SetActive(false);
            componentsList[i].gameObject.SetActive(false);
            componentsQuantityList[i].gameObject.SetActive(false);
        }

        title.text = sp.gem.gemName;
        image.sprite = sp.gem.appearance;

        bonusStats.text = sp.gem.stats;
        description.text = sp.gem.description;
    }

    public void CraftObject(ScriptablePointer sp)
    {
        if (sp.armor != null)
        {
            for (int i = 0; i < sp.armor.components.Length; i++)
            {
                if (UIManager.instance.inventory.GetVariable(sp.armor.components[i].ID) < sp.armor.componentsQty[i])
                {
                    canCraft = false;
                }
            }

            if (canCraft)
            {
                for (int i = 0; i < sp.armor.components.Length; i++)
                {
                    UIManager.instance.inventory.RemoveQty(sp.armor.components[i].ID, sp.armor.componentsQty[i]);
                }

                UIManager.instance.inventory.SetArmor(sp.armor.ID);
                UpdateText(sp);
            }
        }
        else if(sp.fishingRod != null)
        {
            for (int i = 0; i < sp.fishingRod.components.Length; i++)
            {
                if (UIManager.instance.inventory.GetVariable(sp.fishingRod.components[i].ID) < sp.fishingRod.componentsQty[i])
                {
                    canCraft = false;
                }
            }

            if (canCraft)
            {
                for (int i = 0; i < sp.fishingRod.components.Length; i++)
                {
                    UIManager.instance.inventory.RemoveQty(sp.fishingRod.components[i].ID, sp.fishingRod.componentsQty[i]);
                }

                UIManager.instance.inventory.SetArmor(sp.fishingRod.ID);
                UpdateTextRod(sp);
            }
        }
        else if(sp.gem != null)
        {
            for (int i = 0; i < sp.gem.components.Length; i++)
            {
                if (UIManager.instance.inventory.GetVariable(sp.gem.components[i].ID) < sp.gem.componentsQty[i])
                {
                    canCraft = false;
                }
            }

            if (canCraft)
            {
                for (int i = 0; i < sp.gem.components.Length; i++)
                {
                    UIManager.instance.inventory.RemoveQty(sp.gem.components[i].ID, sp.gem.componentsQty[i]);
                }

                UIManager.instance.inventory.SetArmor(sp.gem.ID);
                UpdateTextGem(sp);
            }
        }
    }
}
