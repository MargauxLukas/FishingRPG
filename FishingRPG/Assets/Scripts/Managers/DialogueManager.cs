using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public ArtisanDialogue artisanDialogue;
    public Text artisanText;
    public ButcherDialogue butcherDialogue;
    public Text butcherText;
    public HouseDialogue houseDialogue;
    public TutoDialogue tutoDialogue;

    public Sprite artisanSprite;
    public Sprite butcherSprite;
    public Sprite mainCharacterSprite;

    private void Awake()
    {
        instance = this;
    }

    public void EnterArtisan()
    {
        artisanText.text = artisanDialogue.enterArtisanShopDialogue[Random.Range(0, artisanDialogue.enterArtisanShopDialogue.Length)];
        Debug.Log(artisanDialogue.enterArtisanShopDialogue[0]);
        Debug.Log(artisanDialogue.enterArtisanShopDialogue[1]);
    }

    public void LeaveArtisan()
    {
        artisanText.text = artisanDialogue.leaveArtisanShopDialogue[Random.Range(0, artisanDialogue.leaveArtisanShopDialogue.Length)];
    }

    public void EnterButcher()
    {
        butcherText.text = butcherDialogue.enterButcherShopDialogue[Random.Range(0, butcherDialogue.enterButcherShopDialogue.Length)];
    }

    public void LeaveButcher()
    {
        butcherText.text = butcherDialogue.leaveButcherShopDialogue[Random.Range(0, butcherDialogue.leaveButcherShopDialogue.Length)];
    }

    IEnumerator HideText()
    {
        yield return new WaitForSeconds(1f);
    }
}
