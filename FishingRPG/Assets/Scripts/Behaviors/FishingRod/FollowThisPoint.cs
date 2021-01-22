using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThisPoint : MonoBehaviour
{
    public Transform pointToFollow;

    // Update is called once per frame
    void Update()
    {
        transform.position = pointToFollow.gameObject.GetComponent<SkinnedMeshRenderer>().bounds.center;
    }
}
