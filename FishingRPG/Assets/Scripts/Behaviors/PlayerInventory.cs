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
            case "PQS_1":
                inventory.PQS_1++;
                break;
            default:
                Debug.Log("Don't find any case with id :" + id);
                break;
        }
    }
}
