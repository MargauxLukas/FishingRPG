using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseHubManager : MonoBehaviour
{

    //Wwise declaration
    // public AK.Wwise.Bank MainSoundBank;
    // public AK.Wwise.Event OnItemCrafted;

    // General UI Sounds
    public GameObject gameCamera;
    private void Start()
    {
    }
    public void OnMovingCursor()
    {
        AkSoundEngine.PostEvent("OnCursorMove", gameCamera);
    }
   public void OnSelectingCursor()
    {
        AkSoundEngine.PostEvent("OnCursorSelect", gameCamera);
    }

}
