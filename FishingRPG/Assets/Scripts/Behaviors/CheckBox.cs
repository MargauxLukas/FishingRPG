using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    [System.NonSerialized]
    public bool isNearChest = false;

    public LayerMask chestMask;                                                         
    public Transform chestCheck;                                                        
    public float chestDistance = 0.5f;                                                  

    private void Update()
    {
        isNearChest = Physics.CheckSphere(chestCheck.position, chestDistance, chestMask);
    }
}
