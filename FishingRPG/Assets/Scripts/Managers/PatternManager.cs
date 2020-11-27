using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public static PatternManager instance;

    public FuiteDeLenrage fuiteDelEnrage;
    public FeinteDuFourbe feinteDuFourbe;
    public ColereDuMinuscule colereDuMinuscule;

    private void Awake()
    {
        instance = this;
    }
}
