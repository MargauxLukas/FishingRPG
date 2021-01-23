using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPatterns : MonoBehaviour
{
    private List<FishyPattern> fishyCalmPatterns = new List<FishyPattern>();
    private List<FishyPattern> fishyRagePatterns = new List<FishyPattern>();

    public FishyPattern currentPattern = null;

    private int totalPrioritiesValuesCalm = 0;
    private int totalPrioritiesValuesRage = 0;

    private float timer = 0f;

    private int randValue = 0;

    private int iTempo = 0;

    void Start()
    {
        for(iTempo = 0; iTempo < FishManager.instance.currentFishBehavior.fishyFiche.calmPatterns.Length; iTempo++)
        {
            fishyCalmPatterns.Add(FishManager.instance.currentFishBehavior.fishyFiche.calmPatterns[iTempo]);
            totalPrioritiesValuesCalm += FishManager.instance.currentFishBehavior.fishyFiche.calmPatterns[iTempo].priorityCalm;
        }

        for (iTempo = 0; iTempo < FishManager.instance.currentFishBehavior.fishyFiche.ragePatterns.Length; iTempo++)
        {
            fishyRagePatterns.Add(FishManager.instance.currentFishBehavior.fishyFiche.ragePatterns[iTempo]);
            totalPrioritiesValuesRage += FishManager.instance.currentFishBehavior.fishyFiche.ragePatterns[iTempo].priorityRage;
        }
    }

    private void FixedUpdate()
    {
        if(currentPattern != null)
        {
            PlayPattern(currentPattern.name);
        }
    }

    public void startPattern(bool rage)
    {
        if(rage)
        {
            ChooseRagePattern();
        }
        else
        {
            ChooseCalmPattern();
        }
    }

    public void ChooseCalmPattern()
    {
        randValue = RandomNumber(totalPrioritiesValuesCalm);

        for(iTempo = 0; iTempo < FishManager.instance.currentFishBehavior.fishyFiche.calmPatterns.Length; iTempo++)
        {
            if(randValue < FishManager.instance.currentFishBehavior.fishyFiche.calmPatterns[iTempo].priorityCalm)
            {
                currentPattern = FishManager.instance.currentFishBehavior.fishyFiche.calmPatterns[iTempo];
                break;
            }
            else
            {
                randValue -= FishManager.instance.currentFishBehavior.fishyFiche.calmPatterns[iTempo].priorityCalm;
            }
        }
    }

    public void ChooseRagePattern()
    {
        randValue = RandomNumber(totalPrioritiesValuesRage);

        for (iTempo = 0; iTempo < FishManager.instance.currentFishBehavior.fishyFiche.ragePatterns.Length; iTempo++)
        {
            if (randValue < FishManager.instance.currentFishBehavior.fishyFiche.ragePatterns[iTempo].priorityCalm)
            {
                currentPattern = FishManager.instance.currentFishBehavior.fishyFiche.ragePatterns[iTempo];
                break;
            }
            else
            {
                randValue -= FishManager.instance.currentFishBehavior.fishyFiche.ragePatterns[iTempo].priorityCalm;
            }
        }
    }

    public int RandomNumber(int maxValue)
    {
        return Random.Range(1, maxValue + 1);
    }

    public void PlayPattern(string name)
    {
        timer += Time.fixedDeltaTime;
        if (currentPattern.duration < timer || currentPattern.duration == 0)
        {
            switch (name)
            {
                case "FuiteDeLEnrage":
                    FuiteDeLenrage.Play(currentPattern.DOTFrequency, currentPattern.energyCost, currentPattern.costEnergyOverTime);
                    break;

                case "FeinteDuFourbe":
                    FeinteDuFourbe.Play(currentPattern.DOTFrequency, currentPattern.energyCost, currentPattern.costEnergyOverTime);
                    break;

                case "ColereDuMinuscule":
                    ColereDuMinuscule.Play(currentPattern.energyCost);
                    break;
            }
        }
        else
        {
            ResetPattern();
        }
    }

    public void ResetPattern()
    {
        currentPattern = null;
        FishManager.instance.currentFishBehavior.ResetStats();
        FishManager.instance.currentFishBehavior.idleTimer = 0f;
        FishManager.instance.currentFishBehavior.isIdle = true;
    }

    public void ResetOncePlay()
    {
        FuiteDeLenrage.ResetPlayOnce();
        FeinteDuFourbe.ResetPlayOnce();
        ResetPattern();
    }
}
