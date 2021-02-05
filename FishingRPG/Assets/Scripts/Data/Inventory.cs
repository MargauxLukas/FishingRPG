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
    public int PET_C; 
    public int PES_C;
    public int PEF_R;
    public int PEI_E; 
    public int PEH_L; 

    //Pequessivo Armor
    public bool PEH_1;
    public bool PEP_1;
    public bool PEBE_1;
    public bool PEB_1;

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
    public bool PEFI_1;
    public bool REFI_1;

    //Gems
    public bool GPE_1;
    public bool GRE_1;

    public int GetVariable(string id)
    {
        switch(id)
        {
            //SnapSnackComposants
            case "PET_C":
                return PET_C;
            case "PES_C":
                return PES_C;
            case "PEF_R":
                return PEF_R;
            case "PEI_E":
                return PEI_E;
            case "PEH_L":
                return PEH_L;
            
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
            case "PET_C":
                PET_C -= qty;
                break;
            case "PES_C":
                PES_C -= qty;
                break;
            case "PEF_R":
                PEF_R -= qty;
                break;
            case "PEI_E":
                PEI_E -= qty;
                break;
            case "PEH_L":
                PEH_L -= qty;
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

        
    }

    public void SetArmor(string id)
    {
        switch (id)
        {
            //SnapSnack Armor
            case "PEH_1":
                PEH_1 = true;
                break;
            case "PEP_1":
                PEP_1 = true;
                break;
            case "PEBE_1":
                PEBE_1 = true;
                break;
            case "PEB_1":
                PEB_1 = true;
                break;
            case "PEFI_1":
                PEFI_1 = true;
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
            case "PET_C":
                PET_C++;
                break;
            case "PES_C":
                PES_C++;
                break;
            case "PEF_R":
                PEF_R++;
                break;
            case "PEI_E":
                PEI_E++;
                break;
            case "PEH_L":
                PEH_L++;
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

            case "REH_1":
                if(REH_1)
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
            case "PEP_1":
                if (PEP_1)
                {
                    return true;
                }
                return false;

            case "REP_1":
                if (REP_1)
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
            case "PEBE_1":
                if (PEBE_1)
                {
                    return true;
                }
                return false;

            case "HEBE_1":
                if (HEBE_1)
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
            case "PEB_1":
                if (PEB_1)
                {
                    return true;
                }
                return false;

            case "REB_1":
                if (REB_1)
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
            case "PEFI_1":
                if (PEFI_1)
                {
                    return true;
                }
                return false;

            case "REFI_1":
                if (REFI_1)
                {
                    return true;
                }
                return false;
        }
        return false;
    }

    public bool CheckGems(string id) //ajouter le RC (pas encore dispo)
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
