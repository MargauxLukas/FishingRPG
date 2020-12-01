using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public static DebugManager instance;

    public VictoryZone vz;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }
}
