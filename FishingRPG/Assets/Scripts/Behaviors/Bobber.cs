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

    private void Update()
    {
        if (canBeLaunch)
        {
            timer += Time.deltaTime;

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
        bezier3 = PlayerManager.instance.playerView.GetComponent<PlayerView>().bezierBobberDirection;
        canBeLaunch = true;
    }

    public Vector3 GetAerialPosition(float currentTime)
    {
        float x = Mathf.Pow(1 - currentTime, 2) * bezier1.position.x + 2 * (1 - currentTime) * currentTime * bezier2.position.x + currentTime * (PlayerManager.instance.playerView.GetComponent<PlayerView>().cone.x + bezier3.x);
        float y = Mathf.Pow(1 - currentTime, 2) * bezier1.position.y + 2 * (1 - currentTime) * currentTime * bezier2.position.y + currentTime * (PlayerManager.instance.playerView.GetComponent<PlayerView>().cone.y + bezier3.y);
        float z = Mathf.Pow(1 - currentTime, 2) * bezier1.position.z + 2 * (1 - currentTime) * currentTime * bezier2.position.z + currentTime * (PlayerManager.instance.playerView.GetComponent<PlayerView>().cone.z + bezier3.z);
        return new Vector3(x, y, z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, bezier1.position);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, bezier2.position);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, bezier3);
    }
}
