using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    public float throwPower = 1f;                           //Force ajouté lors du lancé du flotteur

    public void Throw()
    {
        GetComponent<MoveToDynamic>().GameObjectToDynamics();
        transform.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwPower);
    }
}
