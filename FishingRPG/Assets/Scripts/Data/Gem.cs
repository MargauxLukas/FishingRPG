using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewGem", menuName = "BFF Tools/Gem", order = 10)]
public class Gem : ScriptableObject
{
    public string ID;
    public string gemName;
    public Sprite appearance;
    //stats
    public int duration;
    public int cooldown;
    public FishyDrop[] components;
}
