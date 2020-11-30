using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject firstSelectedButton;

    public Sprite[] itemsList;

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
        Debug.Log("Bite");
    } 

    public void ButcherFish(Button b)
    {
        Transform itemsGroup = b.transform.GetChild(0);

        for (int i = 0; i < itemsGroup.childCount - 1; i++)
        {
            Image currentItem = itemsGroup.GetChild(i).gameObject.GetComponent<Image>();
            currentItem.sprite = null;
            currentItem.color = new Color32(255, 255, 255, 0);
        }

        int craftedItems = Random.Range(2, 6);

        for (int i = 0; i < craftedItems; i++)
        {
            Image currentItem = itemsGroup.GetChild(i).gameObject.GetComponent<Image>();
            Color itemColor = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);

            currentItem.sprite = itemsList[Random.Range(0, itemsList.Length - 1)];
            currentItem.color = itemColor;
        }
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
