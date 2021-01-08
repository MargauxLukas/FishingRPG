using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArmorSet", menuName = "BFF Tools/Armor Set", order = 10)]
public class ArmorSet : ScriptableObject
{
    public string ID;
    public Sprite appearance;
    public string itemType;
    public int upgradeState;
    public string itemName;
    public int tier;

    public int strength;
    public int constitution;
    public int dexterity;
    public int intelligence;

    public string description;

    public FishyDrop[] components;
    public int[] componentsQty;
}
