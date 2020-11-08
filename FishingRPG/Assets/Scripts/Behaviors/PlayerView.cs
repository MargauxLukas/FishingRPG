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

    public float bezierBobber = 1f;

    Quaternion forwardRayRotation;    //Direction tout droit

    Vector3 upRayDirection;                      //Point à droite du cône
    Vector3 downRayDirection;                      //Point à gauche du cône
    Vector3 forwardRayDirection;                      //Point le plus éloigné en face
    Vector3 downRayDirectionZ2;                      //Point à gauche du cône - 20%
    public Vector3 bezierBobberDirection;

    Vector3 RayRightFish;
    Vector3 RayLeftFish;

    public Vector3 cone;
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
        //Changera surement vu que y'a plus de cone
        if (freeCamera)
        {
            float mouseX = Input.GetAxis("Right Stick (Horizontal)") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Right Stick (Vertical)") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

        DrawCone();
    }

    public void DrawCone()
    {
        forwardRayRotation = Quaternion.AngleAxis(coneDirection, Vector3.down);               //Direction tout droit

        forwardRayDirection = forwardRayRotation * transform.right * rayRange;             //Point le plus éloigné en face
        bezierBobberDirection = forwardRayRotation * transform.right * bezierBobber;             //Bezier bobber

        Debug.Log(forwardRayDirection);

        cone = new Vector3(transform.position.x, transform.position.y - 3.25f, transform.position.z);       //Cone représente le centre du cercle       
    }

    /*************************************
     *  Gizmos pour visualiser le cône   *
     *************************************/
    void OnDrawGizmos()
    {
        /*****************
         *  Gizmos Draw  *
         *****************/
        //Droite gauche et droite du cone -10%
        Gizmos.color = Color.white;
        Gizmos.DrawLine(cone , cone + forwardRayDirection);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(cone, bezierBobberDirection);
    }
}
