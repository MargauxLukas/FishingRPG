using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewInventory", menuName = "BFF Tools/Inventory", order = 10)]
public class Inventory : ScriptableObject
{
    public int fishTotal;
    public int PQS_1;
    public int PFS_1;

    public int PST_C;
    public int PSC_C;
    public int PFL_R;
    public int PFI_E;
    public int PBH_L;

    public bool PEH_1;
    public bool PCH_1;
    public bool PBE_1;
    public bool PBO_1;
    public bool PFR_1;

    public bool GPE_1;

    public int GetVariable(string id)
    {
        switch(id)
        {
            case "PST_C":
                return PST_C;
            case "PSC_C":
                return PSC_C;
            case "PFL_R":
                return PFL_R;
            case "PFI_E":
                return PFI_E;
            case "PBH_L":
                return PBH_L;
        }

        return 0;
    }

    public void RemoveQty(string id, int qty)
    {
        switch (id)
        {
            case "PST_C":
                PST_C -= qty;
                break;
            case "PSC_C":
                PSC_C -= qty;
                break;
            case "PFL_R":
                PFL_R -= qty;
                break;
            case "PFI_E":
                PFI_E -= qty;
                break;
            case "PBH_L":
                PBH_L -= qty;
                break;
        }

        fishTotal--;
    }

    public void SetArmor(string id)
    {
        switch (id)
        {
            case "PEH_1":
                PEH_1 = true;
                break;
            case "PCH_1":
                PCH_1 = true;
                break;
            case "PBE_1":
                PBE_1 = true;
                break;
            case "PBO_1":
                PBO_1 = true;
                break;
            case "PFR_1":
                PFR_1 = true;
                break;
        }
    }

    public string GetFish()
    {
        if(PQS_1 > 0)
        {
            return "PQS_1";
        }

        return null;
    }

    public void AddLoot(string id)
    {
        switch (id)
        {
            case "PST_C":
                PST_C++;
                break;
            case "PSC_C":
                PSC_C++;
                break;
            case "PFL_R":
                PFL_R++;
                break;
            case "PFI_E":
                PFI_E++;
                break;
            case "PBH_L":
                PBH_L++;
                break;
            case "Empty":
                break;
        }
    }
}
