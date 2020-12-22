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

    float rayRange;                         //Range point du millieu = poisson
    float coneDirection = 90;               //Direction dans un cercle allant de 0 à 380 Droite = 0 / Devant = 90 / Gauche = 180 / Arrière = 270 

    public float bezierBobber = 1f;

    Quaternion forwardRayRotation;    //Direction tout droit
    Vector3 forwardRayDirection;                      //Point le plus éloigné en face
    public Vector3 bezierBobberDirection;

    public Vector3 cone;

    //public GameObject test;

    private void Start()
    {
        rayRange = distance;
        bezierBobberDirection = forwardRayRotation * transform.right * bezierBobber;
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
        forwardRayRotation = Quaternion.AngleAxis(coneDirection, Vector3.down);                  //Direction tout droit

        forwardRayDirection = forwardRayRotation * transform.right * rayRange;                   //Point le plus éloigné en face
        bezierBobberDirection = forwardRayRotation * transform.right * bezierBobber;             //Bezier bobber
        cone = new Vector3(transform.position.x, transform.position.y - 3.25f, transform.position.z);       //Cone représente le centre du cercle 

        //test.transform.localPosition = cone + bezierBobberDirection;
    }

    void OnDrawGizmos()
    {
        /*****************
         *  Gizmos Draw  *
         *****************/
        Gizmos.color = Color.white;
        Gizmos.DrawLine(cone , cone + forwardRayDirection);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(cone, bezierBobberDirection);
    }
}
