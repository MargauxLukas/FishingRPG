using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberBack : MonoBehaviour
{
    public bool canReturn = false;
    Vector3 target;
    void Update()
    {
        if(canReturn)
        {
            Vector3.Slerp(transform.localPosition, target, Time.fixedDeltaTime);
            Debug.Log("ouk");
        }
    }

    public void ReturnToFishingRod(Vector3 Target)
    {
        target = Target;
        canReturn = true;
    }
}
