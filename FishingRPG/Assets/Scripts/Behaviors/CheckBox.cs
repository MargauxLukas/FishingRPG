using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public GameObject aButton;

    [System.NonSerialized]
    public bool isNearChest = false;

    public LayerMask chestMask;                                                         
    public Transform chestCheck;                                                        
    public float chestDistance = 0.5f;                                                  

    private void Update()
    {
        isNearChest = Physics.CheckSphere(chestCheck.position, chestDistance, chestMask);

        if(isNearChest)
        {
            aButton.SetActive(true);
            aButton.transform.LookAt(new Vector3(PlayerManager.instance.player.transform.position.x, aButton.transform.position.y, PlayerManager.instance.player.transform.position.z));
        }
        else
        {
            aButton.SetActive(false);
        }
    }
}
