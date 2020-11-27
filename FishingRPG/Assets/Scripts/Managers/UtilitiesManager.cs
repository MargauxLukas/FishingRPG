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

    public float bHeightAerialBalancing;
    public float cHeightAerialBalancing;

    public float qTimeAerialBalancing;
    public float yTimeAerialBalancing;

    public float qTimeFellingAerialBalancing;

    public float qFellingDamageBalancing;
    [System.NonSerialized]
    public float fellingDamage;

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
        return ((FishingRodManager.instance.distanceCP - FishingRodManager.instance.fishingLine.fCurrent) / FishingRodManager.instance.fishingLine.fCritique) * (FishManager.instance.currentFishBehavior.strength / PlayerManager.instance.playerStats.constitution) * qTensionLossBalancing;
    }

    //Perte d'endurance par seconde (float) * multiplicateur
    public float GetLossEnduranceNumberTakingLine()
    {
        return (((FishingRodManager.instance.distanceCP - FishingRodManager.instance.fishingLine.fCurrent) / FishingRodManager.instance.fishingLine.fCritique) * qEnduranceLossBalancing) * qMultiplicatorBalancing;
    }

    //Perte de Tension par seconde (Float) * multiplicateur
    public float GetLossTensionNumberTakingLine()
    {
        return (((FishingRodManager.instance.distanceCP - FishingRodManager.instance.fishingLine.fCurrent) / FishingRodManager.instance.fishingLine.fCritique) * (FishManager.instance.currentFishBehavior.strength / PlayerManager.instance.playerStats.constitution) * qTensionLossBalancing) * qMultiplicatorBalancing;
    }


    //Timing (en seconde) pour renvoyer le poisson en l’air (aussi appelé “moment critique”, noté Tmc, float strictement positif)

    public float GetTimingForMoreAerial()
    {
        return (1 - qPercentOfMaxTimeBalancing) * (PlayerManager.instance.playerStats.dexterity / FishManager.instance.currentFishBehavior.fishyFiche.agility);
    }

    //Hauteur de projection du poisson dans les airs (notée Hpp, float strictement positif) [Déso par avance]
    public float GetHeightMaxForAerial(float heightMax)
    {
        return (heightMax - heightMax / 4) * (float)System.Math.Tanh(bHeightAerialBalancing * (PlayerManager.instance.playerStats.strenght / FishManager.instance.currentFishBehavior.fishyFiche.weight) - cHeightAerialBalancing) + (heightMax + heightMax / 4);
    }

    //Durée de la projection dans les airs(notée Dpp, float strictement positif)
    public float GetTimeAerial(float heigthMax, int nbRebond)
    {
        return GetHeightMaxForAerial(heigthMax) * (qTimeAerialBalancing / 4) * Mathf.Pow(yTimeAerialBalancing, nbRebond);
    }

    //Temps de chute quand Abattement activé (notée Tca, float strictement positif)

    public float GetTimeFellingAerial(float maxTime, float currentFishHeight, float maxHeight)
    {
        //Tmax = Temps pour faire l'aerial normalement
        //HauP = Hauteur du poisson au moment ou LB appuyé
        //Hmax = Hauteur Max normalement

        fellingDamage = (currentFishHeight/maxHeight) * (PlayerManager.instance.playerStats.dexterity / FishManager.instance.currentFishBehavior.fishyFiche.agility) * qFellingDamageBalancing;
        return (maxTime / (2 * qTimeFellingAerialBalancing)) * (currentFishHeight / maxHeight);
    }

    //Dégat lors du claquage (Calculer lors du GetTimeFellingAerial pour éviter de devoir récupérer les variables de hauteurs plusieurs fois)
    public float GetFellingDamage()
    {
        return fellingDamage;
    }
}
