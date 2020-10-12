using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FishBehavior : MonoBehaviour
{
    public float speed = 4f;
    public Vector3 maxPos;
    public Vector3 minPos;
    public Vector3 pullLeft;
    public Vector3 pullRight;

    public int directionChoice = 0;
    public bool isDirectionChoosen = false;

    void Update()
    {
        if (PlayerManager.instance.blockLine || PlayerManager.instance.pullTowards)
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
        Move();
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
        Debug.Log(Vector3.Distance(transform.position, new Vector3(pullRight.x, transform.position.y, pullRight.z)));
        if (Vector3.Distance(transform.position, new Vector3(maxPos.x, transform.position.y, maxPos.z)) < 0.3f || Vector3.Distance(transform.position, new Vector3(pullRight.x, transform.position.y, pullRight.z)) < 0.3f)
        {
            ChangeDirection();
        }
        else
        {
            if (PlayerManager.instance.blockLine)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(maxPos.x, transform.position.y, maxPos.z), (speed * 2) * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(pullRight.x, transform.position.y, pullRight.z), (speed * 3f) * Time.deltaTime);
            }
        }
    }

    public void MovingLeft()
    {
        Debug.Log(Vector3.Distance(transform.position, new Vector3(pullLeft.x, transform.position.y, pullLeft.z)));
        if (Vector3.Distance(transform.position, new Vector3(minPos.x, transform.position.y, minPos.z)) < 0.3f || Vector3.Distance(transform.position, new Vector3(pullLeft.x, transform.position.y, pullLeft.z)) < 0.3f)
        {
            ChangeDirection();
        }
        else
        {
            if (PlayerManager.instance.blockLine)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(minPos.x, transform.position.y, minPos.z), (speed * 2) * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(pullLeft.x, transform.position.y, pullLeft.z), (speed * 3f) * Time.deltaTime);
            }
        }
    }

    public void ChangeDirection()
    {
        SetMaxAndMinDistance();
        if (directionChoice == 1)
        {
            directionChoice = 2;
        }
        else
        {
            directionChoice = 1;
        }
    }

    /*public void PullTowards()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerManager.instance.player.transform.position.x, transform.position.y, PlayerManager.instance.player.transform.position.z), speed * Time.deltaTime);
    }*/

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
        pullRight  = cone + (upRayRotation * PlayerManager.instance.player.transform.right * (distance - 2f));
        pullLeft = cone + (downRayRotation * PlayerManager.instance.player.transform.right * (distance - 2f));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, pullRight);
        Gizmos.DrawLine(transform.position, pullLeft);
    }
}
