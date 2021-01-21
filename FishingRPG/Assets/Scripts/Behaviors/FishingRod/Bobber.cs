using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    bool canBeLaunch = false;

    private float timer = 0f;
    public float maxTime = 2f;

    public Transform bezier1;
    public Transform bezier2;
    private Vector3 bezier3;
    //private Vector3 realBezier2;

    public List<GameObject> debugBezier = new List<GameObject>();

    public bool isDebugBezierCurve = false;
    public bool stopMovementJustOneTime = false;

    private void FixedUpdate()
    {
        //DEBUG
        if (isDebugBezierCurve)
        {
            CreateBezierCurve();
        }

        if (canBeLaunch)
        {
            timer += Time.fixedDeltaTime;

            transform.position = GetAerialPosition(timer / maxTime);

            if (timer >= maxTime)
            {
                timer = 0f;
                canBeLaunch = false;
                PlayerManager.instance.playerView.GetComponent<PlayerView>().bezierBobber = 1f;
            }
        }

        if(FishManager.instance.currentFish != null && !FishManager.instance.hasJustSpawned)
        {
            transform.position = new Vector3(FishManager.instance.currentFish.transform.position.x, FishManager.instance.currentFish.transform.position.y, FishManager.instance.currentFish.transform.position.z);
        }
    }

    public void Throw()
    {
        GetComponent<MoveToDynamic>().GameObjectToDynamics();
    }

    public Vector3 GetAerialPosition(float currentTime)
    {
        float x = Mathf.Pow(1 - currentTime, 2) * bezier1.position.x + 2 * (1 - currentTime) * currentTime * bezier2.position.x + Mathf.Pow(currentTime,2) * bezier3.x;
        float y = Mathf.Pow(1 - currentTime, 2) * bezier1.position.y + 2 * (1 - currentTime) * currentTime * bezier2.position.y + Mathf.Pow(currentTime,2) * bezier3.y;
        float z = Mathf.Pow(1 - currentTime, 2) * bezier1.position.z + 2 * (1 - currentTime) * currentTime * bezier2.position.z + Mathf.Pow(currentTime,2) * bezier3.z;
        return new Vector3(x, y, z);
    }

    public void CreateBezierCurve()
    {
        float value = 1f / debugBezier.Count;
        float distance = value;

        for(int i = 0; i < debugBezier.Count; i++)
        {
            float x = Mathf.Pow(1 - distance, 2) * bezier1.position.x + 2 * (1 - distance) * distance * bezier2.position.x + Mathf.Pow(distance,2) * bezier3.x;
            float y = Mathf.Pow(1 - distance, 2) * bezier1.position.y + 2 * (1 - distance) * distance * bezier2.position.y + Mathf.Pow(distance,2) * bezier3.y;
            float z = Mathf.Pow(1 - distance, 2) * bezier1.position.z + 2 * (1 - distance) * distance * bezier2.position.z + Mathf.Pow(distance,2) * bezier3.z;

            debugBezier[i].transform.position = new Vector3(x, y, z);
            distance += value;
        }
    }

    public void SetBezierPoint(Vector3 bezierPos3)
    {
        bezier3 = new Vector3(bezierPos3.x, bezierPos3.y -0.7f, bezierPos3.z) ;
        float x = bezier1.position.x * (1 - 0.50f) + bezier3.x * 0.50f;
        float z = bezier1.position.z * (1 - 0.50f) + bezier3.z * 0.50f;

        bezier2.position = new Vector3(x, bezier3.y + 10f, z);

        canBeLaunch = true;
    }

    public void StopMovement()
    {
        timer = 0f;
        canBeLaunch = false;
        PlayerManager.instance.playerView.GetComponent<PlayerView>().bezierBobber = 1f;
    }
}
