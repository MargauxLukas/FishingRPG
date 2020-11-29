using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFishyDrop", menuName = "BFF Tools/Fishy Drop", order = 10)]
public class FishyDrop : ScriptableObject
{
    public string ID;
    public Sprite appearance;
    public string type;
    public string rarity;
    public float dropRate;
}
