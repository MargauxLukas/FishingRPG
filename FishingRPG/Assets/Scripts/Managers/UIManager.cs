using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] itemsList;
    public void LeaveVillage(Text txt)
    {
        txt.text = "Leaving village...";
        StartCoroutine(TextReset(txt));
    }

    public void OpenMenu(GameObject go)
    {
        go.SetActive(true);
    }

    public void CloseMenu(GameObject go)
    {
        go.SetActive(false);
    }

    IEnumerator TextReset(Text txt)
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Reset text");
        txt.text = null;
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
}
