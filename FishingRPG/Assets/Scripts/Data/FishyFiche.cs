using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FishyFiche: ScriptableObject
{
    [Header("Fish Type")]
    public Sprite appearance;
    public string species;
    public int tier;

    [Header("Fish Constitution")]
    public float life;
    public float energy;

    [Header("Fish Stats")]
    public float strength;
    public float weight;
    public float speed;
    public float magicResistance;
}
