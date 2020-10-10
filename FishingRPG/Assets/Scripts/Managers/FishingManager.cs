using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager instance;
    public bool isBobberOnWater = false;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }


}
