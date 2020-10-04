﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    Quaternion xBase;
    bool isMax = false;

    public bool result = false;

    private void Start()
    {
        xBase = transform.localRotation;
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if ((transform.rotation.eulerAngles.x > 270f || transform.rotation.eulerAngles.x == 0) && !isMax)
            {
                transform.Rotate(new Vector3(-0.5f, 0f, 0f));
            }
            else
            {
                isMax = true;
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine("ThrowBobberCoroutine");
            result = true;
            isMax = false;
        }
    }

    IEnumerator ThrowBobberCoroutine()
    {
        for(float t = 0f; t < 1f; t += 2f*Time.deltaTime)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, xBase, t);
            yield return null;
        }
        transform.localRotation = xBase;
        Debug.Log("coroutine");
    }
}

