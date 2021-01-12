using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWaterLevel : MonoBehaviour
{
    //Lorsque Collision, stop chercher collision jusqu'à que Bobber Back
    public bool waterDetected;
    public bool canStartTheDetection = false;

    private Vector3 basePosition;

    public LayerMask waterMask;                                                         //Mask de detection
    public Transform waterCheck;                                                        //Position du Bobber
    public float waterDistance = 0.1f;                                                  //Rayon de la sphere de detection

    private void Start()
    {
        basePosition = waterCheck.transform.localPosition;
    }

    private void FixedUpdate()
    {
        if(canStartTheDetection)
        {
            waterCheck.transform.Translate(-Vector3.up * Time.fixedDeltaTime * 4f);

            waterDetected = Physics.CheckSphere(waterCheck.position, waterDistance, waterMask);

            if (waterDetected)
            {
                canStartTheDetection = false;
            }
        }
    }

    public void ResetPosition()
    {
        waterCheck.transform.localPosition = basePosition;
    }
}
