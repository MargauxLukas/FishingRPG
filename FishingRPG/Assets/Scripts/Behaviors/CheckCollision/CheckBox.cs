using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public GameObject aButtonChest;

    [System.NonSerialized] public bool isNearChest = false;

    public LayerMask chestMask;                                                         
    public Transform chestCheck;                                                        
    public float chestDistance = 0.5f;                                                  

    private void FixedUpdate()
    {
        isNearChest = Physics.CheckSphere(chestCheck.position, chestDistance, chestMask);

        if (isNearChest)
        {
            aButtonChest.SetActive(true);
            aButtonChest.transform.LookAt(new Vector3(PlayerManager.instance.player.transform.position.x, aButtonChest.transform.position.y, PlayerManager.instance.player.transform.position.z));
        }
        else
        {
            aButtonChest.SetActive(false);
        }
    }
}
