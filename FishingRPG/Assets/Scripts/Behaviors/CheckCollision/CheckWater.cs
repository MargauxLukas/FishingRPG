using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWater : MonoBehaviour
{
    public bool isWater = false;
    public bool isFloor = false;

    public LayerMask waterMask;                                                         //Mask de detection
    public Transform waterCheck;                                                        //Position du Bobber
    public float waterDistance = 0.4f;                                                  //Rayon de la sphere de detection

    public LayerMask floorMask;
    public Transform floorCheck;
    public float floorDistance = 0.4f;

    public bool justOneTime = false;

    //Lorsque Collision, stop chercher collision jusqu'à que Bobber Back
    private void FixedUpdate()
    {
        FishingManager.instance.isOnWater = isWater = Physics.CheckSphere(waterCheck.position, waterDistance, waterMask);
        isFloor = Physics.CheckSphere(floorCheck.position, floorDistance, floorMask);
        

        if (isFloor || isWater)
        {
            FishingRodManager.instance.bobber.GetComponent<Bobber>().StopMovement();
            
        }
    }
}
