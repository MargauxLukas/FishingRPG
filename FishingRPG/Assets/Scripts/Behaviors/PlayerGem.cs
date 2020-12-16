using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGem : MonoBehaviour
{
    public void PlayGem(Gem gem)
    {
        switch (gem.ID)
        {
            case "GPE_1":
                GemManager.instance.firstGem.Play(gem);
                break;
            case "gem2":
                
                break;
            case "gem3":
                
                break;
        }
    }
}
