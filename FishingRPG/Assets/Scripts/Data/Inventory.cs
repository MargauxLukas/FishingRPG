using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewInventory", menuName = "BFF Tools/Inventory", order = 10)]
public class Inventory : ScriptableObject
{
    public int fishTotal;
    public int currentFishOnMe;
    public int fishNumberOnStock;
    public int PQS_1;
    public int PFS_1;

    //Equiped
    public ArmorSet equipedHelmet;
    public ArmorSet equipedShoulders;
    public ArmorSet equipedBelt;
    public ArmorSet equipedBoots;
    public FishingRod equipedFishingRod;
    public Gem equipedGem1;
    public Gem equipedGem2;
    public Gem equipedGem3;

    //Pequessivo Mats
    public int PST_C;
    public int PSC_C;
    public int PFL_R;
    public int PFI_E;
    public int PBH_L;

    //Pequessivo Armor
    public bool PEH_1;
    public bool PCH_1;
    public bool PBE_1;
    public bool PBO_1;

    //??? armor
    public bool AEH_1;
    public bool ACH_1;
    public bool ABE_1;
    public bool ABO_1;
    public bool AFR_1;

    //Fishing rod
    public bool PFR_1;

    //Gems
    public bool GPE_1;

    public int GetVariable(string id)
    {
        switch(id)
        {
            case "PST_C":
                return PST_C;
            case "PSC_C":
                return PSC_C;
            case "PLF_R":
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
            case "PLF_R":
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
            case "GPE_1":
                GPE_1 = true;
                break;
        }
    }

    public string GetFish()
    {
        if(PQS_1 > 0)
        {
            PQS_1--;
            fishTotal--;
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
            case "PLF_R":
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

    public bool CheckHelmet(string id)
    {
        switch(id)
        {
            case "PEH_1":
                if(PEH_1)
                {
                    return true;
                }
                return false;
        }
        return false;
    }

    public bool CheckPauldrons(string id)
    {
        switch (id)
        {
            case "PCH_1":
                if (PCH_1)
                {
                    return true;
                }
                return false;
        }
        return false;
    }

    public bool CheckBelt(string id)
    {
        switch (id)
        {
            case "PBE_1":
                if (PBE_1)
                {
                    return true;
                }
                return false;
        }
        return false;
    }

    public bool CheckBoots(string id)
    {
        switch (id)
        {
            case "PBO_1":
                if (PBO_1)
                {
                    return true;
                }
                return false;
        }
        return false;
    }

    public bool CheckFishingRod(string id)
    {
        switch (id)
        {
            case "PFR_1":
                if (PFR_1)
                {
                    return true;
                }
                return false;
        }
        return false;
    }

    public bool CheckGems(string id)
    {
        switch(id)
        {
            case "GPE_1":
                if(GPE_1)
                {
                    return true;
                }
                return false;
        }
        return false;
    }
}
