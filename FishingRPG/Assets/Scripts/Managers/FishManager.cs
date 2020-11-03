using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishManager : MonoBehaviour
{
    public static FishManager instance;
    public GameObject currentFish;
    public FishBehavior currentFishBehavior;
    public Material canAerialMat;
    public Material normalMat;
    public Text enduText;

    public Vector3 minPosCone;
    public Vector3 maxPosCone;

    public Text speedText;
    public Text boolIsGoingRight;

    public bool isAerial = false;
    private float aerialExitWaterX = 0f;
    public float aerialExitWaterY = 0f;
    private float aerialExitWaterZ = 0f;
    private float aerialEnterWaterX = 0f;
    public float aerialEnterWaterY = 0f;
    private float aerialEnterWaterZ = 0f;

    private float aerialX;
    public float aerialY;
    private float aerialZ;

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
        if(currentFishBehavior.extenued)
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

    public void FishRecuperation()
    {
        currentFishBehavior.extenued = false;
        currentFishBehavior.currentStamina = 20f;
        isAerial = false;
        NotExtenued();
        CameraManager.instance.SetOriginPoint();
    }

    public void DownEndurance()
    {
        currentFishBehavior.currentStamina -= 0.2f;
        ChangeText();
        currentFishBehavior.CheckEndurance();
    }

    public void ChangeText()
    {
        enduText.text = currentFishBehavior.currentStamina.ToString();
    }

    public void ChangeSpeedText(float speed)
    {
        speedText.text = speed.ToString();
    }

    public void ChangeBoolText(string direction)
    {
        boolIsGoingRight.text = direction;
    }

    public void MoreAerial()
    {
        Debug.Log("Boing Again");
        aerialExitWaterX = currentFish.transform.position.x;
        aerialExitWaterY = currentFish.transform.position.y;
        aerialExitWaterZ = currentFish.transform.position.z;

        aerialX = currentFish.transform.position.x;
        aerialY = currentFish.transform.position.y + 5f;
        aerialZ = currentFish.transform.position.z;

        currentFishBehavior.timer = 0f;
    }
}
