using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    public float throwPower = 1f;

    public void Throw()
    {
        //transform.GetComponent<Rigidbody>().AddForce(Vector3.forward * throwPower);
    }
}
