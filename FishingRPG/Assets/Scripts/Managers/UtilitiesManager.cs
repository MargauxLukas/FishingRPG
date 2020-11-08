using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilitiesManager : MonoBehaviour
{
    public static UtilitiesManager instance;

    public float getVplBalancing;
    public float getVpjBalancing;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    //Vitesse du poisson quand il est libre
    public float GetVpL(float fishSpeed)
    {
        return fishSpeed * getVplBalancing;
    }
}
