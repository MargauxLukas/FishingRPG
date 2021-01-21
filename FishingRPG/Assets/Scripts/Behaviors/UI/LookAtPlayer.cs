using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    void Update()
    {
        if (gameObject.activeSelf){transform.LookAt(new Vector3(PlayerManager.instance.player.transform.position.x, transform.position.y, PlayerManager.instance.player.transform.position.z));}
    }
}
