using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    public float throwPower = 1f;

    public void Throw()
    {
        transform.parent = null;
        //Debug.Log(Vector3.forward);
        transform.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwPower);
    }
}
