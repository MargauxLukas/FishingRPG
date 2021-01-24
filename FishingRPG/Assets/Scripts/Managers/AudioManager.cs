using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    void Start()
    {
        //Play Sound -> launch ambiance du HUB
        AkSoundEngine.PostEvent("AMBHub", FishManager.instance.currentFish.gameObject);
    }
}
