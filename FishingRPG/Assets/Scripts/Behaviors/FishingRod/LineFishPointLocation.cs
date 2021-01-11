using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFishPointLocation : MonoBehaviour
{
    public Transform farPoint;     //Bobber
    public Transform nearPoint;    //Check Water Level Point

    private float r;
    private float fishPointX;
    private float fishPointZ;
    void Update()
    {
        r = UtilitiesManager.instance.CalculateR();

        fishPointX = farPoint.position.x + (farPoint.position.x - farPoint.position.x) * r;
        fishPointZ = farPoint.position.z + (farPoint.position.z - farPoint.position.z) * r;
    }
}
