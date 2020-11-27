using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
    public bool justOnce = false;

    public bool lookAtPlayer = false;
    public bool oposite = false;
    public bool trente = false;


    void Update()
    {
        if(lookAtPlayer && !justOnce)
        {
            transform.LookAt(new Vector3(FishingRodManager.instance.pointC.position.x, transform.position.y, FishingRodManager.instance.pointC.position.z));
            justOnce = true;
        }

        if(oposite && !justOnce)
        {
            transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
            justOnce = true;
        }

        if(trente && !justOnce)
        {
            transform.rotation *= Quaternion.Euler(0f, -30f, 0f);
            justOnce = true;
        }
    }
}
