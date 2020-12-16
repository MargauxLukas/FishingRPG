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
    public List<ArmorSet> helmetList = new List<ArmorSet>();
    public List<ArmorSet> pauldronsList = new List<ArmorSet>();
    public List<ArmorSet> beltList = new List<ArmorSet>();
    public List<ArmorSet> bootsList = new List<ArmorSet>();
    public List<FishingRod> fishingRodList = new List<FishingRod>();
    public List<Gem> gemList = new List<Gem>();
    public Inventory inventory;

    private void Awake()
    {
        Init();
        DontDestroyOnLoad(this.gameObject);
    }

    public virtual void Init()
    {
        instance = this;
    }

    private void Start()
    {
        ResetSelectedButton(firstSelectedButton);
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

    public void SetButcherSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
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
