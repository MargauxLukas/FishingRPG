using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSwirls : MonoBehaviour
{
    public List<GameObject> swirls = new List<GameObject>();
    public int nbSwirls = 2;
    private int firstRandomNumber;
    private int secondRandomNumber;

    public void Start()
    {
        ChooseSwirl(nbSwirls);
    }

    public void ChooseSwirl(int nb)
    {
        firstRandomNumber = Random.Range(0, swirls.Count);
        secondRandomNumber = Random.Range(0, swirls.Count);

        while(firstRandomNumber == secondRandomNumber)
        {
            secondRandomNumber = Random.Range(0, swirls.Count);
        }

        swirls[firstRandomNumber].SetActive(true);
        swirls[secondRandomNumber].SetActive(true);
    }

    public void DesactivateSwirl()
    {
        swirls[firstRandomNumber].SetActive(false);
        swirls[secondRandomNumber].SetActive(false);
    }
}
