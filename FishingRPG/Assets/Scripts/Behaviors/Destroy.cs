using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public void DestroyThisGameobject()
    {
        Debug.Log("2");
        Destroy(gameObject);
    }
}
