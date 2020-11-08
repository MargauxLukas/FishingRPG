using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilitiesManager : MonoBehaviour
{
    public static UtilitiesManager instance;

    public float getFishSpeedBalancing;


    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    //Vitesse du poisson : Agilité * q
    public float GetFishSpeed(float fishAgility)
    {
        return fishAgility * getFishSpeedBalancing;
    }
}
