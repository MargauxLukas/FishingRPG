using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToDynamic : MonoBehaviour
{
    public GameObject dynamic;
    public void GameObjectToDynamics()
    {
        transform.parent = dynamic.transform;
    }
}
