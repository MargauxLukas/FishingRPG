using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerStats : MonoBehaviour
{
    [Header("Primary Stats")]
    public int strenght;
    public int constitution;
    public int dexterity;
    public int intelligence;

    [Header("Move speed")]
    public float speedMovement;
    public float speedMovementWhileFishing;

    [Header("Distance With Water")]
    public float distanceMax;
    public float currentDistance;           //Valeur qui change souvent par rapport aux autres, mettre ailleurs

    public float fishRewindSpeed;           //A mettre ailleurs sûrement
}
