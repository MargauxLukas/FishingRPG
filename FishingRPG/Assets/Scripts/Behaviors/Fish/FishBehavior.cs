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
    public FishPatterns fishPattern;
    [HideInInspector] public FishyFiche fishyFiche;
    [HideInInspector] public float baseSpeed      = 3f;                                  //A prendre sur fishyFiche                      
    [HideInInspector] public float currentStamina = 0f;                //A prendre sur fishyFiche (Deviendra endurance actuel)
    [HideInInspector] public float currentLife    = 0f;                //Max à prendre sur fishyFiche
    [HideInInspector] public float strength       = 0f;

    [Header("State Fish")]
    [HideInInspector] public bool exhausted        = false;
    [HideInInspector] public bool isDead           = false;
    [HideInInspector] public bool inVictoryZone    = false;
    [HideInInspector] public bool isRage           = false;

    [Header("Idle")]
    [HideInInspector] public bool  isIdle      = true;
    [HideInInspector] public float idleTimer   = 0f;
                      public float idleMaxTime = 0f;

    [Header("Aerial")]
                           public float JumpHeight    = 20f;                   //Valeur à obtenir avec formule (stats du player contre stats du fish)
    [System.NonSerialized] public float timer         = 0f;
    [HideInInspector]      public float timerAerial   = 0f;
                           public float maxTimeAerial = 2f;
    [System.NonSerialized] public int nbRebond        = 1;
    [HideInInspector]      public bool isFellDown = false;
    [HideInInspector]      public bool fellingFreeze = false;

    [Header("Direction")]
    public float minTimeForChangeDirection = 1f;
    public float maxTimeForChangeDirection = 3f;
    private float timeDirection            = 0f;
    private int randomDirection;
    private bool directionHasChoosen = false;
    private Quaternion saveDirection;
    private Quaternion baseRotate;

    private Vector3 target;
    private float distance;

    public GameObject aButton;
    [HideInInspector] public bool canCollectTheFish = false;

    public Animator animator;

    float x;
    float y;
    float z;

    private void Start()
    {
        SetIdleMaxTime();

        fishyFiche     = fishStats.fiche   ;
        currentStamina = fishyFiche.stamina;
        currentLife    = fishyFiche.life   ;
        baseSpeed      = UtilitiesManager.instance.GetFishSpeed(fishyFiche.agility);
        strength       = fishyFiche.strength;
        SetBaseRotation();

        animator = transform.GetChild(0).GetComponent<Animator>();

        FishManager.instance.SetAerialEnterWater();
        FishManager.instance.staminaJauge.fillAmount = FishManager.instance.currentFishBehavior.currentStamina / FishManager.instance.currentFishBehavior.fishyFiche.stamina;
        FishManager.instance.ChangeLifeJauge();
    }

    void FixedUpdate()
    {
        if (!FishManager.instance.isAerial)
        {
            idleTimer += Time.fixedDeltaTime;

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
                    if (!exhausted && !isDead)
                    {
                        LowStaminaRecuperation();

                        if (!directionHasChoosen)
                        {
                            ChooseDirection();
                        }
                        else
                        {
                            timer += Time.fixedDeltaTime;

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
                    if (!isDead)
                    {
                        Aerial();
                    }
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

        if (!exhausted)
        {
            DetectionWall();
        }
    }

    public Vector3 GetAerialPosition(float currentTime )
    {
        if (!isFellDown)
        {
            LookAtFrontPoint(currentTime);

            x = Mathf.Pow(1 - currentTime, 2) * FishManager.instance.aerialExitWaterX + 2 * (1 - currentTime) * currentTime * FishManager.instance.aerialX + Mathf.Pow(currentTime,2) * FishManager.instance.aerialEnterWaterX;
            y = Mathf.Pow(1 - currentTime, 2) * FishManager.instance.aerialExitWaterY + 2 * (1 - currentTime) * currentTime * FishManager.instance.aerialY + Mathf.Pow(currentTime, 2) * FishManager.instance.aerialEnterWaterY;
            z = Mathf.Pow(1 - currentTime, 2) * FishManager.instance.aerialExitWaterZ + 2 * (1 - currentTime) * currentTime * FishManager.instance.aerialZ + Mathf.Pow(currentTime,2) * FishManager.instance.aerialEnterWaterZ;
            return new Vector3(x, y, z);
        }
        else
        {
            LookAtFrontPointFellDown(currentTime);

            x = (1 - currentTime) * FishManager.instance.aerialExitWaterX + currentTime * FishManager.instance.aerialEnterWaterX;
            y = (1- currentTime)  * FishManager.instance.aerialExitWaterY + currentTime * FishManager.instance.aerialEnterWaterY;
            z = (1 - currentTime) * FishManager.instance.aerialExitWaterZ + currentTime * FishManager.instance.aerialEnterWaterZ;

            return new Vector3(x, y, z);
        }
    }

    public void LookAtFrontPoint(float currentTime)
    {
        x = Mathf.Pow(1 - (currentTime+0.1f), 2) * FishManager.instance.aerialExitWaterX + 2 * (1 - (currentTime + 0.1f)) * (currentTime + 0.1f) * FishManager.instance.aerialX + Mathf.Pow(currentTime + 0.1f, 2) * FishManager.instance.aerialEnterWaterX;
        y = Mathf.Pow(1 - (currentTime + 0.1f), 2) * FishManager.instance.aerialExitWaterY + 2 * (1 - (currentTime + 0.1f)) * (currentTime + 0.1f) * FishManager.instance.aerialY + Mathf.Pow(currentTime + 0.1f, 2) * FishManager.instance.aerialEnterWaterY;
        z = Mathf.Pow(1 - (currentTime + 0.1f), 2) * FishManager.instance.aerialExitWaterZ + 2 * (1 - (currentTime + 0.1f)) * (currentTime + 0.1f) * FishManager.instance.aerialZ + Mathf.Pow(currentTime + 0.1f, 2) * FishManager.instance.aerialEnterWaterZ;

        transform.LookAt(new Vector3(x, y, z));
    }

    public void LookAtFrontPointFellDown(float currentTime)
    {
        x = (1 - (currentTime + 0.1f)) * FishManager.instance.aerialExitWaterX + (currentTime + 0.1f) * FishManager.instance.aerialEnterWaterX;
        y = (1 - (currentTime + 0.1f)) * FishManager.instance.aerialExitWaterY + (currentTime + 0.1f) * FishManager.instance.aerialEnterWaterY;
        z = (1 - (currentTime + 0.1f)) * FishManager.instance.aerialExitWaterZ + (currentTime + 0.1f) * FishManager.instance.aerialEnterWaterZ;

        transform.LookAt(new Vector3(x, y, z));
    }

    public void ChooseDirection()
    {
        randomDirection = Random.Range(1, 101);
        for (int i = 0; i < 12; i++)
        {
            if(randomDirection < FishManager.instance.directionPercentList[i])
            {
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
        transform.rotation *= Quaternion.Euler(0f, value, 0f);;
        timeDirection = Random.Range(minTimeForChangeDirection, maxTimeForChangeDirection);
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
            target = FishingRodManager.instance.pointC.position;
        }
        else if(distance < 20)
        {
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

        if (Vector3.Distance(transform.position, PlayerManager.instance.player.transform.position) < 5f)
        {
            Debug.Log("Trop proche, je m'éloigne !");
            ForceDirection();
        }

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
            FishManager.instance.UpStamina();
        }

        transform.LookAt(new Vector3(FishingRodManager.instance.pointC.position.x, transform.position.y, FishingRodManager.instance.pointC.position.z));
        transform.position += transform.forward * UtilitiesManager.instance.GetApplicatedForce() * Time.fixedDeltaTime;
    }

    public void Aerial()
    {
        if (!fellingFreeze)
        {
            timerAerial += Time.fixedDeltaTime;
            //Debug.Log(timerAerial);
        }

        transform.position = GetAerialPosition(timerAerial / maxTimeAerial);

        if (timerAerial >= maxTimeAerial)
        {
            Debug.Log(timerAerial);
            //Debug.Log("===============================");

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
            timerAerial += Time.fixedDeltaTime;

            transform.position = GetAerialPosition(timerAerial / maxTimeAerial);
        }
        else
        {
            canCollectTheFish = true;
            FishManager.instance.NotExtenued();
            aButton.SetActive(true);
            aButton.transform.LookAt(new Vector3(PlayerManager.instance.player.transform.position.x, aButton.transform.position.y, PlayerManager.instance.player.transform.position.z));
        }
    }

    public void CheckStamina()
    {
        if(currentStamina <= 0)
        {
            DebugManager.instance.vz.ActivateZone();
            currentStamina = 0;
            exhausted = true;
            animator.SetBool("isDeadOrExhausted", true);
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
            animator.SetBool("isDeadOrExhausted", true);
            currentStamina = 0;
            //CheckStamina();
            FishManager.instance.ChangeLifeJauge();
            FishManager.instance.staminaJauge.fillAmount = FishManager.instance.currentFishBehavior.currentStamina / FishManager.instance.currentFishBehavior.fishyFiche.stamina;
        }
    }

    public void SetIdleMaxTime()
    {
        idleMaxTime = Random.Range(5, 10);
    }

    public void ResetStats()
    {
        baseSpeed = UtilitiesManager.instance.GetFishSpeed(fishyFiche.agility);
    }

    public void ResetRage()
    {
        isRage = false;
        animator.SetBool("isRage", false);
        strength = fishyFiche.strength;
    }

    public void LowStaminaRecuperation()
    {

    }
}
