using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GearingManager : MonoBehaviour
{
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
}
