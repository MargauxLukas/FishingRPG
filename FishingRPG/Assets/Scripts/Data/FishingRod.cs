using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFishingRod", menuName = "BFF Tools/Fishing Rod", order = 10)]
public class FishingRod : ScriptableObject
{
    public string ID;
    public Sprite appearance;
    public int upgradeState;
    public string itemName;
    public int tier;

    public int rodTension;
    public int gemSlots;

    public int strength;
    public int constitution;
    public int dexterity;
    public int intelligence;

    public FishyDrop[] components;
    public int[] componentsQty;
}
