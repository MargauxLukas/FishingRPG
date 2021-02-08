using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoManager : MonoBehaviour
{
    public static TutoManager instance;

    public GameObject tutorialGo;
    public GameObject helpInputs;
    public GameObject colliderReef;

    //UI INPUT
    public GameObject UIPressRT;

    public bool isFirstTimePlay = true;
    public bool isOnTutorial = true;
    public bool isOnDialogue = false;
    public bool textIsFinish = true;

    //Bool Condition
    public string nextText = "";
    public bool chap1 = false;
    public bool chap2 = false;
    public bool chap3 = false;
    public bool buttonBAutorisation = false;
    public bool launchingBobber = false;

    public Text dialogueText;
    public Text nameText;

    public Color speakingColor;
    public Color notSpeakingColor;

    public Image baal;
    public Image lokasse;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {

    }

    public void Update()
    {
        if (Input.GetButtonUp("A Button"))
        {
            if (textIsFinish)
            {
                DisableDialogueBox();
                isOnDialogue = false;
            }
            else
            {
                switch (nextText)
                {
                    case "c2d2":
                        Chap2Dialogue2();
                        break;
                    case "c2d3":
                        Chap2Dialogue3();
                        break;
                    case "c2d4":
                        Chap2Dialogue4();
                        break;
                    case "c3d2":
                        Chap3Dialogue2();
                        break;
                    case "c3d3":
                        Chap3Dialogue3();
                        break;
                    default:
                        break;
                }
            }
        }

        if (Input.GetButtonUp("B Button"))
        {
            switch (nextText)
            {
                case "c2Fail":
                    Chap2Dialogue4();
                    launchingBobber = false;
                    break;
                default:
                    break;
            }
        }

        if (nextText == "c2d5")
        {
            if (Input.GetAxis("Right Trigger") > 0.1f && !launchingBobber)
            {
                launchingBobber = true;
                Chap2Dialogue5();
            }
        }

        if (nextText == "c2d6" && FishingManager.instance.isOnWater)
        {
            if (!FishingManager.instance.isOnSwirl){Chap2DialogueFail();}
            else                                   { nextText = ""     ;}
        }
    }

    #region Utilities

    public void ShowDialogueBox()
    {
        isOnDialogue = true;
        tutorialGo.SetActive(true);
        helpInputs.SetActive(false);
    }

    public void DisableDialogueBox()
    {
        isOnDialogue = false;
        tutorialGo.SetActive(false);
        helpInputs.SetActive(true);
    }

    public void BaalSpeaking()
    {
        nameText.text = "Baal";
        baal.color = speakingColor;
        lokasse.color = notSpeakingColor;
    }

    public void LokasseSpeaking()
    {
        nameText.text = "Lokasse";
        lokasse.color = speakingColor;
        baal.color = notSpeakingColor;
    }

    #endregion

    #region Chapitre I

    public void Chap1Dialogue1()
    {
        isOnTutorial = true;
        ShowDialogueBox();
        BaalSpeaking();
        dialogueText.text = "Regarde, je vais te montrer comment pêcher";
    }

    #endregion

    #region Chapitre II

    public void Chap2Dialogue1()
    {
        textIsFinish = false;
        ShowDialogueBox();
        BaalSpeaking();
        nextText = "c2d2";
        dialogueText.text = "Ok, on peut commencer.";
    }

    public void Chap2Dialogue2()
    {
        nextText = "c2d3";
        dialogueText.text = "Quand On cherche à attraper un poisson, on guette les remous à la surface de l’eau";
    }

    public void Chap2Dialogue3()
    {
        nextText = "c2d4";
        dialogueText.text = "Quand on a repéré, on lance sa ligne dedans";
        chap2 = true;
    }

    public void Chap2Dialogue4()
    {
        DisableDialogueBox();
        helpInputs.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        //UIPressRT.SetActive(true);
        //Appuyer sur RT
        nextText = "c2d5";
    }

    public void Chap2Dialogue5()
    {
        //Viser un remous
        nextText = "c2d6";
    }

    public void Chap2DialogueFail()
    {
        //Appuyer sur B pour quitter la pêche
        helpInputs.transform.GetChild(1).GetChild(1).GetChild(2).gameObject.SetActive(true);
        nextText = "c2Fail";
        buttonBAutorisation = true;
    }
    #endregion

    #region Chapitre III
    public void Chap3Dialogue1()
    {
        chap3 = true;
        Time.timeScale = 0;
        //Fond noir transparent jauge
        ShowDialogueBox();
        BaalSpeaking();
        dialogueText.text = "L’objectif d’une pêche est de tuer le poisson ou de le fatiguer et de l’amener sur la rive";
        //UI vie + Endu
        nextText = "c3d2";
    }

    public void Chap3Dialogue2()
    {
        //Fond noir transparent fil
        dialogueText.text = "Attention au fil, qu’il ne casse pas. Il faut toujours garder un œil dessus.";
        //UI Fil
        nextText = "c3d3";
    }

    public void Chap3Dialogue3()
    {
        Time.timeScale = 1;
        DisableDialogueBox();
        nextText = "";
        //Enlever fond noir
        //Wait 3 seconds avant de lancer Chapitre IV
    }
    #endregion

    #region Chapitre IV
    public void Chap4Dialogue1()
    {
        //3 Capacités
        dialogueText.text = "Tu as 3 capacités.";
        nextText = "c4d2";
    }

    public void Chap4Dialogue2()
    {
        //Prendre la ligne
        dialogueText.text = "Prendre la ligne. Ramène le poisson et le fatigue, mais la ligne prend cher.";
        nextText = "c4d3";
    }

    public void Chap4Dialogue3()
    {
        //Lâcher la ligne
        dialogueText.text = "Lâcher la ligne. La tension baisse, mais le poisson est libre de bouger et peut s’éloigner de nouveau";
        nextText = "c4d4";
    }

    public void Chap4Dialogue4()
    {
        //Bloquer la ligne
        dialogueText.text = "Un entre deux : Bloquer la ligne. Fatigue peu le poisson, mais l’empêche de reprendre de la distance. La tension augmente peu";
        nextText = "c4d5";
    }

    public void Chap4Dialogue5()
    {
        dialogueText.text = "L’idée est de réussir à affaiblir le poisson sans que la ligne casse";
        nextText = "";
    }

    #endregion
}
