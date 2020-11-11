using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewFishyFiche", menuName = "BFF Tools/Fishy Fiche", order = 10)]
public class FishyFiche: ScriptableObject
{
    //Temporary values waiting for tsv import
    [Header("Fish Type")]
    public string ID = "PQS_0";
    public Sprite appearance;
    public string species = "Pequessivo";
    public int tier = 0;

    [Header("Fish Constitution")]
    public float life = 100;
    public float stamina = 100;

    [Header("Fish Stats")]
    public float strength = 10;
    public float weight = 5;
    public float agility = 2;
    public float magicResistance = 6;

    [Header("Butcher Drops")]
    public FishyDrop[] drops;

    [Header("Fish Patterns")]
    public MonoBehaviour[] patterns;
}
