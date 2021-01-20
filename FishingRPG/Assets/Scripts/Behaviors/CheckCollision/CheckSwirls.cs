using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSwirls : MonoBehaviour
{
    public LayerMask swirlMask;
    public Transform swirlCheck;
    public float swirlDistance = 0.1f;
    //Lorsque Collision, stop chercher collision jusqu'à que Bobber Back
    private void FixedUpdate()
    {
        FishingManager.instance.isOnSwirl = Physics.CheckSphere(swirlCheck.position, swirlDistance, swirlMask);
    }
}
