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

    public GameObject fishPileInput;
    public GameObject fishToButch;
    public GameObject butchedFish;
    public GameObject yInput;
    public GameObject componentsGroup;
    public GameObject firstSelectedComp;
    public GameObject dropsFirstLine;
    public GameObject dropsSecondLine;

    public Image holdButtonImg;
    public float cuttingTime;
    float cuttingTimer = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit") && !fishReadyToCut && !cuttedFish)
        {
            Debug.Log("NewFish");
            fishToButch.SetActive(true);
            fishPileInput.SetActive(false);
            StartCoroutine(FishCanBeCut());
        }
        else if(Input.GetButtonDown("Submit") && cuttedFish)
        {
            Debug.Log("Get component");
            //Add selected component in inventory
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
}
