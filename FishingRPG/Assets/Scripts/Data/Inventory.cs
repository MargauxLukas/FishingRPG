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
    public int SPS;
    public int REC_1;

    //Equiped
    public ArmorSet equipedHelmet;
    public ArmorSet equipedShoulders;
    public ArmorSet equipedBelt;
    public ArmorSet equipedBoots;
    public FishingRod equipedFishingRod;
    public Gem equipedGem1;
    public Gem equipedGem2;
    public Gem equipedGem3;

    [Header("SnapSnack Mats & Armor")]
    //SnapSnack Mats
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

    [Header("ReefCrusher Mats & Armor")]
    //ReefCrusher Mats
    public int RET_C;
    public int RES_C;
    public int REF_R;
    public int RET_R;
    public int REI_E;
    public int REH_L;

    //ReefCrusher armor
    public bool REH_1;
    public bool REP_1;
    public bool HEBE_1;
    public bool REB_1;

    //Fishing rod
    public bool PFR_1;
    public bool REFI_1;

    //Gems
    public bool GPE_1;
    public bool GRE_1;

    public int GetVariable(string id)
    {
        switch(id)
        {
            //SnapSnackComposants
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
            
            //ReefCrusherComposants
            case "RET_C":
                return RET_C;
            case "RES_C":
                return RES_C;
            case "REF_R":
                return REF_R;
            case "RET_R":
                return RET_R;
            case "REI_E":
                return REI_E;
            case "REH_L":
                return REH_L;
        }

        return 0;
    }

    public void RemoveQty(string id, int qty)
    {
        switch (id)
        {
            //SnapSnackComposants
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

            //ReefCrusherComposants
            case "RET_C":
                RET_C -= qty;
                break;
            case "RES_C":
                RES_C -= qty;
                break;
            case "REF_R":
                REF_R -= qty;
                break;
            case "RET_R":
                RET_R -= qty;
                break;
            case "REI_E":
                REI_E -= qty;
                break;
            case "REH_L":
                REH_L -= qty;
                break;
        }

        fishTotal--;
    }

    public void SetArmor(string id)
    {
        switch (id)
        {
            //SnapSnack Armor
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

            //ReefCrusher Armor
            case "REH_1":
                REH_1 = true;
                break;
            case "REP_1":
                REP_1 = true;
                break;
            case "HEBE_1":
                HEBE_1 = true;
                break;
            case "REB_1":
                REB_1 = true;
                break;
            case "REFI_1":
                REFI_1 = true;
                break;
            case "GRE_1":
                GRE_1 = true;
                break;
        }
    }

    public string GetFish()
    {
        if(SPS > 0)
        {
            SPS--;
            fishTotal--;
            currentFishOnMe--;
            return "SPS";
        }
        else if(REC_1 > 0)
        {
            REC_1--;
            fishTotal--;
            currentFishOnMe--;
            return "REC_1";
        }

        return null;
    }

    public void AddLoot(string id)
    {
        switch (id)
        {
            //SnapSnack Mat
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

            //ReefCrusher Mat
            case "RET_C":
                RET_C ++;
                break;
            case "RES_C":
                RES_C++;
                break;
            case "REF_R":
                REF_R++;
                break;
            case "RET_R":
                RET_R++;
                break;
            case "REI_E":
                REI_E++;
                break;
            case "REH_L":
                REH_L++;
                break;



            case "Empty":
                break;

            
        }
    }

    public bool CheckHelmet(string id) //ajouter le RC
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

    public bool CheckPauldrons(string id) //ajouter le RC
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

    public bool CheckBelt(string id) //ajouter le RC
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

    public bool CheckBoots(string id) //ajouter le RC
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

    public bool CheckFishingRod(string id) //ajouter le RC
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

    public bool CheckGems(string id) //ajouter le RC
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
