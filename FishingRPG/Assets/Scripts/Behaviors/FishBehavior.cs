using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FishBehavior : MonoBehaviour
{
    public float speed = 4f;
    public Vector3 maxPos;
    public Vector3 minPos;

    public int directionChoice = 0;
    public bool isDirectionChoosen = false;

    void Update()
    {
        if (PlayerManager.instance.blockLine)
        {
            if (!isDirectionChoosen)
            {
                MovingRightOrLeft();
            }
            else
            {
                Move();
            }
        }
        else if(PlayerManager.instance.pullTowards)
        {
            if (!isDirectionChoosen)
            {
                MovingRightOrLeft();
            }
            else
            {
                Move();
            }
        }
        else
        {
            MovingAway();
            SetMaxAndMinDistance();
        }
    }

    public void MovingRightOrLeft()
    {
        directionChoice = Random.Range(0, 2);

        if (directionChoice == 1)
        {
            MovingRight();
        }
        else
        {
            MovingLeft();
        }
        isDirectionChoosen = true;
    }

    public void Move()
    {
        if (directionChoice == 1)
        {
            MovingRight();
        }
        else
        {
            MovingLeft();
        }
    }

    public void MovingAway()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerManager.instance.player.transform.position.x, transform.position.y, PlayerManager.instance.player.transform.position.z), -1 * speed * Time.deltaTime);
    }

    public void MovingRight()
    {
        Debug.Log("Right :" + Vector3.Distance(transform.position, new Vector3(maxPos.x, transform.position.y, maxPos.z)));
        if (Vector3.Distance(transform.position, new Vector3(maxPos.x, transform.position.y, maxPos.z)) < 0.2f)
        {
            ChangeDirection();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(maxPos.x, transform.position.y, maxPos.z), (speed * 2) * Time.deltaTime);
        }
    }

    public void MovingLeft()
    {
        Debug.Log("Left :" + Vector3.Distance(transform.position, new Vector3(minPos.x, transform.position.y, minPos.z)));
        if (Vector3.Distance(transform.position, new Vector3(minPos.x, transform.position.y, minPos.z)) < 0.2f)
        {
            ChangeDirection();
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(minPos.x, transform.position.y, minPos.z), (speed*2) * Time.deltaTime);
    }

    public void ChangeDirection()
    {
        if(directionChoice == 1)
        {
            directionChoice = 2;
        }
        else
        {
            directionChoice = 1;
        }
    }

    public void SetMaxAndMinDistance()
    {
        float angle = 20.0f;
        float halfFOV = angle / 1.0f;
        float coneDirection = 90;

        Quaternion upRayRotation = Quaternion.AngleAxis(-halfFOV + coneDirection, Vector3.down);
        Quaternion downRayRotation = Quaternion.AngleAxis(halfFOV + coneDirection, Vector3.down);
        Vector3 cone = new Vector3(PlayerManager.instance.player.transform.position.x, PlayerManager.instance.player.transform.position.y - 1.5f, PlayerManager.instance.player.transform.position.z);

        float distance = Vector3.Distance(transform.position, PlayerManager.instance.player.transform.position);
        maxPos = cone +( upRayRotation * PlayerManager.instance.player.transform.right * distance);
        minPos = cone + (downRayRotation * PlayerManager.instance.player.transform.right * distance);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, PlayerManager.instance.player.transform.position);
    }
}
