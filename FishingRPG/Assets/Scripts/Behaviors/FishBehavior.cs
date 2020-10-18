using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FishBehavior : MonoBehaviour
{
    public float speed = 1f;

    public Vector3 maxPos;
    public Vector3 minPos;
    public Vector3 pullLeft;
    public Vector3 pullRight;

    public float zone1;
    public float zone2;

    public int directionChoice = 0;
    public bool isDirectionChoosen = false;

    public bool extenued = false;
    public float endurance = 100f;

    public bool isAerial = false;
    public bool isOnWater = true;

    //Possiblement dans FishManager
    public float JumpHeight = 20f;      //Valeur à obtenir avec formule (stats du player contre stats du fish)
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (!isAerial)
        {
            if (PlayerManager.instance.blockLine || PlayerManager.instance.pullTowards)
            {
                SetMaxAndMinDistance();
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
                SetMaxAndMinDistance();
            }
        }
        else
        {
            isAerial = false;
            extenued = false;
            endurance = 50f;
            FishManager.instance.NotExtenued();
            rb.velocity = new Vector3(0f, JumpHeight, 0f);
        }
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
            MovingRight();
        }
        else
        {
            FishingManager.instance.fishIsGoingRight = false;
            MovingLeft();
        }
    }

    public void MovingAway()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerManager.instance.player.transform.position.x, transform.position.y, PlayerManager.instance.player.transform.position.z), -1 * speed * Time.deltaTime);
    }

    public void MovingRight()
    {
        //Debug.Log("R et Zone 1 : " + (Vector3.Distance(transform.position, new Vector3(maxPos.x, transform.position.y, maxPos.z)) + " < " + zone1));
        //Debug.Log("R et Zone 1 : " + Vector3.Distance(transform.position, new Vector3(pullRight.x, transform.position.y, pullRight.z)) + " <  0.3f");

        if (Vector3.Distance(transform.position, new Vector3(maxPos.x, transform.position.y, maxPos.z)) < 0.3f || Vector3.Distance(transform.position, new Vector3(pullRight.x, transform.position.y, pullRight.z)) < 0.3f)
        {
            ChangeDirection();
        }

        CheckTensionAndEndurance();

        CalculateSpeed();
        if (PlayerManager.instance.blockLine)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(maxPos.x, transform.position.y, maxPos.z), speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(pullRight.x, transform.position.y, pullRight.z), (speed * 2) * Time.deltaTime);
        }

    }

    public void MovingLeft()
    {
        //Debug.Log("L et Zone 1 : " + (Vector3.Distance(transform.position, new Vector3(minPos.x, transform.position.y, minPos.z)) +" < " +zone1));

        if (Vector3.Distance(transform.position, new Vector3(minPos.x, transform.position.y, minPos.z)) < 0.3f || Vector3.Distance(transform.position, new Vector3(pullLeft.x, transform.position.y, pullLeft.z)) < 0.3f)
        {
            ChangeDirection();
        }

        CheckTensionAndEndurance();

        CalculateSpeed();
        if (PlayerManager.instance.blockLine)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(minPos.x, transform.position.y, minPos.z), speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(pullLeft.x, transform.position.y, pullLeft.z), (speed * 2) * Time.deltaTime);
        }

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
    }

    public void CalculateSpeed()
    {
        if(FishingRodManager.instance.IsSameDirection())
        {
            speed = 5f + Mathf.Abs(FishingRodManager.instance.GetPlayerForce())*2;
            //Debug.Log("+ = " + (1f + Mathf.Abs(FishingRodManager.instance.GetPlayerForce())));
        }
        else
        {
            speed = 5f - Mathf.Abs(FishingRodManager.instance.GetPlayerForce())*2;
            //Debug.Log("- = " + (1f - Mathf.Abs(FishingRodManager.instance.GetPlayerForce())));
        }
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
    }

    /*public void PullTowards()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerManager.instance.player.transform.position.x, transform.position.y, PlayerManager.instance.player.transform.position.z), speed * Time.deltaTime);
    }*/

    public void CheckEndurance()
    {
        if(endurance <= 0)
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

        Vector3 cone = new Vector3(CameraManager.instance.mainCamera.transform.position.x, CameraManager.instance.mainCamera.transform.position.y - 1.5f, CameraManager.instance.mainCamera.transform.position.z);   //Cone représente le centre du cercle (Doublon avec PlayerView)

        float distance = Vector3.Distance(transform.localPosition, PlayerManager.instance.player.transform.localPosition);   //Distance Poisson-Joueur (Doublon avec PlayerView)

        maxPos     = cone + FishManager.instance.maxPosCone      ;  //Point d'intersection entre le cercle de rayon Poisson-Joueur et le côté droit du cône (Doublon avec PlayerView)
        minPos     = cone + FishManager.instance.minPosCone      ;  //Point d'intersection entre le cercle de rayon Poisson-Joueur et le côté gauche du cône (Doublon avec PlayerView)
        pullRight  = cone + (upRayRotation     * CameraManager.instance.mainCamera.transform.right * (distance - 2f));  //Pareil que maxPos mais plus proche (Direction qu'il cherche à atteindre lorsqu'on l'attire vers soi)
        pullLeft   = cone + (downRayRotation   * CameraManager.instance.mainCamera.transform.right * (distance - 2f));  //Pareil que minPos mais plus proche (Direction qu'il cherche à atteindre lorsqu'on l'attire vers soi)

        zone2 = Vector3.Distance(minPos, maxPos) * 0.2f;       //Distance à partir de laquelle le poisson perd de l'endurance
        zone1 = Vector3.Distance(minPos, maxPos) * 0.1f;       //Distance à partir de laquelle le joueur perd de la tension
    }


    /*****************
    *  Gizmos Draw  *
    *****************/
    private void OnDrawGizmosSelected()
    {
        Vector3 cone = new Vector3(CameraManager.instance.mainCamera.transform.position.x, CameraManager.instance.mainCamera.transform.position.y - 1.5f, CameraManager.instance.mainCamera.transform.position.z);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, pullRight);
        Gizmos.DrawLine(transform.position,  pullLeft);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.localPosition, maxPos);
        Gizmos.DrawLine(transform.position, minPos);

    }
}
