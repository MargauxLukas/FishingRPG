using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilitiesManager : MonoBehaviour
{
    public static UtilitiesManager instance;

    public float getVplBalancing;
    public float getVpjBalancing;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    //Vitesse du poisson quand il est libre
    public float GetVpL(float fishSpeed)
    {
        return fishSpeed * getVplBalancing;
    }

    //Impact du joueur sur les déplacements horizontaux du poisson
    public float GetVpj(float fishPosition, float playerPosition, float fishWeight)
    {
        //Debug.Log("VPJ = " + Mathf.Abs(playerPosition - fishPosition) * (PlayerManager.instance.playerStats.strenght / fishWeight) * getVpjBalancing);
        return Mathf.Abs(playerPosition-fishPosition) * (PlayerManager.instance.playerStats.strenght/fishWeight) * getVpjBalancing;
    }
    
    //Déplacement du poisson au final
    public float GetVpF(float speedVpL, float fishDistance, float playerDistance, float fishWeight, int valueDirection)
    {
        return speedVpL * valueDirection + GetVpj(fishDistance, playerDistance, fishWeight);
    }
}
