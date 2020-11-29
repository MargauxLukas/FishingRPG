using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FeatureType { Int, Float, Curve }

[CreateAssetMenu(fileName = "NewFishyPattern", menuName = "BFF Tools/Fishy Pattern", order = 10)]
public class FishyPattern : ScriptableObject
{
    //Base variables
    public int priority;
    public float duration;
    public float energyCost;
    public bool costEnergyOverTime;
    public float DOTFrequency;
    public bool triggerRageMode;

    //Added features
    public string featureName;
    public FeatureType featureType;
}
