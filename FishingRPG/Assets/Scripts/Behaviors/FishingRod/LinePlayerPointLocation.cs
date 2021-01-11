using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePlayerPointLocation : MonoBehaviour
{
    public Transform upPoint;      //Point le plus haut (Fishing Rod Point for PlayerPoint)
    public Transform downPoint;    //Point le plus bas (Check Water Level Point)

    private float distance;
    private float r;

    private float playerPointX;
    private float playerPointZ;

    private void Update()
    {
        r = UtilitiesManager.instance.CalculateR();

        playerPointX = upPoint.position.x + (downPoint.position.x - upPoint.position.x) * r;
        playerPointZ = upPoint.position.z + (downPoint.position.z - upPoint.position.z) * r;
    }

    public void CheckDistance()
    {

    }
}
