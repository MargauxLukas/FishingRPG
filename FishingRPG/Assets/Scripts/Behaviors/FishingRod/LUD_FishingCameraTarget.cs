using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUD_FishingCameraTarget : MonoBehaviour
{
    public Transform CPoint;
    public Transform FishTransform;
    public Transform Point3;


    public float mulCPoint;
    public float mulFishT;
    public float mulPoint3;


    Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        targetPosition = (mulCPoint*CPoint.position + mulFishT * FishTransform.position + mulPoint3 * Point3.position) / (mulCPoint+ mulFishT+mulPoint3);

        transform.position = targetPosition;
    }
}
