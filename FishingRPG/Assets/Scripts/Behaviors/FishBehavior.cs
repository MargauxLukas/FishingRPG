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
    public float maxTime       = 2f;
    public float timeDirection = 3f;

    [Range(0f,1f)]
    public float percentOfMaxTime = 0.85f;

    private bool directionHasChoosen = false;

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
            if (PlayerManager.instance.blockLine || PlayerManager.instance.pullTowards)
            {
                //Je sais pas 
            }

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
                    //RayCast à lancer ou GameObject devant le poisson pour savoir vers ou il va
                    if (Vector3.Distance(transform.position + transform.right * 1f * Time.fixedDeltaTime, FishingRodManager.instance.pointC.transform.position) > FishingRodManager.instance.fishingLine.fMax)
                    {
                        //DONT MOVE
                    }
                    else
                    {
                        Debug.Log("hey");
                        transform.position += transform.right * 1f * Time.fixedDeltaTime;
                    }
                }
            }

            if(extenued)
            {
                currentStamina += 1f;

                if(currentStamina >= 50)
                {
                    extenued = false;
                    FishManager.instance.NotExtenued();
                }
            }
        }
        else
        {
            timer += Time.deltaTime;

            transform.position = GetAerialPosition(timer / maxTime);

            if (timer >= maxTime)
            {
                FishManager.instance.FishRecuperation();
                timer = 0f;
            }
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
        directionHasChoosen = true;
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
