using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSwirls : MonoBehaviour
{
    public List<GameObject> swirlsSnap = new List<GameObject>();
    public List<GameObject> swirlsReef = new List<GameObject>();

    public int nbSwirls = 2;
    private int firstRandomNumberSnap;
    private int secondRandomNumberSnap;

    private int firstRandomNumberReef;
    private int secondRandomNumberReef;

    public void Start()
    {
        ChooseSwirlSnap(nbSwirls);
        ChooseSwirlReef(nbSwirls);
    }

    public void ChooseSwirlSnap(int nb)
    {
        firstRandomNumberSnap = Random.Range(0, swirlsSnap.Count);
        secondRandomNumberSnap = Random.Range(0, swirlsSnap.Count);

        while(firstRandomNumberSnap == secondRandomNumberSnap)
        {
            secondRandomNumberSnap = Random.Range(0, swirlsSnap.Count);
        }

        swirlsSnap[firstRandomNumberSnap].SetActive(true);
        swirlsSnap[secondRandomNumberSnap].SetActive(true);
    }

    public void ChooseSwirlReef(int nb)
    {
        firstRandomNumberReef = Random.Range(0, swirlsReef.Count);
        secondRandomNumberReef = Random.Range(0, swirlsReef.Count);

        while (firstRandomNumberReef == secondRandomNumberReef)
        {
            secondRandomNumberReef = Random.Range(0, swirlsReef.Count);
        }

        swirlsReef[firstRandomNumberReef].SetActive(true);
        swirlsReef[secondRandomNumberReef].SetActive(true);
    }

    public void ActivateSwirl()
    {
        ChooseSwirlSnap(nbSwirls);
        ChooseSwirlReef(nbSwirls);
    }

    public void DesactivateSwirl()
    {
        swirlsSnap[firstRandomNumberSnap].SetActive(false);
        swirlsSnap[secondRandomNumberSnap].SetActive(false);
        swirlsReef[firstRandomNumberReef].SetActive(false);
        swirlsReef[secondRandomNumberReef].SetActive(false);
    }
}
