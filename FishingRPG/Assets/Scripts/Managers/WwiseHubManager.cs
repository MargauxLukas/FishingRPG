using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseHubManager : MonoBehaviour
{

    //Wwise declaration
    public AK.Wwise.Bank MainSoundBank;
    public AK.Wwise.Event OnItemCrafted;


    public void Awake()

    {

        MainSoundBank.Load();

    }

}
