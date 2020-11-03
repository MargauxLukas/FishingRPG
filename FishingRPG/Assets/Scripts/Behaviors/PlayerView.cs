using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerView : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    float xRotation = 0f;

    public Transform playerBody;
    public bool freeCamera = true;

    float distance = 30f;
    float distanceFtoP = 200f;

    float angle = 30.0f;                    //Angle voulu
    float angleZ1;                          //Angle - 10% = Zone ou tension augmente
    float angleZ2;                          //Angle - 20% = Zone ou Endurance du poisson diminue
    float rayRange;                         //Range point du millieu = poisson
    float halfFOV;                          //FieldOfView 
    float halfFOVZ1;                        //FieldOfView - 10%
    float halfFOVZ2;                        //FieldOfView - 20%
    float coneDirection = 90;               //Direction dans un cercle allant de 0 à 380 Droite = 0 / Devant = 90 / Gauche = 180 / Arrière = 270 

    Quaternion upRayRotation;    //Direction à droite du cône
    Quaternion downRayRotation;    //Direction à gauche du cône
    Quaternion forwardRayRotation;    //Direction tout droit
    Quaternion upRayRotationZ1;    //Direction à droite - 10%
    Quaternion downRayRotationZ1;    //Direction à gauche - 10%
    Quaternion upRayRotationZ2;    //Direction à droite - 20%
    Quaternion downRayRotationZ2;    //Direction à gauche - 20%

    Vector3 upRayDirection;                      //Point à droite du cône
    Vector3 downRayDirection;                      //Point à gauche du cône
    Vector3 forwardRayDirection;                      //Point le plus éloigné en face
    Vector3 upRayDirectionZ1;                      //Point à droite du cône - 10%
    Vector3 downRayDirectionZ1;                      //Point à gauche du cône - 10%
    Vector3 upRayDirectionZ2;                      //Point à droite du cône - 20%
    Vector3 downRayDirectionZ2;                      //Point à gauche du cône - 20%

    Vector3 RayRightFish;
    Vector3 RayLeftFish;

    Vector3 cone;
    Vector3 midPoint;

    private void Start()
    {
        angleZ1 = angle * 0.9f;
        angleZ2 = angle * 0.8f;
        rayRange = distance;
        halfFOV = angle / 1.0f;
        halfFOVZ1 = angleZ1 / 1.0f;
        halfFOVZ2 = angleZ2 / 1.0f;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        DrawCone();

        if (freeCamera)
        {
            float mouseX = Input.GetAxis("Right Stick (Horizontal)") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Right Stick (Vertical)") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void DrawCone()
    {      
            upRayRotation = Quaternion.AngleAxis(-halfFOV + coneDirection, Vector3.down);    //Direction à droite du cône
            downRayRotation = Quaternion.AngleAxis(halfFOV + coneDirection, Vector3.down);    //Direction à gauche du cône
            forwardRayRotation = Quaternion.AngleAxis(coneDirection, Vector3.down);    //Direction tout droit
            upRayRotationZ1 = Quaternion.AngleAxis(-halfFOVZ1 + coneDirection, Vector3.down);    //Direction à droite - 10%
            downRayRotationZ1 = Quaternion.AngleAxis(halfFOVZ1 + coneDirection, Vector3.down);    //Direction à gauche - 10%
            upRayRotationZ2 = Quaternion.AngleAxis(-halfFOVZ2 + coneDirection, Vector3.down);    //Direction à droite - 20%
            downRayRotationZ2 = Quaternion.AngleAxis(halfFOVZ2 + coneDirection, Vector3.down);    //Direction à gauche - 20%

            upRayDirection = upRayRotation * transform.right * rayRange;                      //Point à droite du cône
            downRayDirection = downRayRotation * transform.right * rayRange;                      //Point à gauche du cône
            forwardRayDirection = forwardRayRotation * transform.right * rayRange;                      //Point le plus éloigné en face
            upRayDirectionZ1 = upRayRotationZ1 * transform.right * rayRange;                      //Point à droite du cône - 10%
            downRayDirectionZ1 = downRayRotationZ1 * transform.right * rayRange;                      //Point à gauche du cône - 10%
            upRayDirectionZ2 = upRayRotationZ2 * transform.right * rayRange;                      //Point à droite du cône - 20%
            downRayDirectionZ2 = downRayRotationZ2 * transform.right * rayRange;                      //Point à gauche du cône - 20%

            cone = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);       //Cone représente le centre du cercle

            //Utilitaire au cas ou 
            midPoint = (upRayDirection + downRayDirection) / 2;                                       //Milieu du point le plus à droite et du point le plus à gauche

            if (FishingManager.instance.currentFish != null && !FishManager.instance.isAerial)
            {
                if (Vector3.Distance(new Vector3(FishingManager.instance.currentFish.transform.localPosition.x, transform.position.y - 1.5f, FishingManager.instance.currentFish.transform.localPosition.z), cone) < distanceFtoP)
                {
                    distanceFtoP = Vector3.Distance(new Vector3(FishingManager.instance.currentFish.transform.localPosition.x, transform.position.y - 1.5f, FishingManager.instance.currentFish.transform.localPosition.z), cone);   //JP
                }
                PlayerManager.instance.distancePlayerView = distanceFtoP;

                float radian = (angle / 2) * Mathf.Deg2Rad;

                float fishToCone = Mathf.Tan(radian) * distanceFtoP;                                                 //PA

                float normalizedFishToPlayer = Mathf.Sqrt(Mathf.Pow(distanceFtoP, 2) + Mathf.Pow(fishToCone, 2));    //JA

                RayRightFish = upRayRotation * transform.right * (normalizedFishToPlayer * 1.109f);       //Point d'intersection entre le cercle de rayon Poisson-Joueur et le côté droit du cône 
                RayLeftFish  = downRayRotation * transform.right * (normalizedFishToPlayer * 1.109f);       //Point d'intersection entre le cercle de rayon Poisson-Joueur et le côté gauche du cône 

                if (distance > normalizedFishToPlayer * 1.109f)
                {
                    distance = normalizedFishToPlayer * 1.109f;
                }

                FishManager.instance.maxPosCone = RayRightFish;
                FishManager.instance.minPosCone = RayLeftFish;
            }
        
    }

    /*************************************
     *  Gizmos pour visualiser le cône   *
     *************************************/
    void OnDrawGizmos()
    {
        /*****************
         *  Gizmos Draw  *
         *****************/
        //Droite gauche et droite du cone
        Gizmos.DrawRay(cone, upRayDirection);
        Gizmos.DrawRay(cone, downRayDirection);
        Gizmos.DrawRay(cone, midPoint);  //UTILITAIRE
        Gizmos.DrawLine(cone + downRayDirection, cone + upRayDirection); //Droite allant de l'extrémité droit à l'extrémité gauche du cône

        //Droite gauche et droite du cone -10%
        Gizmos.color = Color.red;
        Gizmos.DrawRay(cone, upRayDirectionZ1);
        Gizmos.DrawRay(cone, downRayDirectionZ1);
        Gizmos.DrawLine(cone + upRayDirection, cone + forwardRayDirection);
        Gizmos.DrawLine(cone + downRayDirection, cone + forwardRayDirection);

        //Droite gauche et droite du cone -20%
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(cone, upRayDirectionZ2);
        Gizmos.DrawRay(cone, downRayDirectionZ2);
        Gizmos.DrawLine(cone + upRayDirection, cone + (midPoint + forwardRayDirection) / 2);
        Gizmos.DrawLine(cone + downRayDirection, cone + (midPoint + forwardRayDirection) / 2);

        Gizmos.color = Color.green;
        //Distance du poisson et du joueur retranscrit sur le cône droit et gauche
        if (FishingManager.instance != null)
        {
            if (FishingManager.instance.currentFish != null)
            {
                Gizmos.DrawLine(new Vector3(FishingManager.instance.currentFish.transform.localPosition.x, transform.position.y - 1.5f, FishingManager.instance.currentFish.transform.localPosition.z), cone);

                Gizmos.DrawLine(cone + RayRightFish, cone + RayLeftFish);                     //Droite déssiné du point RayRightFish à RayLeftFish                                                                               
            }
        }
    }
}
