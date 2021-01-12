using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingHead : MonoBehaviour
{
    public float walkingBobbingSpeed = 14f;
    public float bobbingAmount = 0.05f;
    public PlayerMovement controller;
    float normeVector;
    float defaultPosY = 0;
    float defaultPosX = 0;
    float timer = 0;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.localPosition.y;
        defaultPosX = transform.localPosition.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       

        if (Input.GetAxisRaw("Horizontal")>0.1f || Input.GetAxisRaw("Vertical")>0.1f || Input.GetAxisRaw("Horizontal") < -0.1f || Input.GetAxisRaw("Vertical")<-0.1f)
        {
            normeVector = Mathf.Sqrt(Mathf.Pow(Input.GetAxisRaw("Horizontal"),2) + Mathf.Pow(Input.GetAxisRaw("Vertical"),2));
            //Player is moving
            timer += Time.fixedDeltaTime * walkingBobbingSpeed * normeVector;

            if (Mathf.Abs(Mathf.Sin(timer)) > 0.996f) Debug.Log("Pas");
            //Debug.Log(Mathf.Abs(Mathf.Sin(timer)));
            //Debug.Log(Mathf.Abs(Mathf.Sin(Time.fixedDeltaTime * walkingBobbingSpeed * normeVector)));

            transform.localPosition = new Vector3(defaultPosX + Mathf.Sin(timer) * bobbingAmount * normeVector, defaultPosY + Mathf.Abs(Mathf.Sin(timer)) *1 * bobbingAmount * normeVector, transform.localPosition.z);
        }
        else
        {
            //Idle
            timer = 0;
            transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, defaultPosX, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.y , transform.localPosition.z);
        }


    }
}
