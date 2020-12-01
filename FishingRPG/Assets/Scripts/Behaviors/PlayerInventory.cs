using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int pequessivoQty = 0;

    public void AddThisFishToInventory(string id)
    {
        switch(id)
        {
            case "PQS_1":
                pequessivoQty++;
                break;
            default:
                Debug.Log("Don't find any case with id :" + id);
                break;
        }
    }
}
