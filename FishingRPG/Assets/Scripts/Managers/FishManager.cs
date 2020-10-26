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

    public Vector3 minPosCone;
    public Vector3 maxPosCone;

    public Text speedText;

    public bool isAerial = false;
    public float aerialExitWaterX = 0f;
    public float aerialExitWaterY = 0f;
    public float aerialExitWaterZ = 0f;
    public float aerialEnterWaterX = 0f;
    public float aerialEnterWaterY = 0f;
    public float aerialEnterWaterZ = 0f;

    public float aerialX;
    public float aerialY;
    public float aerialZ;

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
            isAerial = true;
            aerialExitWaterX = currentFish.transform.position.x;
            aerialExitWaterY = currentFish.transform.position.y;
            aerialExitWaterZ = currentFish.transform.position.z;

            aerialEnterWaterX = currentFish.transform.position.x;
            aerialEnterWaterY = currentFish.transform.position.y;
            aerialEnterWaterZ = currentFish.transform.position.z;

            aerialX = currentFish.transform.position.x;
            aerialY = currentFish.transform.position.y + 5f;
            aerialZ = currentFish.transform.position.z;
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
        currentFish.GetComponent<FishBehavior>().endurance -= 0.2f;
        ChangeText();
        currentFish.GetComponent<FishBehavior>().CheckEndurance();
    }

    public void ChangeText()
    {
        enduText.text = currentFish.GetComponent<FishBehavior>().endurance.ToString();
    }

    public void ChangeSpeedText(float speed)
    {
        speedText.text = speed.ToString();
    }
}
