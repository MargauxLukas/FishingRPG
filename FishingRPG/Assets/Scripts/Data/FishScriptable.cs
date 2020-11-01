using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScriptable: ScriptableObject
{
    public int maxStammina;
    public int currentStamina;
    public int maxLife;
    public int currentLife;
    public enum length { little = 1, medium = 2, large = 3 }
    public int weight;
    public int strenght;
    public int speed;
    public int magicRes;
    public bool isExtenued;
    public bool isAerial;
}
