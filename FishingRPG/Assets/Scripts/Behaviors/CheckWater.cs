using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWater : MonoBehaviour
{
    [System.NonSerialized] public bool isWater = false;
    [System.NonSerialized] public bool isFloor = false;

    public LayerMask waterMask;                                                         //Mask de detection
    public Transform waterCheck;                                                        //Position du Bobber
    public float waterDistance = 0.4f;                                                  //Rayon de la sphere de detection

    public LayerMask floorMask;
    public Transform floorCheck;
    public float floorDistance = 0.4f;

    private void Update()
    {
        FishingManager.instance.isOnWater = isWater = Physics.CheckSphere(waterCheck.position, waterDistance, waterMask);
        isFloor = Physics.CheckSphere(floorCheck.position, floorDistance, floorMask);

        if(isWater || isFloor)
        {
            FishingRodManager.instance.bobber.GetComponent<Bobber>().StopMovement();
        }
    }
}
