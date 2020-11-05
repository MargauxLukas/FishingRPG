using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    Quaternion FishingRodRotaBase;
    bool isMax = false;
    public float timeBeforeLaunch = 0.2f;

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
            isMax = false;
        }

        if (Input.GetButton("B Button") && !FishingRodManager.instance.bobberThrowed)
        {
            if ((transform.rotation.eulerAngles.x > 270f || transform.rotation.eulerAngles.x == 0) && !isMax)
            {
                transform.Rotate(new Vector3(-1f, 0f, 0f));
                PlayerManager.instance.playerView.GetComponent<PlayerView>().bezierBobber += 0.3f;
            }
            else
            {
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
}

