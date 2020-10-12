using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager instance;
    public GameObject currentFish;
    public Material canAerialMat;
    public Material normalMal;

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
