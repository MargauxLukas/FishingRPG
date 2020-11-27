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
    public FishPatterns fishPattern;
    public  float baseSpeed      = 3f;                //A prendre sur fishyFiche                      
    public  float currentStamina = 0f;                //A prendre sur fishyFiche (Deviendra endurance actuel)
    public float currentLife     = 0f;                //Max à prendre sur fishyFiche
    public float strength        = 0f;
    public bool exhausted        = false;
    public bool isDead           = false;
    public bool inVictoryZone    = false;

    [Header("Idle")]
    public bool  isIdle      = true;
    public float idleTimer   = 0f;
    public float idleMaxTime = 0f;

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

    public bool isRage = false;

    private Vector3 target;
    private float distance;
    private Quaternion baseRotate;
    private int randomDirection;

    private void Start()
    {
        SetIdleMaxTime();

        fishyFiche     = fishStats.fiche   ;
        currentStamina = fishyFiche.stamina;
        currentLife    = fishyFiche.life   ;
        baseSpeed      = UtilitiesManager.instance.GetFishSpeed(fishyFiche.agility);
        strength       = fishyFiche.strength;
        SetBaseRotation();


        FishManager.instance.ChangeEnduranceText();
        FishManager.instance.ChangeLifeText();
    }

    void Update()
    {
        if (!FishManager.instance.isAerial)
        {
            idleTimer += Time.deltaTime;

            if (idleTimer > idleMaxTime)
            {
                isIdle = false;
            }
        }

        if (isIdle)
        {
            if (!inVictoryZone)
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
                                Idle();
                            }
                        }
                    }
                    else
                    {
                        ExhaustedAndDeath();
                    }
                }
                else
                {
                    Aerial();
                }
            }
            else
            {
                Victory();
            }
        }
        else
        {
            if (gameObject.GetComponent<FishPatterns>().currentPattern == null)
            {
                Debug.Log("Choose a Patern !");
                fishPattern.startPattern(isRage);
            }
        }

        DetectionWall();
    }

    public Vector3 GetAerialPosition(float currentTime )
    {
        if (!isFellDown)
        {
            float x = Mathf.Pow(1 - currentTime, 2) * FishManager.instance.aerialExitWaterX + 2 * (1 - currentTime) * currentTime * FishManager.instance.aerialX + Mathf.Pow(currentTime,2) * FishManager.instance.aerialEnterWaterX;
            float y = Mathf.Pow(1 - currentTime, 2) * FishManager.instance.aerialExitWaterY + 2 * (1 - currentTime) * currentTime * FishManager.instance.aerialY + Mathf.Pow(currentTime, 2) * FishManager.instance.aerialEnterWaterY;
            float z = Mathf.Pow(1 - currentTime, 2) * FishManager.instance.aerialExitWaterZ + 2 * (1 - currentTime) * currentTime * FishManager.instance.aerialZ + Mathf.Pow(currentTime,2) * FishManager.instance.aerialEnterWaterZ;
            return new Vector3(x, y, z);
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
        randomDirection = Random.Range(1, 101);
        Debug.Log("Rand -- >" + randomDirection);
        for (int i = 0; i < 12; i++)
        {
            if(randomDirection < FishManager.instance.directionPercentList[i])
            {
                Debug.Log("Rotate -- >" + i);
                ApplicateRotation(i);
                break;
            }
            else
            {
                randomDirection -= FishManager.instance.directionPercentList[i];
            }
        }

        saveDirection = transform.rotation;
        directionHasChoosen = true;
    }

    public void ApplicateRotation(int i)
    {
        int value = -30 * i;
        transform.rotation = baseRotate;
        Debug.Log(transform.rotation);
        transform.rotation *= Quaternion.Euler(0f, value, 0f);
        Debug.Log(transform.rotation);
    }

    public void ChooseDirectionOpposite()
    {
        Debug.Log("HIT donc change direction : " + transform.rotation.y + " pour " + transform.rotation.y + 180f);
        transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
    }

    public void ForceDirection()
    {
        timer = 0f;
        transform.LookAt(new Vector3(FishManager.instance.savePos.position.x, transform.position.y, FishManager.instance.savePos.position.z));
    }

    //Call at Start, sert de valeur de base pour savoir ces différentes rotations.
    public void SetBaseRotation()
    {
        transform.LookAt(new Vector3(FishingRodManager.instance.pointC.position.x, transform.position.y, FishingRodManager.instance.pointC.position.z));
        baseRotate = transform.rotation;
    }

    public void ChooseTarget()
    {
        distance = Vector3.Distance(transform.position, FishingRodManager.instance.pointC.position);

        if (distance < 10f)
        {
            Debug.Log("TARGET : Point C");
            target = FishingRodManager.instance.pointC.position;
        }
        else if(distance < 20)
        {
            Debug.Log("TARGET : NEAR");
            if (Input.GetAxis("Right Stick (Horizontal)") > 0.1f)
            {
                target = FishingRodManager.instance.listTargetNear[0].position;
            }
            else
            {
                target = FishingRodManager.instance.listTargetNear[1].position;
            }
            
        }
        else
        {
            Debug.Log("TARGET : FAR");

            if (Input.GetAxis("Right Stick (Horizontal)") > 0.1f)
            {
                target = FishingRodManager.instance.listTargetFar[0].position;
            }
            else
            {
                target = FishingRodManager.instance.listTargetFar[1].position;
            }
        }
    }

    public void Idle()
    {
        ChooseTarget();

        if (FishingRodManager.instance.CheckIfOverFCritique())
        {
            transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
            transform.position += transform.forward * UtilitiesManager.instance.GetApplicatedForce() * Time.fixedDeltaTime;
            transform.rotation = saveDirection;
        }
        else
        {
            if (FishingRodManager.instance.distanceCP > FishingRodManager.instance.fishingLine.fCurrent)
            {
                transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
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

    public void ExhaustedAndDeath()
    {
        if (!isDead)
        {
            FishManager.instance.UpEndurance();
        }
        transform.LookAt(new Vector3(FishingRodManager.instance.pointC.position.x, transform.position.y, FishingRodManager.instance.pointC.position.z));
        transform.position += transform.forward * UtilitiesManager.instance.GetApplicatedForce() * Time.fixedDeltaTime;
    }

    public void Aerial()
    {
        if (!fellingFreeze)
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

    public void DetectionWall()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 4f))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            ChooseDirection();
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 4f, Color.white);
        }

        if(Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            Debug.Log("Force Direction");
            ForceDirection();
        }
    }

    public void Victory()
    {
        if (timerAerial <= maxTimeAerial)
        {
            timerAerial += Time.deltaTime;

            transform.position = GetAerialPosition(timerAerial / maxTimeAerial);
        }
    }

    public void CheckEndurance()
    {
        if(currentStamina <= 0)
        {
            DebugManager.instance.vz.ActivateZone();
            currentStamina = 0;
            exhausted = true;
            FishManager.instance.ExtenuedChange();
            ResetRage();
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
            DebugManager.instance.vz.ActivateZone();
            currentLife = 0;
            isDead = true;
            currentStamina = 0;
            CheckEndurance();
            FishManager.instance.ChangeLifeText();
            FishManager.instance.ChangeEnduranceText();
        }
    }

    public void SetIdleMaxTime()
    {
        idleMaxTime = Random.Range(7, 16);
    }

    public void ResetStats()
    {
        baseSpeed = UtilitiesManager.instance.GetFishSpeed(fishyFiche.agility);
    }

    public void ResetRage()
    {
        isRage = false;
        strength = fishyFiche.strength;
    }
}
