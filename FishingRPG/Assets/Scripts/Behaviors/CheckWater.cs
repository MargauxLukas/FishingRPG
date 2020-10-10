using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWater : MonoBehaviour
{
    public bool isWater = false;

    public Transform waterCheck;
    public float waterDistance = 0.4f;
    public LayerMask waterMask;

    private void Update()
    {
        isWater = Physics.CheckSphere(waterCheck.position, waterDistance, waterMask);
    }
}
