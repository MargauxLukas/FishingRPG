using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArmorSet", menuName = "BFF Tools/Armor Set", order = 10)]
public class ArmorSet : ScriptableObject
{
    public string ID;
    public enum ItemType { Head, Shoulders, Waist, Boots };
    public int upgradeState;
    public string itemName;
    public int tier;

    public int strength;
    public float constitution;
    public float dexterity;
    public float intelligence;

    public FishyDrop[] components;
    public int[] componentsQty;
}
