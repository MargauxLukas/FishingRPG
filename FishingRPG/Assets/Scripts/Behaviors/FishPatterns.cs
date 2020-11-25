using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPatterns : MonoBehaviour
{
    private List<FishyPattern> fishyCalmPatterns = new List<FishyPattern>();
    private List<FishyPattern> fishyRagePatterns = new List<FishyPattern>();

    private int totalValues = 0;

    void Start()
    {
        for(int i = 0; i < FishManager.instance.currentFishBehavior.fishyFiche.calmPatterns.Length;i++)
        {
            fishyCalmPatterns.Add(FishManager.instance.currentFishBehavior.fishyFiche.calmPatterns[i]);
        }

        for (int i = 0; i < FishManager.instance.currentFishBehavior.fishyFiche.ragePatterns.Length; i++)
        {
            fishyRagePatterns.Add(FishManager.instance.currentFishBehavior.fishyFiche.ragePatterns[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //Ici je regarde quel patterns est à utiliser
    //Je l'ai utilise
    //Je retourne en IDLE
}
