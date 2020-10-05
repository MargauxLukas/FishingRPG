using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRodManager : MonoBehaviour
{
    public static FishingRodManager instance;

    public GameObject fishingRodPivot;
    public GameObject bobber;

    bool bobberThrowed = false;

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
        if(fishingRodPivot.GetComponent<Rotate>().result && !bobberThrowed)
        {
            //fishingRodPivot.GetComponent<Rotate>().result = false;
            bobberThrowed = true;
            LaunchBobber();
        }
    }

    public void LaunchBobber()
    {
        bobber.GetComponent<Rigidbody>().useGravity = true;
        bobber.GetComponent<Bobber>().Throw();
    }
}
