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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

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

    void OnDrawGizmosSelected()
    {
        float angle = 20.0f;
        float rayRange = 30.0f;
        float halfFOV = angle/1.0f;
        float coneDirection = 90;

        Quaternion upRayRotation = Quaternion.AngleAxis(-halfFOV + coneDirection, Vector3.down);
        Quaternion downRayRotation = Quaternion.AngleAxis(halfFOV + coneDirection, Vector3.down);

        Vector3 upRayDirection = upRayRotation * transform.right * rayRange;
        Vector3 downRayDirection = downRayRotation * transform.right * rayRange;

        Vector3 cone = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);

        Gizmos.DrawRay(cone, upRayDirection);
        Gizmos.DrawRay(cone, downRayDirection);
        Gizmos.DrawLine(cone + downRayDirection, cone + upRayDirection);

        if(FishingManager.instance.currentFish != null)
        {
            float distance = Vector3.Distance(FishingManager.instance.currentFish.transform.position , PlayerManager.instance.player.transform.position);
            //Debug.Log(distance);
            Vector3 RayRightFish = upRayRotation * transform.right * distance;
            Vector3 RayLeftFish = downRayRotation * transform.right * distance;

            Gizmos.DrawLine(cone + RayRightFish, cone + RayLeftFish);
        }
    }
}
