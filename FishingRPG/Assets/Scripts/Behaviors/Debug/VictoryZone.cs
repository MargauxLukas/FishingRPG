using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryZone : MonoBehaviour
{
    public bool DebugVictoryZone = false;
    public MeshRenderer victoryZoneMeshRenderer;

    public Material openMat;
    public Material closeMat;

    public void Start()
    {
        if(DebugVictoryZone)
        {
            victoryZoneMeshRenderer.enabled = true;
        }
    }

    public void ActivateZone()
    {
        victoryZoneMeshRenderer.material = openMat;
    }

    public void DesactivateZone()
    {
        victoryZoneMeshRenderer.material = closeMat;
    }
}
