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
    public CheckWater checkWaterScript;
    public BendFishingRod bendFishingRod;
    public Transform pointC;
    public List<Transform> listTargetFar = new List<Transform>();
    public List<Transform> listTargetNear = new List<Transform>();
    public FishingLine fishingLine;
    public GemSlot slot1;
    public GemSlot slot2;
    public GemSlot slot3;

    //Bobber
    private Vector3 bobberScale = new Vector3(5f, 0.25f, 5f);
    private Quaternion bobberRotation;

    [HideInInspector] public bool bobberThrowed = false;

    [Header("Ligne de la canne à pêche")]
    public LineRenderer line;

    [Header("Speed de la canne à pêche")]
    public float speed           = 10f;
    private float lastAxisValues = 0f;
    private float currentAxis;

    public float distanceCP;

    [Header("Jauge")]
    public Scrollbar fishDistanceCP;
    public Image fCurrentJauge;
    public Scrollbar fishHook;

    [Header("FishingRodAnimator")]
    public Animator animFishingRod;

    public float speedNumberAnimation = 0;
    public float speedAnimation = 0;
    public bool upSpeedNumberAnimation = false;
    public bool downSpeedNumberAnimation = false;

    [Header("Tweaking Debug")]
    public float mulAxisRight = 0.5f;
    public float mulAxisLeft = 1.5f;
    
    
    public float mulPosX = 1f;
    public float mulPosY = 1f;
    public float mulPosZ = 1f;

    public float mulRotX = 0f;
    public float mulRotY = 0f;
    public float mulRotZ = 0f;

    [Header("UI HELPER")]
    public GameObject huntUI;
    public GameObject fishingUI;


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
        bendFishingRod.SetupValuePerFloat();
    }

    private void FixedUpdate()
    {
        if(fishingRodPivot.GetComponent<Rotate>().result && !bobberThrowed)
        {
            bobberThrowed = true;
            LaunchBobber();
        }

        if (FishingManager.instance.currentFish != null)
        {
            distanceCP = Vector3.Distance(pointC.position, FishManager.instance.currentFish.transform.position);
            ChangeTextCPDistance();
            CheckFCurrent();
        }
        else
        {
            if(fishingLine.currentTension > 0)
            {
                fishingLine.TensionDown();
            }
        }

        /*if(upSpeedNumberAnimation && speedNumberAnimation < 1)
        {
            Debug.Log("test : " + speedNumberAnimation);
            speedNumberAnimation = Mathf.Lerp(speedNumberAnimation, 1, 1f);
            Debug.Log("test : " + speedNumberAnimation);
        }

        if (downSpeedNumberAnimation && speedNumberAnimation != 0)
        {
            speedNumberAnimation = Mathf.Lerp(1, 0, 1f);
        }*/
    }

    public void LaunchBobber()
    {
        bobber.GetComponent<Bobber>().Throw();

        CameraManager.instance.CameraLookAtGameObject(bobber);
        CameraManager.instance.SaveBaseRotation();

        PlayerManager.instance.DisablePlayerMovement();
        PlayerManager.instance.EnableFishMovement();

        huntUI.SetActive(false);
        fishingUI.SetActive(true);
    }

    public void BobberBack()
    {
        line.enabled = false;
        //A METTRE DANS UN BEHAVIOUR BobberBACK 
        bobberThrowed = false;
        bobber.transform.parent        = fishingRodGameObject.transform.GetChild(0).GetChild(0).transform   ;              //Reset parent
        StartCoroutine("Test");
        bobber.transform.localRotation = bobberRotation;
        SetFishingRodPosition(0f);
        bendFishingRod.ResetBendable();

        fishingRodPivot.GetComponent<Rotate>().result = false;                      //N'attend plus de pêcher un poisson

        CameraManager.instance.FreeCameraEnable();
        PlayerManager.instance.EnablePlayerMovement();
        PlayerManager.instance.DisableFishMovement();
        fishingUI.SetActive(false);
        huntUI.SetActive(true);

        //Debug.Log("isMax false");
        fishingRodPivot.GetComponent<Rotate>().ResetRotation();

        //Fish Poisson
    }

    IEnumerator Test()
    {
        yield return new WaitForEndOfFrame();
        Vector3 newVector = Vector3.Lerp(bobber.transform.localPosition , bobberPosition.transform.localPosition, 1f);
        bobber.transform.localPosition = newVector;
        FishingManager.instance.readyToFish = false;
        checkWaterScript.justOneTime = false;
    }

    public void SetFishingRodPosition(float axisValue)
    {
        if (Mathf.Abs(axisValue - lastAxisValues) > 0.1f)
        {
            if (axisValue > 0)
            {
                lastAxisValues = axisValue;
                currentAxis    = axisValue * 0.3f;
            }
            else
            {
                lastAxisValues = axisValue;
                currentAxis    = axisValue * 0.8f;
            }
        }

            fishingRodGameObject.transform.localPosition = Vector3.Lerp(fishingRodGameObject.transform.localPosition, new Vector3(currentAxis, fishingRodGameObject.transform.localPosition.y, fishingRodGameObject.transform.localPosition.z), speed * Time.fixedDeltaTime);
            fishingRodGameObject.transform.localRotation = Quaternion.Slerp(fishingRodGameObject.transform.localRotation, Quaternion.Euler(-20 * Mathf.Abs(axisValue), 10 * Mathf.Abs(axisValue), -30 * axisValue), speed * Time.fixedDeltaTime);
        


        //  /!\ Valeur au pif pour tester, need calcul d'un nombre entre 0f et 1f
        if(FishManager.instance.isAerial && !FishManager.instance.currentFishBehavior.isDead)
        {
            FishManager.instance.aerialEnterWaterX += currentAxis*0.2f;
            FishManager.instance.UpdateAerial();
        }
    }

    public void CheckFCurrent()
    {
        if (fishingLine.isTaken)
        {
            if (distanceCP < fishingLine.fCurrent + fishingLine.fCritique)
            {
                speedAnimation += 1f * Time.fixedDeltaTime;
                if(speedAnimation > 1f)
                {
                    speedAnimation = 1f;
                }
                AnimationReelUp(speedAnimation);

                //Play Sound
                //AkSoundEngine.PostEvent("OnMoulinetOn", gameObject);
                
                fishingLine.FCurrentDown();

                if ((distanceCP > fishingLine.fCurrent) && !FishManager.instance.currentFishBehavior.exhausted)
                {
                    FishManager.instance.DownStaminaTakingLine();
                    fishingLine.TensionUpTakingLine();
                }
                else if(FishManager.instance.currentFishBehavior.exhausted)
                {
                    fishingLine.TensionDown();
                }
            }
            else
            {
                //Ravalement annulé
            }
        }
        else if (fishingLine.isBlocked)
        {
            animFishingRod.SetFloat("SpeedMultiplier", 0);
            if (distanceCP > fishingLine.fCurrent)
            {
                FishManager.instance.DownStamina();
                fishingLine.TensionUp();
            }
        }
        else if (distanceCP > fishingLine.fCurrent && fishingLine.fCurrent < fishingLine.fMax)    //Mettre à jour Fcurrent
        {
            speedAnimation += -1f * Time.fixedDeltaTime;

            if (speedAnimation < -1f)
            {
                speedAnimation = -1f;
            }
            AnimationReelUp(speedAnimation);

            fishingLine.TensionDown();
            fishingLine.fCurrent = distanceCP;
        }

        if (distanceCP >= fishingLine.fMax) 
        { 
            fishingLine.isFCurrentAtMax(); 
        }

        if(distanceCP < fishingLine.fCurrent)
        {
            fishingLine.TensionDown();
        }

        if (!fishingLine.isTaken && !fishingLine.isBlocked && !FishManager.instance.currentFishBehavior.exhausted)
        {
            FishManager.instance.LowUpStamina();
        }

        if (distanceCP < fishingLine.fCurrent && !fishingLine.isTaken)
        {
            if (speedAnimation > 0f)
            {
                speedAnimation += -1f * Time.fixedDeltaTime;

                if (speedAnimation < 0)
                {
                    speedAnimation = 0f;
                }
            }

            if (speedAnimation < 0f)
            {
                speedAnimation += 1f * Time.fixedDeltaTime;

                if (speedAnimation > 0f)
                {
                    speedAnimation = 0f;
                }
            }

            animFishingRod.SetFloat("SpeedMultiplier", speedAnimation);
        }
            UpdateFCurrent();
    }

    public void AnimationReelUp(float choosenSpeed)
    {
        animFishingRod.SetFloat("SpeedMultiplier", choosenSpeed);
    }

    public void AnimationReelDown(int choosenSpeed)
    {
        downSpeedNumberAnimation = true;
        animFishingRod.speed = choosenSpeed * speedNumberAnimation;
    }

    public bool CheckIfOverFCritique()
    {
        if(distanceCP > fishingLine.fCurrent + fishingLine.fCritique)
        {
            //FCritiqueText.color = Color.green;
            return true;
        }

        //FCritiqueText.color = Color.red;
        return false;
    }


    #region Text Change
    public void ChangeTextCPDistance()
    {
        fishDistanceCP.value = distanceCP/(fishingLine.fMax + fishingLine.fCritique);
    }

    public void UpdateFCurrent()
    {
        fCurrentJauge.fillAmount = fishHook.value = (fishingLine.fCurrent*100f)/((fishingLine.fMax + fishingLine.fCritique) * 100f);
    }
    #endregion
}
