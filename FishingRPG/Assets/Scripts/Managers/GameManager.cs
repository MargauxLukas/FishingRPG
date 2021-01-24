using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Play Sound -> launch ambiance de l'explo
        AkSoundEngine.PostEvent("AMBWind", FishManager.instance.currentFish.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
