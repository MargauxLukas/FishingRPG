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

       /*
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0; // VSync must be disabled.
        Application.targetFrameRate = 10;
#endif
       */
     
    }

    public virtual void Init()
    {
        instance = this;
    }
}
