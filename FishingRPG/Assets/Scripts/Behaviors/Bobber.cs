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
    private Vector3 realBezier2;

    public List<GameObject> debugBezier = new List<GameObject>();

    public bool isDebugBezierCurve = false;

    private void Update()
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

        if(FishManager.instance.currentFish != null)
        {
            transform.position = new Vector3(FishManager.instance.currentFish.transform.position.x, transform.position.y, FishManager.instance.currentFish.transform.position.z);
        }
    }

    public void Throw()
    {
        GetComponent<MoveToDynamic>().GameObjectToDynamics();
        bezier3 = PlayerManager.instance.playerView.GetComponent<PlayerView>().bezierBobberDirection + PlayerManager.instance.playerView.GetComponent<PlayerView>().cone;
        canBeLaunch = true;
    }

    public Vector3 GetAerialPosition(float currentTime)
    {
        /*capsule1Debug.transform.position = bezier1.position;
        capsule2Debug.transform.position = realBezier2;
        capsule3Debug.transform.position = bezier3;*/

        float x = Mathf.Pow(1 - currentTime, 2) * bezier1.position.x + 2 * (1 - currentTime) * currentTime * realBezier2.x + Mathf.Pow(currentTime,2) * bezier3.x;
        float y = Mathf.Pow(1 - currentTime, 2) * bezier1.position.y + 2 * (1 - currentTime) * currentTime * realBezier2.y + Mathf.Pow(currentTime,2) * bezier3.y;
        float z = Mathf.Pow(1 - currentTime, 2) * bezier1.position.z + 2 * (1 - currentTime) * currentTime * realBezier2.z + Mathf.Pow(currentTime,2) * bezier3.z;
        return new Vector3(x, y, z);
    }

    public void CreateBezierCurve()
    {
        float value = 1f / debugBezier.Count;
        float distance = value;

        for(int i = 0; i < debugBezier.Count; i++)
        {
            float x = Mathf.Pow(1 - distance, 2) * bezier1.position.x + 2 * (1 - distance) * distance * realBezier2.x + Mathf.Pow(distance,2) * bezier3.x;
            float y = Mathf.Pow(1 - distance, 2) * bezier1.position.y + 2 * (1 - distance) * distance * realBezier2.y + Mathf.Pow(distance,2) * bezier3.y;
            float z = Mathf.Pow(1 - distance, 2) * bezier1.position.z + 2 * (1 - distance) * distance * realBezier2.z + Mathf.Pow(distance,2) * bezier3.z;

            debugBezier[i].transform.position = new Vector3(x, y, z);
            distance += value;
        }
    }

    public void SetSecondBezierPoint()
    {
        realBezier2 = bezier2.position;
    }
}
