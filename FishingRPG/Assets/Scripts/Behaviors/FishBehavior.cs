using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FishBehavior : MonoBehaviour
{
    [Header("Stats Fish")]
    public FishStats fishStats;
    private FishyFiche fishyFiche;
    public  float baseSpeed = 3f;                     //A prendre sur fishyFiche
    private float speed     = 1f;                       
    public  float currentStamina = 0f;                //A prendre sur fishyFiche (Deviendra endurance actuel)
    public float currentLife = 0f;                    //Max à prendre sur fishyFiche
    public bool extenued    = false;

    //Position de référence
    private Vector3 maxPos;                         //Position la plus éloigné sur le cone à sa droite que le poisson cherche lorsqu'on bloque la ligne
    private Vector3 minPos;                         //Position la plus éloigné sur le cone à sa gauche que le poisson cherche lorsqu'on bloque la ligne
    private Vector3 midPos;
    private Vector3 pullLeft;                       //Position proche sur le cone à gauche que le poisson cherche à atteindre --- Lien avec pullDistance
    private Vector3 pullRight;                      //Position proche sur le cone à droite que le poisson cherche à atteindre --- Lien avec pullDistance
    private Vector3 forwardPoint;                   //Point le plus éloigné devant le joueur

    [Header("Pull Values")]
    public float pullDistance = 2f;                 //Défini le point que le poisson cherche à atteindre --> Position que le poisson cherche à atteindre lorsque ligne bloqué - pullDistance

    //Zone de pénalité
    private float zone1;
    private float zone2;
    private float farAway1;
    private float farAway2;

    //Direction
    private int directionChoice = 0;
    private bool isDirectionChoosen = false;

    [Header("Aerial")]
    //Possiblement dans FishManager
    public float JumpHeight = 20f;      //Valeur à obtenir avec formule (stats du player contre stats du fish)

    [System.NonSerialized]
    public float timer = 0f;
    public float maxTime = 2f;

    [Range(0f,1f)]
    public float percentOfMaxTime = 0.85f;

    private bool bPull = true;

    private void Start()
    {
        fishyFiche = fishStats.fiche;
        currentStamina = fishyFiche.stamina;
        currentLife = fishyFiche.life;
        baseSpeed = UtilitiesManager.instance.GetVpL(fishyFiche.speed);
    }

    void Update()
    {
        if (!FishManager.instance.isAerial)
        {
            SetMaxAndMinDistance();
            if (PlayerManager.instance.blockLine || PlayerManager.instance.pullTowards)
            {
                if (!isDirectionChoosen)
                {
                    MovingRightOrLeft();
                }
                else
                {
                    Move();
                }
            }
            else
            {
                MovingAway();
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

    public void MovingRightOrLeft()
    {
        directionChoice = Random.Range(0, 2);
        Move();
        isDirectionChoosen = true;
    }

    public void Move()
    {
        if (directionChoice == 1)
        {
            FishingManager.instance.fishIsGoingRight = true;
            FishManager.instance.ChangeBoolText("Droite");
            MovingRight();
        }
        else
        {
            FishingManager.instance.fishIsGoingRight = false;
            FishManager.instance.ChangeBoolText("Gauche");
            MovingLeft();
        }
    }

    public void MovingAway()
    {
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerManager.instance.player.transform.position.x, transform.position.y, PlayerManager.instance.player.transform.position.z), -1 * 1.2f * Time.deltaTime);
        transform.Translate(Vector3.forward * 0.5f * Time.fixedDeltaTime, PlayerManager.instance.playerView.transform);

        //A améliorer

        /*if (Vector3.Distance(transform.position, new Vector3(PlayerManager.instance.player.transform.position.x, transform.position.y, PlayerManager.instance.player.transform.position.z)) > farAway1)
        {
            FishingRodManager.instance.fishingLine.TensionDown();
            FishManager.instance.DownEndurance();
        }
        else if(Vector3.Distance(transform.position, new Vector3(PlayerManager.instance.player.transform.position.x, transform.position.y, PlayerManager.instance.player.transform.position.z)) > farAway2)
        {
            FishManager.instance.DownEndurance();
        }*/
    }

    public void MovingRight()
    {
        if (Vector3.Distance(transform.position, new Vector3(maxPos.x, transform.position.y, maxPos.z)) < 0.3f || Vector3.Distance(transform.position, new Vector3(pullRight.x, transform.position.y, pullRight.z)) < 0.3f)
        {
            ChangeDirection();
        }

        CheckTensionAndEndurance();

        CalculateSpeed();
        if (PlayerManager.instance.blockLine){transform.position = Vector3.MoveTowards(transform.position, new Vector3(maxPos.x   , transform.position.y, maxPos.z   ), speed * Time.deltaTime);}
        else                                 {transform.position = Vector3.MoveTowards(transform.position, new Vector3(pullRight.x, transform.position.y, pullRight.z), speed * Time.deltaTime);}
    }

    public void MovingLeft()
    {
        if (Vector3.Distance(transform.position, new Vector3(minPos.x, transform.position.y, minPos.z)) < 0.3f || Vector3.Distance(transform.position, new Vector3(pullLeft.x, transform.position.y, pullLeft.z)) < 0.3f)
        {
            ChangeDirection();
        }

        CheckTensionAndEndurance();

        CalculateSpeed();
        if (PlayerManager.instance.blockLine){transform.position = Vector3.MoveTowards(transform.position, new Vector3(minPos.x  , transform.position.y, minPos.z  ), speed * Time.deltaTime);}
        else                                 {transform.position = Vector3.MoveTowards(transform.position, new Vector3(pullLeft.x, transform.position.y, pullLeft.z), speed * Time.deltaTime);}
    }

    public void ChangeDirection()
    {
        CheckEndurance();
        if (directionChoice == 1)
        {
            directionChoice = 2;
        }
        else
        {
            directionChoice = 1;
        }

        bPull = true;
        SetMaxAndMinDistance();
    }

    public void CalculateSpeed()
    {
        float pos = (Vector3.Distance(minPos, transform.position) - Vector3.Distance(midPos, minPos)) / Vector3.Distance(midPos, minPos);
        /*if (FishingRodManager.instance.IsSameDirection())
        {
            if (FishingManager.instance.fishIsGoingRight)
            {
                speed = UtilitiesManager.instance.GetVpF(baseSpeed, pos, FishingRodManager.instance.axisValueForCalcul, fishyFiche.weight, 1);
                Debug.Log("BaseSpeed = " + baseSpeed + " / Position Fish = " + pos + " / Position Player = " + FishingRodManager.instance.axisValueForCalcul + " / Poid Fish = " + fishyFiche.weight);
            }
            else
            {
                speed = UtilitiesManager.instance.GetVpF(baseSpeed, pos, FishingRodManager.instance.axisValueForCalcul, fishyFiche.weight, -1);
                Debug.Log("BaseSpeed = " + baseSpeed + " / Position Fish = " + pos + " / Position Player = " + FishingRodManager.instance.axisValueForCalcul + " / Poid Fish = " + fishyFiche.weight);
            }
        }
        else
        {
            if (FishingManager.instance.fishIsGoingRight)
            {
                speed = UtilitiesManager.instance.GetVpF(baseSpeed, pos, FishingRodManager.instance.axisValueForCalcul, fishyFiche.weight, 1);
                Debug.Log("BaseSpeed = " + baseSpeed + " / Position Fish = " + pos + " / Position Player = " + FishingRodManager.instance.axisValueForCalcul + " / Poid Fish = " + fishyFiche.weight);
            }
            else
            {
                speed = UtilitiesManager.instance.GetVpF(baseSpeed, pos, FishingRodManager.instance.axisValueForCalcul, fishyFiche.weight, -1);
                Debug.Log("BaseSpeed = " + baseSpeed + " / Position Fish = " + pos + " / Position Player = " + FishingRodManager.instance.axisValueForCalcul + " / Poid Fish = " + fishyFiche.weight);
            }
        }*/
        
        if (FishingRodManager.instance.IsSameDirection())
        {
            if (FishingManager.instance.fishIsGoingRight)
            {
                //Debug.Log("1");
                speed = (baseSpeed + (baseSpeed * (Mathf.Abs((FishingRodManager.instance.axisValueForCalcul -  pos/ 2))))) * (PlayerManager.instance.playerStats.strenght / fishyFiche.weight);
                Debug.Log("1 : " + (Mathf.Abs((FishingRodManager.instance.axisValueForCalcul - pos / 2))));
                FishManager.instance.ChangeSpeedText(speed);
                //Debug.Log("+ = " + (1f + Mathf.Abs(FishingRodManager.instance.GetPlayerForce())));
            }
            else
            {
                //Debug.Log("2");
                speed = (baseSpeed + (baseSpeed * (Mathf.Abs((FishingRodManager.instance.axisValueForCalcul - pos / 2))))) * (PlayerManager.instance.playerStats.strenght / fishyFiche.weight);
                Debug.Log("2 : " + (Mathf.Abs((FishingRodManager.instance.axisValueForCalcul - pos / 2))));
                FishManager.instance.ChangeSpeedText(speed);
                //Debug.Log("- = " + (1f - Mathf.Abs(FishingRodManager.instance.GetPlayerForce())));
            }
        }
        else
        {
            if (FishingManager.instance.fishIsGoingRight)
            {
                //Debug.Log("3");
                speed = (baseSpeed - (baseSpeed * (Mathf.Abs((FishingRodManager.instance.axisValueForCalcul - pos))))) * (PlayerManager.instance.playerStats.strenght / fishyFiche.weight);
                Debug.Log("3 : " + (Mathf.Abs((FishingRodManager.instance.axisValueForCalcul - pos))));
                FishManager.instance.ChangeSpeedText(speed);
                //Debug.Log("- = " + (1f - Mathf.Abs(FishingRodManager.instance.GetPlayerForce())));
            }
            else
            {
                //Debug.Log("4");
                speed = (baseSpeed - (baseSpeed * (Mathf.Abs((FishingRodManager.instance.axisValueForCalcul - pos))))) * (PlayerManager.instance.playerStats.strenght / fishyFiche.weight);
                Debug.Log("4 : " + (Mathf.Abs((FishingRodManager.instance.axisValueForCalcul - pos))));
                FishManager.instance.ChangeSpeedText(speed);
                //Debug.Log("- = " + (1f - Mathf.Abs(FishingRodManager.instance.GetPlayerForce())));
            }
        }

        FishManager.instance.ChangeSpeedText(speed);
    }

    public void CheckTensionAndEndurance()
    {
        if (Vector3.Distance(transform.position, new Vector3(maxPos.x, transform.position.y, maxPos.z)) < zone1 || Vector3.Distance(transform.position, new Vector3(minPos.x, transform.position.y, minPos.z)) < zone1)
        {
            FishingRodManager.instance.fishingLine.TensionDown();
            FishManager.instance.DownEndurance();
        }
        else if (Vector3.Distance(transform.position, new Vector3(maxPos.x, transform.position.y, maxPos.z)) < zone2 || Vector3.Distance(transform.position, new Vector3(minPos.x, transform.position.y, minPos.z)) < zone2)
        {
            FishManager.instance.DownEndurance();
        }
        else
        {
            FishingRodManager.instance.fishingLine.TensionUp();
        }
    }

    /*public void PullTowards()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerManager.instance.player.transform.position.x, transform.position.y, PlayerManager.instance.player.transform.position.z), speed * Time.deltaTime);
    }*/

    public void CheckEndurance()
    {
        if(currentStamina <= 0)
        {
            extenued = true;
            FishManager.instance.ExtenuedChange();
        }
    }

    public void SetMaxAndMinDistance()
    {
        float angle         = 30.0f;            //Angle voulu (Doublon avec PlayerView)
        float halfFOV       = angle / 1.0f;     //FieldOfView (Doublon avec PlayerView)
        float coneDirection = 90;               //Direction dans un cercle allant de 0 à 380 Droite = 0 / Devant = 90 / Gauche = 180 / Arrière = 270 (Doublon avec PlayerView)

        Quaternion upRayRotation   = Quaternion.AngleAxis(-halfFOV + coneDirection, Vector3.down);   //Direction à droite du cône (Doublon avec PlayerView)
        Quaternion downRayRotation = Quaternion.AngleAxis( halfFOV + coneDirection, Vector3.down);   //Direction à gauche du cône (Doublon avec PlayerView)
        Quaternion forwardRayRotation = Quaternion.AngleAxis(coneDirection, Vector3.down);           //Direction tout droit (Doublon avec PlayerView)

        Vector3 cone = new Vector3(CameraManager.instance.mainCamera.transform.position.x, CameraManager.instance.mainCamera.transform.position.y - 3.25f, CameraManager.instance.mainCamera.transform.position.z);   //Cone représente le centre du cercle (Doublon avec PlayerView)

        float distance = Vector3.Distance(transform.localPosition, PlayerManager.instance.playerView.transform.position);   //Distance Poisson-Joueur (Doublon avec PlayerView)

        maxPos     = cone + FishManager.instance.maxPosCone      ;  //Point d'intersection entre le cercle de rayon Poisson-Joueur et le côté droit du cône (Doublon avec PlayerView)
        minPos     = cone + FishManager.instance.minPosCone      ;  //Point d'intersection entre le cercle de rayon Poisson-Joueur et le côté gauche du cône (Doublon avec PlayerView)
        midPos = (maxPos + minPos) / 2;
        midPos = new Vector3(midPos.x, transform.position.y, midPos.z);
        forwardPoint = forwardRayRotation * CameraManager.instance.mainCamera.transform.right * distance;                      //Point le plus éloigné en face

        zone2 = Vector3.Distance(minPos, maxPos) * 0.2f;       //Distance à partir de laquelle le poisson perd de l'endurance
        zone1 = Vector3.Distance(minPos, maxPos) * 0.1f;       //Distance à partir de laquelle le joueur perd de la tension
        farAway1 = Vector3.Distance(cone, forwardPoint) * 1.1f;
        farAway2 = Vector3.Distance(cone, forwardPoint) * 1.2f;

        if(bPull)
        {
            pullRight = cone + (upRayRotation * CameraManager.instance.mainCamera.transform.right * (distance - pullDistance));  //Pareil que maxPos mais plus proche (Direction qu'il cherche à atteindre lorsqu'on l'attire vers soi)
            pullLeft = cone + (downRayRotation * CameraManager.instance.mainCamera.transform.right * (distance - pullDistance));  //Pareil que minPos mais plus proche (Direction qu'il cherche à atteindre lorsqu'on l'attire vers soi)
            bPull = false;
        }
    }


    /*****************
    *  Gizmos Draw  *
    *****************/
    private void OnDrawGizmos()
    {
        Vector3 cone = new Vector3(CameraManager.instance.mainCamera.transform.position.x, CameraManager.instance.mainCamera.transform.position.y - 3.5f, CameraManager.instance.mainCamera.transform.position.z);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, pullRight);
        Gizmos.DrawLine(transform.position,  pullLeft);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, maxPos);
        Gizmos.DrawLine(transform.position, minPos);
        Gizmos.DrawLine(transform.position, midPos);


    }
}
