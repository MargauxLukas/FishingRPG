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

        if(r < 0)
        {
            r = 0f;
        }

        
        if (r > 1)
        {
            r = 1f;
        }

        Debug.Log("FP : " + r);

        fishPointX = farPoint.position.x * (1 - r) + nearPoint.position.x * r;
        fishPointZ = farPoint.position.z * (1 - r) + nearPoint.position.z * r;

        gameObject.transform.position = new Vector3(fishPointX, farPoint.transform.position.y, fishPointZ);
    }
}
