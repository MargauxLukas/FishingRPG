using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishManager : MonoBehaviour
{
    public static FishManager instance;
    public GameObject currentFish;
    public Material canAerialMat;
    public Material normalMat;
    public Text enduText;

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

    public void NotExtenued()
    {
        currentFish.GetComponent<MeshRenderer>().material = normalMat;
    }

    public void DownEndurance()
    {
        Debug.Log("heyEndu");
        currentFish.GetComponent<FishBehavior>().endurance -= 0.2f;
        ChangeText();
    }

    public void ChangeText()
    {
        enduText.text = currentFish.GetComponent<FishBehavior>().endurance.ToString();
    }
}
