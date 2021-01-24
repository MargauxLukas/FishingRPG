using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory inventory;

    public void AddThisFishToInventory(string id)
    {
        switch(id)
        {
            case "SPS":
                inventory.SPS++;
                break;
            case "REC_1":
                inventory.REC_1++;
                break;            
            default:
                Debug.Log("Don't find any case with id :" + id);
                break;
        }

        inventory.fishTotal++;
        inventory.currentFishOnMe++;
    }
}
