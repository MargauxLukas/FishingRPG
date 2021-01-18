using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePlayerPointLocation : MonoBehaviour
{
    public Transform upPoint;      //Point le plus haut (Fishing Rod Point for PlayerPoint)
    public Transform downPoint;    //Point le plus bas (Check Water Level Point)

    private float distance;
    private float r;

    private float playerPointY;

    private void Update()
    {
        r = UtilitiesManager.instance.CalculateR();

        //Debug.Log("Player Point t = " + r);

        playerPointY = downPoint.position.y * (1 - r) + upPoint.position.y * r;

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, playerPointY, gameObject.transform.position.z);
    }
}
