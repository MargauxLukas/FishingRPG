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
    public float qApplicatedForceBalancing;

    [Range(0f, 1f)]
    public float qPercentOfMaxTimeBalancing = 0.85f;
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

    //Force appliqué du Joueur sur le poisson quand CP > FCurrent
    public float GetApplicatedForce()
    {
        return ((FishingRodManager.instance.distanceCP - FishingRodManager.instance.fishingLine.fCurrent) / FishingRodManager.instance.fishingLine.fCritique) * (PlayerManager.instance.playerStats.strenght / FishManager.instance.currentFishBehavior.fishyFiche.weight) * qApplicatedForceBalancing;
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

    public float GetTimingForMoreAerial()
    {
        return (1 - qPercentOfMaxTimeBalancing) * (PlayerManager.instance.playerStats.dexterity / FishManager.instance.currentFishBehavior.fishyFiche.agility);
    }
}
