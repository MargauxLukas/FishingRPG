using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHub : MonoBehaviour
{
    public GameObject aButtonFishingStock;

    [System.NonSerialized] public bool isNearFishingStock = false;

    public LayerMask fishingStockMask;
    public Transform fishingStockCheck;
    public float fishingStockDistance = 0.5f;

    private void FixedUpdate()
    {
        isNearFishingStock = Physics.CheckSphere(fishingStockCheck.position, fishingStockDistance, fishingStockMask);

        if (isNearFishingStock)
        {
            aButtonFishingStock.SetActive(true);
            aButtonFishingStock.transform.LookAt(new Vector3(PlayerManager.instance.player.transform.position.x, aButtonFishingStock.transform.position.y, PlayerManager.instance.player.transform.position.z));
        }
        else
        {
            aButtonFishingStock.SetActive(false);
        }
    }
}
