using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGem : MonoBehaviour
{
    public void PlayGem(Gem gem)
    {
        switch (gem.name)
        {
            case "FirstGem":
                GemManager.instance.firstGem.Play(gem, FishingRodManager.instance.slot1.mat);
                break;
            case "gem2":
                
                break;
            case "gem3":
                
                break;
        }
    }
}
