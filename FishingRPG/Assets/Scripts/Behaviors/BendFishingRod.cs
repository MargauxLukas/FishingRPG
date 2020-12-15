using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class BendFishingRod : MonoBehaviour
{
    public Transform basePosition;
    public TwoBoneIKConstraint tbIK;

    public float maxBendable = 0.5f;

    private float valuePerFloat = 0f;
    private float valuePos = 0f;

    void Update()
    {
        if (FishingManager.instance.currentFish != null)
        {
            //transform.LookAt(FishingManager.instance.currentFish.transform.position);
            basePosition.transform.LookAt(FishingManager.instance.currentFish.transform.position);

            CalculPosTarget();
            tbIK.weight = valuePos;
            transform.position = FishingManager.instance.currentFish.transform.position;        
        }    
    }

    public void SetupValuePerFloat()
    {
        valuePerFloat = maxBendable / FishingRodManager.instance.fishingLine.fCritique;
    }

    public void CalculPosTarget()
    {
        //Debug.Log(valuePerFloat + " * " +(FishingRodManager.instance.distanceCP - FishingRodManager.instance.fishingLine.fCurrent));
        valuePos = valuePerFloat * (FishingRodManager.instance.distanceCP - FishingRodManager.instance.fishingLine.fCurrent);
    }

    public void ResetBendable()
    {
        transform.position = basePosition.position;
    }
}
