using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArtisanManager : MonoBehaviour
{
    EventSystem _event;

    GameObject firstSelected;

    void Start()
    {
        _event = EventSystemPointer.instance.gameObject.GetComponent<EventSystem>();
        _event.firstSelectedGameObject = firstSelected;
    }

    void Update()
    {
        if(Input.GetKey("Left Bumper"))
        {
            GameObject nextSelected = EventSystem.current.currentSelectedGameObject.GetComponent<Button>().navigation.selectOnLeft.gameObject;
            _event.SetSelectedGameObject(nextSelected);
        }

        if (Input.GetKey("Right Bumper"))
        {
            GameObject nextSelected = EventSystem.current.currentSelectedGameObject.GetComponent<Button>().navigation.selectOnRight.gameObject;
            _event.SetSelectedGameObject(nextSelected);
        }

        if (Input.GetKey("Vertical"))
        {
            GameObject nextSelected = EventSystem.current.currentSelectedGameObject.GetComponent<FirstSelectedChild>().gameObject;
            _event.SetSelectedGameObject(nextSelected);
        }
    }
}
