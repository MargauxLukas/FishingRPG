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

    private void Update()
    {
        waterCheck.position = new Vector3(transform.position.x, waterCheck.position.y, transform.position.z);
    }

    public void ResetPosition()
    {
        waterCheck.transform.localPosition = basePosition;
    }

    public void SetPositionOnWater()
    {
        waterCheck.transform.position = new Vector3(waterCheck.transform.position.x, FishingRodManager.instance.bobber.transform.localPosition.y, waterCheck.transform.position.z);
    }
}
