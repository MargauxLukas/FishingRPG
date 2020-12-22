using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGem : MonoBehaviour
{
    public void PlayGem(Gem gem, int i)
    {
        switch (gem.ID)
        {
            case "GPE_1":
                GemManager.instance.firstGem.Play(gem, i);
                break;
            case "gem2":
                
                break;
            case "gem3":
                
                break;
        }
    }
}
