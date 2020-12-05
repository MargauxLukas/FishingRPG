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

    public List<Image> dropList = new List<Image>();                  //Image components de chaque case
    private List<float> percentDropLoot = new List<float>();          //List des percentages de loot
    private List<string> lootID = new List<string>();

    private string fishID;                                            //Savoir quel poisson on découpe
    private float totalDropLoot = 0f;                                 //Total de tous les percentages
    private float randomNumber = 0;
    private FishyFiche actualFish;                                    //Le fish choisi pour savoir quel sont les loots

    public Image holdButtonImg;
    public float cuttingTime;
    float cuttingTimer = 0;

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

            for(int i = 0; i < lootID.Count; i++)
            {
                dropList[i].enabled = false;
                UIManager.instance.inventory.AddLoot(lootID[i]);
                lootID[i] = "Empty";
            }
        }

        if (Input.GetButtonDown("B Button"))
        {
            UIManager.instance.CloseMenu(gameObject);
        }
    }

    public void ClearDropList(int i)
    {
        dropList[i].enabled = false;
        UIManager.instance.inventory.AddLoot(lootID[i]);
        lootID[i] = "Empty";
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
                    lootID.Add(actualFish.drops[i].ID);
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
