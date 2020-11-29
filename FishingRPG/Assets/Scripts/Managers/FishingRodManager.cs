using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class FishingRodManager : MonoBehaviour
{
    public static FishingRodManager instance;

    [Header("FishingRod Components")]
    public GameObject fishingRodPivot;
    public GameObject bobber;
    public GameObject bobberPosition;
    public GameObject fishingRodGameObject;
    public BendFishingRod bendFishingRod;
    public Transform pointC;
    public List<Transform> listTargetFar = new List<Transform>();
    public List<Transform> listTargetNear = new List<Transform>();
    [HideInInspector] public FishingLine fishingLine;

    [Header("Pour montrer visuellement que le poisson est arrivé")]
    public Material catchMaterial;
    public Material dontCatchMaterial;

    //Bobber
    private Vector3 bobberScale = new Vector3(5f, 0.25f, 5f);
    private Quaternion bobberRotation;

    [HideInInspector] public bool bobberThrowed = false;

    [Header("Speed de la canne à peche")]
    public float speed           = 10f;
    private float lastAxisValues = 0f;
    private float currentAxis;

    public float distanceCP;

    [Header("Texte")]
    public Text distanceCPText;
    public Text fCurrentText;
    public Text fMaxText;
    public Text FCritiqueText;

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
        bendFishingRod.SetupValuePerFloat();
        ChangeTextFMax();
    }

    private void Update()
    {
        if(fishingRodPivot.GetComponent<Rotate>().result && !bobberThrowed)
        {
            bobberThrowed = true;
            fishingLine.cableComponent.UpdateLineLength(Vector3.Distance(pointC.position, bobber.transform.position));
            LaunchBobber();
        }

        if (FishingManager.instance.currentFish != null)
        {
            distanceCP = Vector3.Distance(pointC.position, FishManager.instance.currentFish.transform.position);
            ChangeTextCPDistance();
            CheckFCurrent();
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
        bobber.transform.parent        = fishingRodGameObject.transform   ;              //Reset parent
        StartCoroutine("Test");
        bobber.transform.localScale    = bobberScale   ;
        bobber.transform.localRotation = bobberRotation;
        SetFishingRodPosition(0f);

        fishingRodPivot.GetComponent<Rotate>().result = false;                      //N'attend plus de pêcher un poisson

        CameraManager.instance.FreeCameraEnable();
        PlayerManager.instance.EnablePlayerMovement();
        PlayerManager.instance.DisableFishMovement();

        //Fish Poisson
    }

    IEnumerator Test()
    {
        yield return new WaitForEndOfFrame();
        Vector3 newVector = Vector3.Lerp(bobber.transform.localPosition , bobberPosition.transform.localPosition, 1f);
        bobber.transform.localPosition = newVector;
        FishingManager.instance.readyToFish = false;
    }

    public void SetBobberMaterialToSucces()
    {
        bobber.GetComponent<MeshRenderer>().material = catchMaterial;
    }

    public void SetBobberMaterialToFail()
    {
        bobber.GetComponent<MeshRenderer>().material = dontCatchMaterial;
    }

    public void SetFishingRodPosition(float axisValue)
    {
        if (Mathf.Abs(axisValue - lastAxisValues) > 0.1f)
        {
            if (axisValue > 0)
            {
                lastAxisValues = axisValue;
                currentAxis    = axisValue * 0.5f;
            }
            else
            {
                lastAxisValues = axisValue;
                currentAxis    = axisValue * 1.5f;
            }
        }
        fishingRodGameObject.transform.localPosition = Vector3.Lerp(fishingRodGameObject.transform.localPosition, new Vector3(currentAxis, fishingRodGameObject.transform.localPosition.y, fishingRodGameObject.transform.localPosition.z), speed*Time.fixedDeltaTime);
        fishingRodGameObject.transform.localRotation = Quaternion.Slerp(fishingRodGameObject.transform.localRotation, Quaternion.Euler(0f, 0 , -50*axisValue), speed*Time.fixedDeltaTime);
    }

    public void CheckFCurrent()
    {
        if (fishingLine.isTaken)
        {
            if (distanceCP < fishingLine.fCurrent + fishingLine.fCritique)
            {
                fishingLine.FCurrentDown();

                if (distanceCP > fishingLine.fCurrent)
                {
                    FishManager.instance.DownEnduranceTakingLine();
                    fishingLine.TensionDownTakingLine();
                }
            }
            else
            {
                //Ravalement annulé
            }
        }
        else if (fishingLine.isBlocked)
        {
            if (distanceCP > fishingLine.fCurrent)
            {
                FishManager.instance.DownEndurance();
                fishingLine.TensionDown();
            }
        }     
        else if (distanceCP > fishingLine.fCurrent && fishingLine.fCurrent < fishingLine.fMax)    //Mettre à jour Fcurrent
        {
            fishingLine.TensionUp();
            fishingLine.fCurrent = distanceCP;
        }

        if (distanceCP >= fishingLine.fMax) 
        { 
            fishingLine.isFCurrentAtMax(); 
        }

        if(distanceCP < fishingLine.fCurrent)
        {
            fishingLine.TensionUp();
        }

        ChangeTextFCurrent();
    }

    public bool CheckIfOverFCritique()
    {
        if(distanceCP > fishingLine.fCurrent + fishingLine.fCritique)
        {
            FCritiqueText.color = Color.green;
            return true;
        }

        FCritiqueText.color = Color.red;
        return false;
    }


    #region Text Change
    public void ChangeTextCPDistance()
    {
        distanceCPText.text = distanceCP.ToString();
    }

    public void ChangeTextFCurrent()
    {
        fishingLine.cableComponent.UpdateLineLength(fishingLine.fCurrent);

        if(fishingLine.fCurrent < fishingLine.fMax)
        {
            fCurrentText.color = Color.green;
        }
        if (fishingLine.fCurrent >= fishingLine.fMax)
        {
            fCurrentText.color = Color.red;
        }

        fCurrentText.text = fishingLine.fCurrent.ToString();
    }
    public void ChangeTextFMax()
    {
        fMaxText.text = fishingLine.fMax.ToString();
    }
    #endregion
}
