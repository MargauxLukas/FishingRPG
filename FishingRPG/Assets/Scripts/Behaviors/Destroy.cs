using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    //Il faut pas Destroy, faut le mettre de côté
    public void DestroyThisGameobject()
    {
        Destroy(gameObject);
    }
}
