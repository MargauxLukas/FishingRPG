using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFishyPattern", menuName = "BFF Tools/Fishy Pattern", order = 10)]
public class FishyPattern : ScriptableObject
{
    //Base variables
    public MonoScript script;
    public int priorityCalm;
    public int priorityRage;
    public float duration;
    public float energyCost;
    public bool costEnergyOverTime;
    public float DOTFrequency;
    public bool triggerRageMode;

    //Added features
    public string featureName;
    public enum FeatureType { Int, Float, Curve };
}
