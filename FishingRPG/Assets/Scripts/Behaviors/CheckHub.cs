using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHub : MonoBehaviour
{
    public GameObject aButtonHub;

    [System.NonSerialized] public bool isNearHub = false;

    public LayerMask hubMask;
    public Transform hubCheck;
    public float hubDistance = 0.5f;

    private void Update()
    {
        isNearHub = Physics.CheckSphere(hubCheck.position, hubDistance, hubMask);

        if (isNearHub)
        {
            aButtonHub.SetActive(true);
            aButtonHub.transform.LookAt(new Vector3(PlayerManager.instance.player.transform.position.x, aButtonHub.transform.position.y, PlayerManager.instance.player.transform.position.z));
        }
        else
        {
            aButtonHub.SetActive(false);
        }
    }
}
