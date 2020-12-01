using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArtisanManager : MonoBehaviour
{
    EventSystem _event;

    GameObject firstSelectedTab;
    GameObject currentSelectedTab;

    void Start()
    {
        _event = EventSystemPointer.instance.gameObject.GetComponent<EventSystem>();
        firstSelectedTab = currentSelectedTab;
    }

    void Update()
    {
        if(Input.GetKey("Left Bumper"))
        {
            ResetTabColor(currentSelectedTab);
            currentSelectedTab = currentSelectedTab.GetComponent<TabNeighbours>().selectedOnLeft;
            SetSelectedTabColor(currentSelectedTab);
        }

        if (Input.GetKey("Right Bumper"))
        {
            GameObject nextSelected = EventSystem.current.currentSelectedGameObject.GetComponent<Button>().navigation.selectOnRight.gameObject;
            _event.SetSelectedGameObject(nextSelected);
        }

        if (Input.GetKey("Vertical"))
        {
            
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
}
