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
    public bool extenued         = false;

    [Header("Aerial")]
    //Possiblement dans FishManager
    public float JumpHeight = 20f;                   //Valeur à obtenir avec formule (stats du player contre stats du fish)

    [System.NonSerialized]
    public float timer         = 0f;
    public float timerAerial   = 0f;
    public float maxTime       = 2f;
    public float timeDirection = 3f;

    [Range(0f,1f)]
    public float percentOfMaxTime = 0.85f;

    private bool directionHasChoosen = false;

    private Quaternion saveDirection;

    private void Start()
    {
        fishyFiche     = fishStats.fiche   ;
        currentStamina = fishyFiche.stamina;
        currentLife    = fishyFiche.life   ;
        baseSpeed      = UtilitiesManager.instance.GetFishSpeed(fishyFiche.agility);
    }

    void Update()
    {
        if (!FishManager.instance.isAerial)
        {
            if(!directionHasChoosen)
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
                            //Debug.Log("Force A : " + UtilitiesManager.instance.GetApplicatedForce());
                            transform.position += transform.forward * UtilitiesManager.instance.GetApplicatedForce() * Time.fixedDeltaTime;
                            transform.rotation = saveDirection;
                            //Debug.Log("Vitesse base : " + baseSpeed);
                            transform.position += transform.forward * baseSpeed * Time.fixedDeltaTime;
                        }
                        else
                        {
                            transform.position += transform.forward * baseSpeed * Time.fixedDeltaTime;
                        }
                    }
                }
            }

            if(extenued)
            {
                FishManager.instance.UpEndurance();
            }
        }
        else
        {
            timerAerial += Time.deltaTime;

            transform.position = GetAerialPosition(timerAerial / maxTime);

            if (timerAerial >= maxTime)
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
        //float x = Mathf.Pow(1 - currentTime, 2) * FishManager.instance.aerialExitWaterX + 2 * (1 - currentTime) * currentTime * FishManager.instance.aerialX + currentTime * FishManager.instance.aerialEnterWaterX;
        float y = Mathf.Pow(1 - currentTime, 2) * FishManager.instance.aerialExitWaterY + 2 * (1 - currentTime) * currentTime * FishManager.instance.aerialY + currentTime * FishManager.instance.aerialEnterWaterY;
        //float z = Mathf.Pow(1 - currentTime, 2) * FishManager.instance.aerialExitWaterZ + 2 * (1 - currentTime) * currentTime * FishManager.instance.aerialZ + currentTime * FishManager.instance.aerialEnterWaterZ;
        return new Vector3(transform.position.x , y, transform.position.z);
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
            extenued = true;
            FishManager.instance.ExtenuedChange();
        }

        if(currentStamina > fishyFiche.stamina)
        {
            currentStamina = fishyFiche.stamina;
        }
    }
}
