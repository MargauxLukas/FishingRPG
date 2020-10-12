using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager instance;
    public GameObject currentFish;
    public Material canAerialMat;
    public Material normalMal;

    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    Vector3 velocity;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    public void IsExtenued()
    {
        if(currentFish.GetComponent<FishBehavior>().extenued)
        {
            currentFish.GetComponent<FishBehavior>().isAerial = true;
        }
    }

    public void ExtenuedChange()
    {
        currentFish.GetComponent<MeshRenderer>().material = canAerialMat;
    }
}
