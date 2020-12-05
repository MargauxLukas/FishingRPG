using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButcherManager : MonoBehaviour
{
    bool fishReadyToCut = false;
    bool cuttedFish = false;
    bool isCutting = false;

    public GameObject fishPile;
    public GameObject fishPileInput;
    public GameObject fishToButch;
    public GameObject butchedFish;
    public GameObject yInput;
    public GameObject componentsGroup;
    public GameObject firstSelectedComp;
    public GameObject dropsFirstLine;
    public GameObject dropsSecondLine;

    public List<int> numberLootList = new List<int>();
    public int totalNumberLootList = 0;

    public List<Image> dropList = new List<Image>();
    private List<float> percentDropLoot = new List<float>();
    private string fishID;
    private float totalDropLoot = 0f;
    private float randomNumber = 0;
    private FishyFiche actualFish;

    public Image holdButtonImg;
    public float cuttingTime;
    float cuttingTimer = 0;

    private void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit") && !fishReadyToCut && !cuttedFish && fishPile.activeSelf)
        {
            Debug.Log("NewFish");
            fishToButch.SetActive(true);
            fishPileInput.SetActive(false);

            fishID = UIManager.instance.inventory.GetFish();
            ShowGoodFish(fishID);

            StartCoroutine(FishCanBeCut());
        }

        if(Input.GetButton("Submit") && fishReadyToCut)
        {
            Debug.Log("Cut Fish");
            isCutting = true;

            cuttingTimer += Time.fixedDeltaTime;

            if (cuttingTimer < cuttingTime)
            {
                holdButtonImg.fillAmount = cuttingTimer / cuttingTime;
            }
            
            if(cuttingTimer >= cuttingTime)
            {
                cuttedFish = true;
                fishToButch.SetActive(false);
                butchedFish.SetActive(true);

                //Display Components (if > 3 display line2)
                HowManyLoot();
                componentsGroup.SetActive(true);

                yInput.SetActive(true);
                EventSystem.current.SetSelectedGameObject(firstSelectedComp);

                cuttingTimer = 0;
                isCutting = false;
                holdButtonImg.fillAmount = 0;
            }
        }

        if(Input.GetButtonUp("Submit") && isCutting)
        {
            Debug.Log("Release");
            cuttingTimer = 0;
            isCutting = false;
            holdButtonImg.fillAmount = 0;
        }

        if(Input.GetButtonDown("Y Button") && cuttedFish)
        {
            Debug.Log("Y");

            for (int i = 0; i < dropsFirstLine.transform.childCount; i++)
            {
                dropsFirstLine.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
            }

            for (int k = 0; k < dropsSecondLine.transform.childCount; k++)
            {
                dropsSecondLine.transform.GetChild(k).transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
            }

            //Add all components to inventory
        }

        if (Input.GetButtonDown("B Button"))
        {
            UIManager.instance.CloseMenu(gameObject);
        }
    }

    public void ClearDropList(GameObject _go)
    {
        _go.GetComponent<Image>().enabled = false;
    }

    IEnumerator FishCanBeCut()
    {
        yield return new WaitForSeconds(.5f);

        fishReadyToCut = true;
    }

    public void HowManyLoot()
    {
        foreach(int i in numberLootList)
        {
            totalNumberLootList += i;
        }

        randomNumber = Random.Range(0f, totalNumberLootList);

        for(int i = 0; i < 4; i++)
        {
            if(randomNumber < numberLootList[i])
            {
                SetDrops(3 + i);
                Debug.Log((3 + i) + " loots");
                break;
            }
            else
            {
                randomNumber -= numberLootList[i];
            }
        }
    }

    public void ShowGoodFish(string id)
    {
        percentDropLoot.Clear();
        //Afficher selon l'id le bon poisson
        if(fishID == "PQS_1")
        {
            //Afficher bonne pile
            //Mettre à jour liste de drop
            for (int i = 0; i < UIManager.instance.fishyFishList[0].drops.Length; i++)
            {
                percentDropLoot.Add(UIManager.instance.fishyFishList[0].drops[i].dropRate);
            }
            actualFish = UIManager.instance.fishyFishList[0];
        }
    }

    public void SetDrops(int nbLoot)
    {
        totalDropLoot = 0f;
        foreach(float f in percentDropLoot)
        {
            totalDropLoot += f;
        }

        for (int j = 0; j < nbLoot; j++)
        {
            randomNumber = Random.Range(0f, totalDropLoot);
            for (int i = 0; i < percentDropLoot.Count; i++)
            {
                if (randomNumber <= percentDropLoot[i])
                {
                    Debug.Log(actualFish.drops[i].name);
                    SetLoot(j,i);
                    break;
                }
                else
                {
                    randomNumber -= percentDropLoot[i];
                }
            }
        }
    }

    public void SetLoot(int j, int i)
    {
        dropList[j].sprite = actualFish.drops[i].appearance;
        dropList[j].gameObject.SetActive(true);
    }
}
