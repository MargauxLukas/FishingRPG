using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArtisanManager : MonoBehaviour
{
    public GameObject firstSelectedTab;
    GameObject currentSelectedTab;

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

        if (Input.GetButtonDown("A Button"))
        {
            Debug.Log("Creating Object");
            //CreateObject
            //Delete components
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
}
