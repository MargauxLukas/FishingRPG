using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FishBehavior : MonoBehaviour
{
    [Header("Stats Fish")]
    public FishStats  fishStats ;
    public FishyFiche fishyFiche;
    public  float baseSpeed      = 3f;                //A prendre sur fishyFiche
    private float speed          = 1f;                       
    public  float currentStamina = 0f;                //A prendre sur fishyFiche (Deviendra endurance actuel)
    public float currentLife     = 0f;                //Max à prendre sur fishyFiche
    public bool exhausted        = false;
    public bool isDead           = false;

    [Header("Aerial")]
    //Possiblement dans FishManager
    public float JumpHeight = 20f;                   //Valeur à obtenir avec formule (stats du player contre stats du fish)

    [System.NonSerialized]
    public float timer         = 0f;
    public float timerAerial   = 0f;
    public float maxTimeAerial = 2f;
    public float timeDirection = 3f;
    [System.NonSerialized]
    public int nbRebond = 1;
    public bool isFellDown = false;

    private bool directionHasChoosen = false;
    public bool fellingFreeze = false;

    private Quaternion saveDirection;

    private void Start()
    {
        fishyFiche     = fishStats.fiche   ;
        currentStamina = fishyFiche.stamina;
        currentLife    = fishyFiche.life   ;
        baseSpeed      = UtilitiesManager.instance.GetFishSpeed(fishyFiche.agility);

        FishManager.instance.ChangeEnduranceText();
        FishManager.instance.ChangeLifeText();
    }

    void Update()
    {
        if (!FishManager.instance.isAerial)
        {
            if (!exhausted)
            {
                if (!directionHasChoosen)
                {
                    ChooseDirection();
                }
                else
                {
                    timer += Time.deltaTime;

                    if (timer >= timeDirection)
                    {
                        directionHasChoosen = false;
                        timer = 0f;
                    }
                    else
                    {
                        if (FishingRodManager.instance.CheckIfOverFCritique())
                        {
                            transform.LookAt(new Vector3(FishingRodManager.instance.pointC.position.x, transform.position.y, FishingRodManager.instance.pointC.position.z));
                            transform.position += transform.forward * UtilitiesManager.instance.GetApplicatedForce() * Time.fixedDeltaTime;
                            transform.rotation = saveDirection;
                        }
                        else
                        {
                            if (FishingRodManager.instance.distanceCP > FishingRodManager.instance.fishingLine.fCurrent)
                            {
                                transform.LookAt(new Vector3(FishingRodManager.instance.pointC.position.x, transform.position.y, FishingRodManager.instance.pointC.position.z));
                                transform.position += transform.forward * UtilitiesManager.instance.GetApplicatedForce() * Time.fixedDeltaTime;
                                transform.rotation = saveDirection;
                                transform.position += transform.forward * baseSpeed * Time.fixedDeltaTime;
                            }
                            else
                            {
                                transform.position += transform.forward * baseSpeed * Time.fixedDeltaTime;
                            }
                        }
                    }
                }
            }
            else
            {
                if (!isDead)
                {
                    FishManager.instance.UpEndurance();
                }
                transform.LookAt(new Vector3(FishingRodManager.instance.pointC.position.x, transform.position.y, FishingRodManager.instance.pointC.position.z));
                transform.position += transform.forward * UtilitiesManager.instance.GetApplicatedForce() * Time.fixedDeltaTime;
            }
        }
        else
        {
            if(!fellingFreeze)
            {
                timerAerial += Time.deltaTime;
            }

            transform.position = GetAerialPosition(timerAerial / maxTimeAerial);

            if (timerAerial >= maxTimeAerial)
            {
                FishManager.instance.FishRecuperation();
                timerAerial = 0f;
            }
        }

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 4f))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            ChooseDirectionOpposite();
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 4f, Color.white);
        }
    }

    public Vector3 GetAerialPosition(float currentTime )
    {
        if (!isFellDown)
        {
            //float x = Mathf.Pow(1 - currentTime, 2) * FishManager.instance.aerialExitWaterX + 2 * (1 - currentTime) * currentTime * FishManager.instance.aerialX + Mathf.Pow(currentTime,2) * FishManager.instance.aerialEnterWaterX;
            float y = Mathf.Pow(1 - currentTime, 2) * FishManager.instance.aerialExitWaterY + 2 * (1 - currentTime) * currentTime * FishManager.instance.aerialY + Mathf.Pow(currentTime, 2) * FishManager.instance.aerialEnterWaterY;
            //float z = Mathf.Pow(1 - currentTime, 2) * FishManager.instance.aerialExitWaterZ + 2 * (1 - currentTime) * currentTime * FishManager.instance.aerialZ + Mathf.Pow(currentTime,2) * FishManager.instance.aerialEnterWaterZ;
            return new Vector3(transform.position.x, y, transform.position.z);
        }
        else
        {
            //(1-t)*P0 + t*p1
            Debug.Log("test");
            float y = (1- currentTime) * FishManager.instance.aerialExitWaterY + currentTime*FishManager.instance.aerialEnterWaterY;

            return new Vector3(transform.position.x, y, transform.position.z);
        }
    }

    public void ChooseDirection()
    {
        transform.rotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);

        saveDirection = transform.rotation;
        directionHasChoosen = true;
    }

    public void ChooseDirectionOpposite()
    {
        //Debug.Log("HIT donc change direction : " + transform.rotation.y + " pour " + transform.rotation.y + 180f);
        transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
    }

    public void CalculateSpeed()
    {
        //Plus comme avant

        FishManager.instance.ChangeSpeedText(speed);
    }

    public void CheckTensionAndEndurance()
    {
        //Plus comme avant
    }

    public void CheckEndurance()
    {
        if(currentStamina <= 0)
        {
            currentStamina = 0;
            exhausted = true;
            FishManager.instance.ExtenuedChange();
        }

        if(currentStamina > fishyFiche.stamina)
        {
            currentStamina = fishyFiche.stamina;
        }
    }

    public void CheckLife()
    {
        if(currentLife <= 0)
        {
            currentLife = 0;
            isDead = true;
            currentStamina = 0;
            CheckEndurance();
            FishManager.instance.ChangeEnduranceText();
        }
    }
}
