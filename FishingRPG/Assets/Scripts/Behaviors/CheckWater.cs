using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWater : MonoBehaviour
{
    [System.NonSerialized]
    public bool isWater = false;

    public LayerMask waterMask;                                                         //Mask de detection
    public Transform waterCheck;                                                        //Position du Bobber
    public float waterDistance = 0.4f;                                                  //Rayon de la sphere de detection

    private void Update()
    {
        isWater = Physics.CheckSphere(waterCheck.position, waterDistance, waterMask);  
    }
}
