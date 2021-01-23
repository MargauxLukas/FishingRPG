using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThisPoint : MonoBehaviour
{
    public Transform pointToFollow;
    //public Transform arm;

    // Update is called once per frame
    void Update()
    {
        transform.position = pointToFollow.gameObject.GetComponent<SkinnedMeshRenderer>().bounds.center;
        //arm.transform.localRotation = new Quaternion(FishingRodManager.instance.fishingRodGameObject.transform.localRotation.x, 0f, 0f, 0f);
    }
}
