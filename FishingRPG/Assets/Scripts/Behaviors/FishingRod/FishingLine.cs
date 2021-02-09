using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class FishingLine : MonoBehaviour
{
    [Header("Tension Values")]
    public float fMax;
    public float fCritique;
    [HideInInspector] public float fCurrent;
    [HideInInspector] public float currentTension = 0;
    public float maxTension = 100;

    [Header("Etat Ligne")]
    [HideInInspector] public bool isBlocked = false;
    [HideInInspector] public bool isTaken = false;

    [Header("Texts/Jauge")]
    public Image tensionJauge;
    public Text textBlocked;
    public Text textTaken  ;

    [Header("Fishing Line Bezier")]
    public CheckWaterLevel checkWaterLevelScript;
    public Transform playerPoint;     //Point allant de 0 à 1 entre le joueur et le niveau de l'eau sous ses pieds
    public Transform fishPoint;		//Point allant de 0 à 1 entre le poisson et le niveau de l'eau sous le joueur

    private bool tensionOnce = false;

    public void Update()
    {
       
    }

    public void LineIsBroken()
    {
        FishingManager.instance.CancelFishing();
        
        //Vibrations cassures
        StartCoroutine(VibrationsLineBreak());

        CameraManager.instance.ScreenShake(0.35f);
        currentTension = 0f;
        UpdateJaugeTension();
    }

    //Augmentation tension Blocked
    public void TensionUp()
    {
        if (!FishManager.instance.currentFishBehavior.isDead && !FishManager.instance.currentFishBehavior.exhausted)
        {
            if (TutoManager.instance.isOnTutorial)
            {
                currentTension += (UtilitiesManager.instance.GetLossTensionNumber() / 50)/50;

                if (currentTension > 90f)
                {
                    currentTension = 90f;
                }
            }
            else
            {
                currentTension += UtilitiesManager.instance.GetLossTensionNumber() / 50;
            }
            UpdateJaugeTension();

            if (!tensionOnce)
            {

                //Play Sound
                AkSoundEngine.PostEvent("OnFilTendu", gameObject);
                tensionOnce = true;
            }
           

            if (currentTension >= maxTension)
            {
                currentTension = maxTension;
                UpdateJaugeTension();
                LineIsBroken();
            }
        }
        else
        {
            Debug.Log("IsExhausted or Dead so Tension dont down");
        }
    }

    //Augmentation tension Take Line
    public void TensionUpTakingLine()
    {
        if (!FishManager.instance.currentFishBehavior.isDead && !FishManager.instance.currentFishBehavior.exhausted)
        {
            if (TutoManager.instance.isOnTutorial)
            {
                currentTension += (UtilitiesManager.instance.GetLossTensionNumberTakingLine() / 60)/50;
                
                if(currentTension > 90f)
                {
                    currentTension = 90f;
                }
            }
            else
            {
                currentTension += UtilitiesManager.instance.GetLossTensionNumberTakingLine() / 60;
            }
            UpdateJaugeTension();
            
            if (!tensionOnce)
            {

                //Play Sound
                AkSoundEngine.PostEvent("OnFilTendu", gameObject);
                tensionOnce = true; 
            }

            if (currentTension >= maxTension )
            {
                currentTension = maxTension;
                UpdateJaugeTension();
                LineIsBroken();
            }
        }
        else
        {
            Debug.Log("IsExhausted or Dead so Tension dont up");
        }
    }

    public void TensionDown()
    {
        tensionOnce = false;
        //Stop Sound
        AkSoundEngine.PostEvent("STOP_FilTendu", gameObject);

        if (currentTension > 0f)
        {
            currentTension -= 55f * Time.fixedDeltaTime;
        }
        else
        {
            currentTension = 0f;
        }

        UpdateJaugeTension();
    }

    public void FCurrentDown()
    {
        fCurrent -= 0.05f;

        if(FishManager.instance.isAerial)
        {
            FishManager.instance.aerialEnterWaterZ -= 0.05f;
            FishManager.instance.UpdateAerial();
        }

    }

    public void GetFCurrent()
    {
        fCurrent = Vector3.Distance(FishingRodManager.instance.pointC.position, FishingRodManager.instance.bobber.transform.position) * 1.1f;
        FishingRodManager.instance.UpdateFCurrent();
    }

    public void isFCurrentAtMax()
    {
        isBlocked = true;
        fCurrent = fMax;
        textBlocked.color = Color.green;
    }

    public void UpdateJaugeTension()
    {
        tensionJauge.fillAmount = currentTension/maxTension;
    }

    #region Bezier Fishing Line
    public void CheckWaterLevel()
    {
        checkWaterLevelScript.SetPositionOnWater();
    }

    #endregion

    IEnumerator VibrationsLineBreak()
    {
        GamePad.SetVibration(0, 1f, 1f);
        yield return new WaitForSeconds(0.7f);
        GamePad.SetVibration(0, 0f, 0f);
    }

}
