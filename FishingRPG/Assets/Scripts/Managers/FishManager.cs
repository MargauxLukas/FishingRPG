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

    [Header("Aerial variables")]
    [HideInInspector] public bool isAerial = false;
    [HideInInspector] public bool isFelling = false;
    [HideInInspector] public bool hasJustSpawned = false;
    [HideInInspector] public float aerialExitWaterX  = 0f;
    [HideInInspector] public float aerialX;
    [HideInInspector] public float aerialEnterWaterX = 0f;
    [HideInInspector] public float  aerialExitWaterY  = 0f;
    [HideInInspector] public float  aerialY;
    [HideInInspector] public float  aerialEnterWaterY = 0f;
    [HideInInspector] public float aerialExitWaterZ  = 0f;
    [HideInInspector] public float aerialZ;
    [HideInInspector] public float aerialEnterWaterZ = 0f;

    public GameObject splash;

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

    public GameObject iconRage;
    public GameObject iconDeath;
    public GameObject iconExhausted;

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
        splash.transform.position = new Vector3(aerialEnterWaterX, aerialEnterWaterY, aerialEnterWaterZ);
        StartCoroutine(SplashCoroutine());
        currentFishBehavior.timerAerial = 0f;
    }

    IEnumerator SplashCoroutine()
    {
        splash.SetActive(true);
        yield return new WaitForSeconds(1f);
        splash.SetActive(false);
    }

    public void FellAerial()
    {
        isFelling = true;
        currentFishBehavior.fellingFreeze = true;

        StartCoroutine(FellingFreeze());
    }

    IEnumerator FellingFreeze()
    {
        yield return new WaitForSeconds(0.4f);

        currentFishBehavior.maxTimeAerial = UtilitiesManager.instance.GetTimeFellingAerial(currentFishBehavior.maxTimeAerial, currentFish.transform.position.y - aerialExitWaterY, aerialY);
        //Debug.Log("Abattage : " + currentFishBehavior.maxTimeAerial);

        aerialExitWaterX = currentFish.transform.position.x;
        aerialExitWaterY = currentFish.transform.position.y;
        aerialExitWaterZ = currentFish.transform.position.z;

        currentFishBehavior.isFellDown = true;
        currentFishBehavior.fellingFreeze = false;
        currentFishBehavior.timerAerial = 0f;

        yield return new WaitForSeconds(currentFishBehavior.maxTimeAerial);
        splash.transform.position = new Vector3(aerialEnterWaterX, aerialEnterWaterY, aerialEnterWaterZ);
        //Play Sound
        AkSoundEngine.PostEvent("OnFishSlammed", gameObject);

        StartCoroutine(SplashCoroutine());
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
        if (!currentFishBehavior.isDead)
        {
            currentFishBehavior.exhausted = false;
            DesactivateAllIcon();
            currentFishBehavior.animator.SetBool("isDeadOrExhausted", false);
            currentFishBehavior.shaderMaterialFish.SetFloat("Vector1_403CFD6B", 1f);
            currentFishBehavior.shaderMaterialEyes.SetFloat("Vector1_403CFD6B", 1f);
            currentFishBehavior.currentStamina = currentFishBehavior.fishyFiche.stamina;
            currentFishBehavior.nbRebond = 1;
            isAerial = false;
            isFelling = false;
            currentFishBehavior.isFellDown = false;
            NotExtenued();
            LifeStaminaUI.instance.UpdateStamina(currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina);
            //ChangeStaminaText();
        }
        else
        {
            currentFishBehavior.nbRebond = 1;
            isAerial = false;
            isFelling = false;
            currentFishBehavior.isFellDown = false;
        }
    }

    //Perte d'endurance lorsque la ligne est bloqué
    public void DownStamina()
    {
        if(!PlayerManager.instance.playerInventory.inventory.tutoFini)
        {
            if (currentFishBehavior.currentStamina > 0)
            {
                currentFishBehavior.currentStamina -= UtilitiesManager.instance.GetLossEnduranceNumber() / 20;
                LifeStaminaUI.instance.UpdateStamina(currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina);
                //ChangeStaminaText();
            }
        }
        else
        {
            if (currentFishBehavior.currentStamina > 0)
            {
                currentFishBehavior.currentStamina -= UtilitiesManager.instance.GetLossEnduranceNumber() / 60;
                LifeStaminaUI.instance.UpdateStamina(currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina);
                //ChangeStaminaText();
            }
        }

        currentFishBehavior.CheckStamina();
    }

    //Perte d'endurance lorsque la ligne est remonté
    public void DownStaminaTakingLine()
    {
        if(!PlayerManager.instance.playerInventory.inventory.tutoFini)
        {
            if (currentFishBehavior.currentStamina > 0)
            {
                currentFishBehavior.currentStamina -= UtilitiesManager.instance.GetLossEnduranceNumberTakingLine() / 20;
                LifeStaminaUI.instance.UpdateStamina(currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina);
            }
        }
        else
        {
            if (currentFishBehavior.currentStamina > 0)
            {
                currentFishBehavior.currentStamina -= UtilitiesManager.instance.GetLossEnduranceNumberTakingLine() / 60;
                LifeStaminaUI.instance.UpdateStamina(currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina);
            }
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
                LifeStaminaUI.instance.UpdateStamina(currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina);
            }

            if (currentFishBehavior.currentStamina > currentFishBehavior.fishyFiche.stamina)
            {
                currentFishBehavior.currentStamina = currentFishBehavior.fishyFiche.stamina;
                DebugManager.instance.vz.DesactivateZone();
                currentFishBehavior.exhausted = false;
                DesactivateAllIcon();
                currentFishBehavior.animator.SetBool("isDeadOrExhausted", false);
                currentFishBehavior.shaderMaterialFish.SetFloat("Vector1_403CFD6B", 1f);
                currentFishBehavior.shaderMaterialEyes.SetFloat("Vector1_403CFD6B", 1f);
                NotExtenued();
                LifeStaminaUI.instance.UpdateStamina(currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina);
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
                LifeStaminaUI.instance.UpdateStamina(currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina);
            }
            else
            {
                    currentFishBehavior.currentStamina = currentFishBehavior.fishyFiche.stamina;
                    LifeStaminaUI.instance.UpdateStamina(currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina);
                
            }
        }
    }

    public void AerialDamage()
    {
        if (currentFishBehavior.currentLife > 0f)
        {
            currentFishBehavior.currentLife -= UtilitiesManager.instance.GetFellingDamage();
            FishManager.instance.currentFishBehavior.animator.SetTrigger("isDamage");
            LifeStaminaUI.instance.UpdateLife(currentFishBehavior.currentLife / currentFishBehavior.fishyFiche.life);
            //Set Switch
            AkSoundEngine.SetSwitch("CurrentFishInCombat", "SnapSnack", gameObject);
            //Play Sound
            AkSoundEngine.PostEvent("OnDammage", gameObject);
        }

        currentFishBehavior.CheckLife();
    }

    public void AerialRebondDamage()
    {
        if (currentFishBehavior.currentLife > 0f)
        {
            currentFishBehavior.currentLife -= UtilitiesManager.instance.GetAerialRebondDamage();
            FishManager.instance.currentFishBehavior.animator.SetTrigger("isDamage");
            LifeStaminaUI.instance.UpdateLife(currentFishBehavior.currentLife / currentFishBehavior.fishyFiche.life);
            // Set Switch
            AkSoundEngine.SetSwitch("CurrentFishInCombat", "SnapSnack", gameObject);
            //Play Sound
            AkSoundEngine.PostEvent("OnDammage", gameObject);
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
        LifeStaminaUI.instance.UpdateStamina(currentFishBehavior.currentStamina / currentFishBehavior.fishyFiche.stamina);
    }

    public void ChangeLifeJauge()
    {
        LifeStaminaUI.instance.UpdateLife(currentFishBehavior.currentLife / currentFishBehavior.fishyFiche.life);
    }

    public void DesactivateAllIcon()
    {
        iconDeath.SetActive(false);
        iconExhausted.SetActive(false);
        iconRage.SetActive(false);
    }

    public void ActiveRageIcon()
    {
        iconDeath.SetActive(false);
        iconExhausted.SetActive(false);
        iconRage.SetActive(true);
    }

    public void ActiveExhaustedIcon()
    {
        iconRage.SetActive(false);
        iconDeath.SetActive(false);
        iconExhausted.SetActive(true);
    }

    public void ActiveDeathIcon()
    {
        iconDeath.SetActive(true);
        iconRage.SetActive(false);
        iconExhausted.SetActive(false);
    }
}
