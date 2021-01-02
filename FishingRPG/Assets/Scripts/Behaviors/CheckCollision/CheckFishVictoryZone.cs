using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFishVictoryZone : MonoBehaviour
{
    public GameObject rbButtonVictory;

    [System.NonSerialized] public bool isNearVictoryZone = false;

    public LayerMask victoryMask;
    public Transform fishCheck;
    public float victoryDistance = 0.5f;

    private void FixedUpdate()
    {
        if (fishCheck != null)
        {
            isNearVictoryZone = Physics.CheckSphere(fishCheck.position, victoryDistance, victoryMask);

            if (isNearVictoryZone && (FishManager.instance.currentFishBehavior.isDead || FishManager.instance.currentFishBehavior.exhausted))
            {
                rbButtonVictory.SetActive(true);
                rbButtonVictory.transform.LookAt(new Vector3(PlayerManager.instance.player.transform.position.x, rbButtonVictory.transform.position.y, PlayerManager.instance.player.transform.position.z));
            }
            else
            {
                rbButtonVictory.SetActive(false);
            }
        }
    }
}
