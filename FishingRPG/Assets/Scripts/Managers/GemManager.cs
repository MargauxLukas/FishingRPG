using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    public static GemManager instance;

    public FirstGem firstGem;

    void Start()
    {
        instance = this;
    }
}
