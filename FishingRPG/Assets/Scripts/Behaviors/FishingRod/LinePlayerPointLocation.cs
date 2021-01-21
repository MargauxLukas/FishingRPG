using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePlayerPointLocation : MonoBehaviour
{
    public Transform upPoint;      //Point le plus haut (Fishing Rod Point for PlayerPoint)
    public Transform downPoint;    //Point le plus bas (Check Water Level Point)
    public Transform FishingRodPointC;

    private float distance;
    private float r;

    private float playerPointY;

    private void Update()
    {
        r = UtilitiesManager.instance.CalculateR();

        if (r < 0)
        {
            r = 0f;
        }

        playerPointY = upPoint.position.y * (1 - r) + downPoint.position.y * r;

        gameObject.transform.position = new Vector3(FishingRodPointC.transform.position.x, playerPointY, FishingRodPointC.transform.position.z);
    }
}
