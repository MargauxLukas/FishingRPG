using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject firstSelectedButton;

    public List<FishyFiche> fishyFishList = new List<FishyFiche>();
    public Inventory inventory;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    private void Start()
    {
        ResetSelectedButton(firstSelectedButton);
    }

    private void Update()
    {
        Debug.Log(firstSelectedButton.name);
    }

    public void LeaveVillage(Text txt)
    {
        txt.text = "Leaving village...";
        StartCoroutine(TextReset(txt));
    }

    public void OpenMenu(GameObject go)
    {
        go.SetActive(true);
        firstSelectedButton = EventSystem.current.currentSelectedGameObject;
        ResetSelectedButton(firstSelectedButton);
    }

    public void CloseMenu(GameObject go)
    {
        go.SetActive(false);
        ResetSelectedButton(firstSelectedButton);
    }

    public void ResetSelectedButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);
    }

    public void SetFirstSelected(GameObject _go)
    {
        EventSystem.current.SetSelectedGameObject(_go);
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator TextReset(Text txt)
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Reset text");
        txt.text = null;
    }
}
