using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishManager : MonoBehaviour
{
    public static FishManager instance;

    [Header("Fish")]
    public GameObject currentFish;
    public FishBehavior currentFishBehavior;

    [Header("Material Aerial")]
    public Material canAerialMat;
    public Material normalMat;

    [Header("Text")]
    public Text enduText;
    public Text speedText;

    public bool isAerial = false;
    private float aerialExitWaterX  = 0f;
    private float aerialX;
    private float aerialEnterWaterX = 0f;
    public float  aerialExitWaterY  = 0f;
    public float  aerialY;
    public float  aerialEnterWaterY = 0f;
    private float aerialExitWaterZ  = 0f;
    private float aerialZ;
    private float aerialEnterWaterZ = 0f;

    private float timer = 0f;
    private float maxTime = 1f;
    private bool isEnduranceJustDown = false;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    private void Update()
    {
        if(isEnduranceJustDown)
        {
            timer += Time.fixedDeltaTime;

            if(timer >= maxTime)
            {
                isEnduranceJustDown = false;
                timer = 0f;
            }
        }
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
        currentFishBehavior.currentStamina = currentFishBehavior.fishyFiche.stamina;
        isAerial = false;
        NotExtenued();
        CameraManager.instance.SetOriginPoint();
    }

    public void DownEndurance()
    {
        if (!isEnduranceJustDown)
        {
            if (currentFishBehavior.currentStamina > 0)
            {
                currentFishBehavior.currentStamina -= UtilitiesManager.instance.GetLossEnduranceNumber();
                ChangeText();
            }

            currentFishBehavior.CheckEndurance();

            isEnduranceJustDown = true;
        }
    }

    public void DownEnduranceTakingLine()
    {
        if (!isEnduranceJustDown)
        {
            if (currentFishBehavior.currentStamina > 0)
            {
                currentFishBehavior.currentStamina -= UtilitiesManager.instance.GetLossEnduranceNumberTakingLine();
                ChangeText();
            }

            currentFishBehavior.CheckEndurance();

            isEnduranceJustDown = true;
        }
    }

    public void UpEndurance()
    {
        if (currentFishBehavior.currentStamina < currentFishBehavior.fishyFiche.stamina)
        {
            currentFishBehavior.currentStamina += 0.5f;
            ChangeText();
        }
    }

    public void ChangeText()
    {
        enduText.text = currentFishBehavior.currentStamina.ToString();
    }

    public void ChangeSpeedText(float speed)
    {
        speedText.text = speed.ToString();
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
