using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUD_FishingCameraTarget : MonoBehaviour
{
    public Transform CPoint;    //RIG_Canne_Pequessivo_Tige01 (centre de la base de la canne)
    public Transform FishTransform; //Le Bobber


    public float mulCPoint = 3.63f;
    public float mulFishT = 1f ;


    Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        targetPosition = (mulCPoint*CPoint.position + mulFishT * FishTransform.position) / (mulCPoint+ mulFishT);

        transform.position = targetPosition;
    }
}
