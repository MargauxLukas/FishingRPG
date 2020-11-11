using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilitiesManager : MonoBehaviour
{
    public static UtilitiesManager instance;

    public float getFishSpeedBalancing;

    public float qEnduranceLossBalancing;
    public float qTensionLossBalancing;
    public float qMultiplicatorBalancing;
    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    //Vitesse du poisson : Agilité * q
    public float GetFishSpeed(float fishAgility)
    {
        return fishAgility * getFishSpeedBalancing;
    }

    //Perte d'endurance par seconde (Float)
    public float GetLossEnduranceNumber()
    {
        return ((FishingRodManager.instance.distanceCP - FishingRodManager.instance.fishingLine.fCurrent) / FishingRodManager.instance.fishingLine.fCritique) * qEnduranceLossBalancing;
    }

    //Perte de Tension par seconde (Float)
    public float GetLossTensionNumber()
    {
        return ((FishingRodManager.instance.distanceCP - FishingRodManager.instance.fishingLine.fCurrent) / FishingRodManager.instance.fishingLine.fCritique) * (FishManager.instance.currentFishBehavior.fishyFiche.strength / PlayerManager.instance.playerStats.constitution) * qTensionLossBalancing;
    }

    //Perte d'endurance par seconde (float) * multiplicateur
    public float GetLossEnduranceNumberTakingLine()
    {
        return (((FishingRodManager.instance.distanceCP - FishingRodManager.instance.fishingLine.fCurrent) / FishingRodManager.instance.fishingLine.fCritique) * qEnduranceLossBalancing) * qMultiplicatorBalancing;
    }

    //Perte de Tension par seconde (Float) * multiplicateur
    public float GetLossTensionNumberTakingLine()
    {
        return (((FishingRodManager.instance.distanceCP - FishingRodManager.instance.fishingLine.fCurrent) / FishingRodManager.instance.fishingLine.fCritique) * (FishManager.instance.currentFishBehavior.fishyFiche.strength / PlayerManager.instance.playerStats.constitution) * qTensionLossBalancing) * qMultiplicatorBalancing;
    }
}
