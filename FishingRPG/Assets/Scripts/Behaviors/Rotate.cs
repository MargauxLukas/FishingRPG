using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    Quaternion FishingRodRotaBase;
    [HideInInspector] public bool isMax = false;

    public bool result = false;

    private void Start()
    {
        FishingRodRotaBase = transform.localRotation;
    }

    void Update()
    {
        if (Input.GetButtonUp("B Button") && !FishingRodManager.instance.bobberThrowed)
        {
            StartCoroutine("Throw");
            FishingRodManager.instance.bobber.GetComponent<Bobber>().SetSecondBezierPoint();
            Debug.Log("isMax false");
            isMax = false;
        }

        if (Input.GetButton("B Button") && !FishingRodManager.instance.bobberThrowed)
        {
            Debug.Log(transform.rotation.eulerAngles.x + " > 270f || " + transform.rotation.eulerAngles.x + " == 0");
            if ((transform.localRotation.eulerAngles.x > 270f || (transform.localRotation.eulerAngles.x >= 0 && transform.localRotation.eulerAngles.x < 1)) && !isMax)
            {
                transform.Rotate(new Vector3(-1f, 0f, 0f));
                PlayerManager.instance.playerView.GetComponent<PlayerView>().bezierBobber += 0.3f;
            }
            else
            {
                Debug.Log("isMax true");
                isMax = true;
            }
        }
        else if(Input.GetButtonUp("B Button") && FishingRodManager.instance.bobberThrowed)
        {
            FishingManager.instance.CancelFishing();
            FishingRodManager.instance.bobberThrowed = false;
        }
    }

    IEnumerator Throw()
    {
        for (float t = 0f; t < 1f; t += 2f * Time.deltaTime)
        {
            if(t > 0.25f)
            {
                result = true;
            }
            transform.localRotation = Quaternion.Lerp(transform.localRotation, FishingRodRotaBase, t);
            yield return null;
        }
        transform.localRotation = FishingRodRotaBase;
    }

    public void ResetRotation()
    {
        transform.localRotation = Quaternion.Euler(360, 0, 0);
        transform.parent.transform.localPosition = new Vector3(0f, 0.5f, 0.5f);
        transform.parent.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}

