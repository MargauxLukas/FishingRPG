using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    public float speed = 2f;
    void Update()
    {
        if(PlayerManager.instance.blockLine)
        {
            
        }
        else if(PlayerManager.instance.pullTowards)
        {
            
        }
        else
        {
            MovingAway();
        }
    }

    public void MovingAway()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerManager.instance.player.transform.position.x, transform.position.y, PlayerManager.instance.player.transform.position.z), -1 * speed * Time.deltaTime);
    }
}
