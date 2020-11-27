using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFishingRodUpgrade", menuName = "BFF Tools/Fishing Rod Upgrade", order = 10)]
public class FishingRodUpgrade : ScriptableObject
{
    public string ID;
    public int upgradeState;
    public string itemName;
    public int tier;
    public int gemPosition;

    public int strength;
    public float constitution;
    public float dexterity;
    public float intelligence;

    public FishyDrop[] components;
    public int[] componentsQty;
}
