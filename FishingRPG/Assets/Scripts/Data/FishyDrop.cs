using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFishyDrop", menuName = "BFF Tools/Fishy Drop", order = 10)]
public class FishyDrop : ScriptableObject
{
    public string ID = "ELI_C";
    public Sprite appearance;
    public string type = "Écaille Luisante";
    public string rarity = "Common";
    public float dropRate = 45;
}
