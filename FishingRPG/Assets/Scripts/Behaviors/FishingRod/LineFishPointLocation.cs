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

        //Debug.Log("Fish Point t = " + r);

        fishPointX = nearPoint.position.x * (1 - r) + farPoint.position.x * r;
        fishPointZ = nearPoint.position.z * (1 - r) + farPoint.position.z * r;

        gameObject.transform.position = new Vector3(fishPointX, nearPoint.transform.position.y, fishPointZ);
    }
}
