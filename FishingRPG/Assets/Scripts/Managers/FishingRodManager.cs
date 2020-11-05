using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FishingRodManager : MonoBehaviour
{
    public static FishingRodManager instance;

    [Header("FishingRod Components")]
    public GameObject fishingRodPivot;
    public GameObject bobber;
    public GameObject bobberPosition;
    public GameObject fishingRodGameObject;
    public FishingLine fishingLine;

    [Header("Plus besoin si on prend le nouveau systeme de déplacement")]
    public GameObject[] positionGroup;

    [Header("Pour montrer visuellement que le poisson est arrivé")]
    public Material catchMaterial;
    public Material dontCatchMaterial;

    //Bobber
    private Vector3 bobberScale = new Vector3(5f, 0.25f, 5f);
    private Quaternion bobberRotation;

    public bool bobberThrowed = false;

    //Surement innutile plus tard
    public float direction = 0f;

    [Header("Speed de la canne à peche")]
    public float speed = 5f;
    private float lastAxisValues = 0f;
    private float currentAxis;
    public float axisValueForCalcul;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    private void Start()
    {
        bobberRotation = bobber.transform.localRotation;
        fishingLine = GetComponent<FishingLine>();
    }

    private void Update()
    {
        if(fishingRodPivot.GetComponent<Rotate>().result && !bobberThrowed)
        {
            bobberThrowed = true;
            LaunchBobber();
        }
    }

    public void LaunchBobber()
    {
        bobber.GetComponent<Bobber>().Throw();
        CameraManager.instance.CameraLookAtGameObject(bobber);
        CameraManager.instance.SaveBaseRotation();
        PlayerManager.instance.DisablePlayerMovement();
        PlayerManager.instance.EnableFishMovement();
    }

    public void BobberBack()
    {
        //A METTRE DANS UN BEHAVIOUR BobberBACK 
        bobberThrowed = false;
        bobber.transform.position = bobberPosition.transform.position;
        bobber.transform.parent   = fishingRodGameObject.transform;
        fishingRodPivot.GetComponent<Rotate>().result = false;
        bobber.transform.localScale    = bobberScale;
        bobber.transform.localRotation = bobberRotation;
        CameraManager.instance.FreeCameraEnable();
        PlayerManager.instance.EnablePlayerMovement();
        PlayerManager.instance.DisableFishMovement();
        FishManager.instance.currentFish.GetComponent<Destroy>().DestroyThisGameobject();
        //Fish Poisson
    }

    public void SetBobberMaterialToSucces()
    {
        bobber.GetComponent<MeshRenderer>().material = catchMaterial;
    }

    public void SetBobberMaterialToFail()
    {
        bobber.GetComponent<MeshRenderer>().material = dontCatchMaterial;
    }

    /*public void LeftFishingRod()
    {
        fishingRodGameObject.transform.localPosition = Vector3.MoveTowards(fishingRodGameObject.transform.localPosition, positionGroup[0].transform.localPosition, speed * Time.fixedDeltaTime);
    }

    public void RightFishingRod()
    {
        fishingRodGameObject.transform.localPosition = Vector3.MoveTowards(fishingRodGameObject.transform.localPosition, positionGroup[2].transform.localPosition, speed * Time.fixedDeltaTime);
    }*/

    public void SetFishingRodPosition(float axisValue)
    {
        axisValueForCalcul = axisValue;
        if (Mathf.Abs(axisValue - lastAxisValues) > 0.1f)
        {
            if (axisValue > 0)
            {
                lastAxisValues = axisValue;
                currentAxis = axisValue * 0.5f;
            }
            else
            {
                lastAxisValues = axisValue;
                currentAxis= axisValue * 1.5f;
            }
        }
        fishingRodGameObject.transform.localPosition = Vector3.Lerp(fishingRodGameObject.transform.localPosition, new Vector3(currentAxis, fishingRodGameObject.transform.localPosition.y, fishingRodGameObject.transform.localPosition.z), speed*Time.fixedDeltaTime);
        fishingRodGameObject.transform.localRotation = Quaternion.Slerp(fishingRodGameObject.transform.localRotation, Quaternion.Euler(50f, 0 , -50*axisValue), speed*Time.fixedDeltaTime);
    }

    public float GetPlayerForce()
    {
        direction = fishingRodGameObject.transform.localPosition.x;
        if(direction > 0)
        {
            direction *= 3f;
        }
        return direction;
    }

    public bool IsSameDirection()
    {
        if(FishingManager.instance.fishIsGoingRight)
        {
            if(axisValueForCalcul > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (axisValueForCalcul < 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
