using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRodManager : MonoBehaviour
{
    public static FishingRodManager instance;

    public GameObject fishingRodPivot;
    public GameObject bobber;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    private void Update()
    {
        Debug.Log(fishingRodPivot.GetComponent<Rotate>().result);
        if(fishingRodPivot.GetComponent<Rotate>().result)
        {

            LaunchBobber();
        }
    }

    public void LaunchBobber()
    {
        bobber.GetComponent<Rigidbody>().useGravity = true;
        bobber.GetComponent<Bobber>().Throw();
    }
}
