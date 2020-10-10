using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject player;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    public void DisablePlayerMovement()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
    }

    public void EnablePlayerMovement()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    public void DisableFishMovement()
    {

    }

    public void EnableFishMovement()
    {

    }
}
