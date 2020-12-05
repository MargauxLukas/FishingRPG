using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HouseManager : MonoBehaviour
{
    public GameObject firstSelected;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    void Update()
    {
        if (Input.GetButtonDown("B Button"))
        {
            UIManager.instance.CloseMenu(gameObject);
        }
    }

    public void SelectedColor(Text _txt)
    {
        _txt.color = new Color32(176, 142, 66, 255);
    }

    public void DeselectedColor(Text _txt)
    {
        _txt.color = new Color32(39, 75, 94, 255);
    }

    public void OpenMenu(GameObject _go)
    {
        _go.SetActive(true);
    }

    public void CloseMenu(GameObject _go)
    {
        _go.SetActive(false);
    }

    public void SetFirstSelected(GameObject _go)
    {
        EventSystem.current.SetSelectedGameObject(_go);
    }

    public void Quit()
    {
        Application.Quit();
    }
}