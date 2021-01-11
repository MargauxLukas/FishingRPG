using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishManager : MonoBehaviour
{
    public static FishManager instance;

    [Header("Fish")]
    [HideInInspector] public GameObject currentFish;
    [HideInInspector] public FishBehavior currentFishBehavior;
    public Transform savePos;

    [Header("Material Aerial")]
    public ExhaustedDebug exhaustedDebug;

    [Header("Text")]
    public Text staminaText;
    public Text lifeText;
    public Image staminaJauge;
    public Image lifeJauge;

    [Header("Aerial variables")]
    [HideInInspector] public bool isAerial = false;
    [HideInInspector] public bool isFelling = false;
    [HideInInspector] public float aerialExitWaterX  = 0f;
    [HideInInspector] public float aerialX;
    [HideInInspector] public float aerialEnterWaterX = 0f;
    [HideInInspector] public float  aerialExitWaterY  = 0f;
    [HideInInspector] public float  aerialY;
    [HideInInspector] public float  aerialEnterWaterY = 0f;
    [HideInInspector] public float aerialExitWaterZ  = 0f;
    [HideInInspector] public float aerialZ;
    [HideInInspector] public float aerialEnterWaterZ = 0f;

    [Header("Rotation Percent")]
    public int sPercent;
    public int sePercent;
    public int esPercent;
    public int ePercent;
    public int enPercent;
    public int nePercent;
    public int nPercent;
    public int noPercent;
    public int onPercent;
    public int oPercent;
    public int osPercent;
    public int soPercent;

    //Debug Aerial
    public bool debugAerialFish = false;
    public GameObject test1;
    public GameObject test2;

    [HideInInspector]public List<int> directionPercentList = new List<int>(11);
    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    public void Start()
    {
        directionPercentList = new List<int>(new int[] { sPercent, sePercent, esPercent, ePercent, enPercent, nePercent, nPercent, noPercent, onPercent, oPercent, osPercent, soPercent });
    }

    public void IsExhausted()
    {
        if(currentFishBehavior.exhausted)
        {
            isAerial = true;
            currentFishBehavior.maxTimeAerial = UtilitiesManager.instance.GetTimeAerial(currentFishBehavior.JumpHeight, currentFishBehavior.nbRebond);
            aerialExitWaterX = currentFish.transform.position.x;
            aerialExitWaterY = currentFish.transform.position.y;
            aerialExitWaterZ = currentFish.transform.position.z;
            
            aerialEnterWaterX = currentFish.transform.position.x;
            aerialEnterWaterZ = currentFish.transform.position.z;
            
            aerialX = currentFish.transform.position.x;
            aerialY = currentFish.transform.position.y + UtilitiesManager.instance.GetHeightMaxForAerial(currentFishBehavior.JumpHeight);
            aerialZ = currentFish.transform.position.z;

            if (debugAerialFish)
            {
                test1.transform.position = new Vector3(aerialEnterWaterX, aerialEnterWaterY, aerialEnterWaterZ);
                test2.transform.position = new Vector3(aerialX, aerialY, aerialZ);
            }
        }
    }

    public void UpdateAerial()
    {
        if (debugAerialFish)
        {
            test1.transform.position = new Vector3(aerialEnterWaterX, aerialEnterWaterY, aerialEnterWaterZ);
            test2.transform.position = new Vector3(aerialX, aerialY, aerialZ);
        }
    }

    public void SetAerialEnterWater()
    {
        aerialEnterWaterY = currentFish.transform.position.y;
    }

    public void MoreAerial()
    {
        Debug.Log("Boing Again");
        currentFishBehavior.nbRebond++;
        currentFishBehavior.maxTimeAerial = UtilitiesManager.instance.GetTimeAerial(currentFishBehavior.JumpHeight, currentFishBehavior.nbRebond);
        aerialExitWaterX = currentFish.transform.position.x;
        aerialExitWaterY = currentFish.transform.position.y;
        aerialExitWaterZ = currentFish.transform.position.z;

        aerialX = currentFish.transform.position.x;
        //aerialY = currentFish.transform.position.y + UtilitiesManager.instance.GetHeightMaxForAerial(currentFishBehavior.JumpHeight);
        aerialZ = currentFish.transform.position.z;

        isFelling = false;
        currentFishBehavior.isFellDown = false;
        AerialRebondDamage();
        currentFishBehavior.timerAerial = 0f;
    }

    public void FellAerial()
    {
        isFelling = true;
        currentFishBehavior.fellingFreeze = true;
        
        StartCoroutine(FellingFreeze());
    }

    IEnumerator FellingFreeze()
    {
        yield return new WaitForSeconds(0.05f);

        Debug.Log("Abattage");

        currentFishBehavior.maxTimeAerial = UtilitiesManager.instance.GetTimeFellingAerial(currentFishBehavior.maxTimeAerial, currentFish.transform.position.y - aerialExitWaterY, aerialY);

        aerialExitWaterX = currentFish.transform.position.x;
        aerialExitWaterY = currentFish.transform.position.y;
        aerialExitWaterZ = currentFish.transform.position.z;

        currentFishBehavior.isFellDown = true;
        currentFishBehavior.fellingFreeze = false;
        currentFishBehavior.timerAerial = 0f;

        yield return new WaitForSeconds(currentFishBehavior.maxTimeAerial);
        AerialDamage();
    }


    public void ExtenuedChange()
    {
        exhaustedDebug.SetToGreen();
    }

    public void NotExtenued()
    {
        exhaustedDebug.SetToRed();
    }

    //Recuperation d'Endurance après Aerial
    public void FishRecuperation()
    {
        currentFishBehavior.exhausted = false;
        currentFishBehavior.animator.SetBool("isDeadOrExhausted", false);
        currentFishBehavior.currentStamina = currentFishBehavior.fishyFiche.stamina;
        currentFishBehavior.nbRebond = 1;
        isAerial = false;
        isFelling = false;
        currentFishBehavior.isFellDown = false;
        NotExtenued();
        staminaJauge.fillAmount = currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina;
        //ChangeStaminaText();
    }

    //Perte d'endurance lorsque la ligne est bloqué
    public void DownStamina()
    {
        if (currentFishBehavior.currentStamina > 0)
        {
            currentFishBehavior.currentStamina -= UtilitiesManager.instance.GetLossEnduranceNumber()/60;
            staminaJauge.fillAmount = currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina;
            //ChangeStaminaText();
        }

        currentFishBehavior.CheckStamina();
    }

    //Perte d'endurance lorsque la ligne est remonté
    public void DownStaminaTakingLine()
    {
        if (currentFishBehavior.currentStamina > 0)
        {
            currentFishBehavior.currentStamina -= UtilitiesManager.instance.GetLossEnduranceNumberTakingLine()/60;
            staminaJauge.fillAmount = currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina;
        }

        currentFishBehavior.CheckStamina();
    }

    //Récupération d'endurance lorsque extenué
    public void UpStamina()
    {
        if (!currentFishBehavior.isDead)
        {
            if (currentFishBehavior.currentStamina < currentFishBehavior.fishyFiche.stamina)
            {
                currentFishBehavior.currentStamina += (currentFishBehavior.fishyFiche.stamina * 0.50f) / 45;
                staminaJauge.fillAmount = currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina;
            }

            if (currentFishBehavior.currentStamina > currentFishBehavior.fishyFiche.stamina)
            {
                currentFishBehavior.currentStamina = currentFishBehavior.fishyFiche.stamina;
                DebugManager.instance.vz.DesactivateZone();
                currentFishBehavior.exhausted = false;
                currentFishBehavior.animator.SetBool("isDeadOrExhausted", false);
                NotExtenued();
                staminaJauge.fillAmount = currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina;
            }
        }
    }

    //Récupération d'endurance lorsque pas extenué et qu'il n'en perd pas
    public void LowUpStamina()
    {
        if (!currentFishBehavior.isDead)
        {
            if (currentFishBehavior.currentStamina < currentFishBehavior.fishyFiche.stamina)
            {
                currentFishBehavior.currentStamina += (currentFishBehavior.fishyFiche.stamina * 0.02f) / 60;
                staminaJauge.fillAmount = currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina;
            }
            else
            {
                currentFishBehavior.currentStamina = currentFishBehavior.fishyFiche.stamina;
                staminaJauge.fillAmount = currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina;
            }
        }
    }

    public void AerialDamage()
    {
        if (currentFishBehavior.currentLife > 0f)
        {
            currentFishBehavior.currentLife -= UtilitiesManager.instance.GetFellingDamage();
            FishManager.instance.currentFishBehavior.animator.SetBool("isDamage", true);
            lifeJauge.fillAmount = currentFishBehavior.currentLife / currentFishBehavior.fishyFiche.life;
        }

        currentFishBehavior.CheckLife();
    }

    public void AerialRebondDamage()
    {
        if (currentFishBehavior.currentLife > 0f)
        {
            currentFishBehavior.currentLife -= UtilitiesManager.instance.GetAerialRebondDamage();
            FishManager.instance.currentFishBehavior.animator.SetBool("isDamage", true);
            lifeJauge.fillAmount = currentFishBehavior.currentLife / currentFishBehavior.fishyFiche.life;
        }

        currentFishBehavior.CheckLife();
    }

    public void SetFinishPoint()
    {
        aerialExitWaterX = currentFish.transform.position.x;
        aerialExitWaterY = currentFish.transform.position.y;
        aerialExitWaterZ = currentFish.transform.position.z;

        aerialEnterWaterX = FishingManager.instance.finishFishDestination.transform.position.x;
        aerialEnterWaterY = FishingManager.instance.finishFishDestination.transform.position.y;
        aerialEnterWaterZ = FishingManager.instance.finishFishDestination.transform.position.z;

        aerialX = FishingManager.instance.midFishDestination.transform.position.x;
        aerialY = FishingManager.instance.midFishDestination.transform.position.y;
        aerialZ = FishingManager.instance.midFishDestination.transform.position.z;

        currentFishBehavior.inVictoryZone = true;
        isAerial = true;
    }

    public void ChangeStaminaJauge()
    {
        staminaJauge.fillAmount = currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina;
    }

    public void ChangeLifeJauge()
    {
        lifeJauge.fillAmount = currentFishBehavior.currentLife / currentFishBehavior.fishyFiche.life;
    }
}
