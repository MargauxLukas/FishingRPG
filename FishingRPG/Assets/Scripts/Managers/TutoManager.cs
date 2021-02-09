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

    public bool isFirstTimePlay = true;
    public bool isOnTutorial = true;
    public bool isOnDialogue = false;
    public bool textIsFinish = true;

    //Bool Condition
    public string nextText = "";
    public bool chap1 = false;
    public bool chap2 = false;
    public bool chap3 = false;
    public bool chap4 = false;
    public bool buttonBAutorisation = false;
    public bool launchingBobber = false;
    public bool staminaNeedToDown = false;
    public bool fishIsDead = false;

    public bool canRebond = false;
    public bool canFell = false;
    public bool canVictory = false;

    public Text dialogueText;
    public Text nameText;

    public Color speakingColor;
    public Color notSpeakingColor;

    public Image baal;
    public Image lokasse;

    //Timer
    public float timer;

    //UI
    public Text UItext;
    public Image RTHL;
    public Image RTFishHL;
    public Image LTFishHL;
    public GameObject maskUI;
    public GameObject aerialText;
    public GameObject fellText;
    public GameObject rebondText;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        if (PlayerManager.instance.playerInventory.inventory.tutoFini)
        {
            canRebond = true;
            canFell = true;
            canVictory = true;
            fishIsDead = true;
            buttonBAutorisation = true;
            DisableDialogueBox();
            helpInputs.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            helpInputs.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
            helpInputs.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
            helpInputs.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
            helpInputs.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(true);
            helpInputs.transform.GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
            helpInputs.transform.GetChild(1).GetChild(1).GetChild(1).gameObject.SetActive(true);
            helpInputs.transform.GetChild(1).GetChild(1).GetChild(2).gameObject.SetActive(true);
            helpInputs.transform.GetChild(1).GetChild(1).GetChild(3).gameObject.SetActive(true);
        }
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
                    case "c1d2":
                        Chap1Dialogue2();
                        break;
                    case "c1d3":
                        Chap1Dialogue3();
                        break;
                    case "c1d4":
                        Chap1Dialogue4();
                        break;
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
                    case "c3d4":
                        Chap3Dialogue4();
                        break;
                    case "c3d5":
                        Chap3Dialogue5();
                        break;
                    case "c3d6":
                        Chap3Dialogue6();
                        break;
                    case "c3d7":
                        Chap3Dialogue7();
                        break;
                    case "c4d2":
                        Chap4Dialogue2();
                        break;
                    case "c4d3":
                        Chap4Dialogue3();
                        break;
                    case "c4d4":
                        Chap4Dialogue4();
                        break;
                    case "c4d5":
                        Chap4Dialogue5();
                        break;
                    case "c4d6":
                        Chap4Dialogue6();
                        break;
                    case "c5d2":
                        Chap5Dialogue2();
                        break;
                    case "c5d3":
                        Chap5Dialogue3();
                        break;
                    case "c5d5":
                        Chap5Dialogue7();
                        break;
                    case "c5d6":
                        Chap5Dialogue8();
                        break;
                    case "c5d7":
                        Chap5Dialogue9();
                        break;
                    case "c6Fail1":
                        Chap6Fail2();
                        break;
                    case "c6Fail2":
                        Chap6Fail3();
                        break;
                    case "c6Fail3":
                        Chap6Fail4();
                        break;
                    case "c6Fail4":
                        Chap6Fail5();
                        break;
                    case "c6Win1":
                        Chap6Win2();
                        break;
                    case "c6Win2":
                        Chap6Win3();
                        break;
                    case "c6Win3":
                        Chap6Win4();
                        break;
                    case "c6Win4":
                        Chap6Win5();
                        break;
                    case "c6Win5":
                        Chap6Win6();
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
            RTHL.enabled = false;
            if (!FishingManager.instance.isOnSwirl) { Chap2DialogueFail(); }
            else
            {
                UItext.text = "";
                dialogueText.text = "";
                nextText = "";
            }
        }

        if (nextText == "c4Wait")
        {
            timer += Time.deltaTime;

            if (timer > 60f || Vector3.Distance(FishingRodManager.instance.bobberPosition.transform.position, FishManager.instance.currentFish.transform.position) < 5f
                           || FishManager.instance.currentFishBehavior.currentStamina <= 0)
            {
                Chap4Dialogue8();
            }
        }

        if (nextText == "c5Wait" && Input.GetButton("Right Bumper"))
        {
            Time.timeScale = 1f;
            nextText = "c5Wait2";
        }

        if (nextText == "c5Wait3" && Input.GetButton("Right Bumper"))
        {
            Time.timeScale = 1f;
            nextText = "c5d4";
        }

        if (nextText == "c5Wait4" && Input.GetButton("Left Bumper"))
        {
            Time.timeScale = 1f;
            fellText.SetActive(false);
            nextText = "";
        }

        if (staminaNeedToDown)
        {
            FishManager.instance.currentFishBehavior.currentStamina -= 2f;
            LifeStaminaUI.instance.UpdateStamina(FishManager.instance.currentFishBehavior.currentStamina / FishManager.instance.currentFishBehavior.fishyFiche.stamina);
            FishManager.instance.currentFishBehavior.CheckStamina();
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

    public void EndTutorial()
    {
        isOnTutorial = false;
        dialogueText.text = "";
        UItext.text = "";
        PlayerManager.instance.playerInventory.inventory.tutoFini = true;
        canRebond = true;
        canFell = true;
        canVictory = true;
        fishIsDead = true;
        buttonBAutorisation = true;
        DisableDialogueBox();
        helpInputs.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        helpInputs.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        helpInputs.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
        helpInputs.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
        helpInputs.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(true);
        helpInputs.transform.GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
        helpInputs.transform.GetChild(1).GetChild(1).GetChild(1).gameObject.SetActive(true);
        helpInputs.transform.GetChild(1).GetChild(1).GetChild(2).gameObject.SetActive(true);
        helpInputs.transform.GetChild(1).GetChild(1).GetChild(3).gameObject.SetActive(true);
    }


    #endregion

    #region Chapitre I

    public void Chap1Dialogue1()
    {
        ShowDialogueBox();
        BaalSpeaking();
        textIsFinish = false;
        dialogueText.text = "Here we are! The BluePearl Bay. You will quickly understand why we call it that way.";
        nextText = "c1d2";
    }

    public void Chap1Dialogue2()
    {
        LokasseSpeaking();
        dialogueText.text = "Nice! I always wanted to see what your daily life looks like…";
        nextText = "c1d3";
    }

    public void Chap1Dialogue3()
    {
        dialogueText.text = "I stay all day long in my shop, it’s good to go out sometimes.";
        nextText = "c1d4";
    }

    public void Chap1Dialogue4()
    {
        BaalSpeaking();
        dialogueText.text = "Well, now is the best part. A fishing session! First, let’s go to the waterfront.";
        textIsFinish = true;
        nextText = "";
    }

    #endregion

    #region Chapitre II

    public void Chap2Dialogue1()
    {
        textIsFinish = false;
        ShowDialogueBox();
        BaalSpeaking();
        nextText = "c2d2";
        dialogueText.text = "So it’s begin. First: find a fish. This part is easy.";
    }

    public void Chap2Dialogue2()
    {
        nextText = "c2d3";
        dialogueText.text = "You get the eddies?";
    }

    public void Chap2Dialogue3()
    {
        nextText = "c2d4";
        dialogueText.text = "Exactly! And when you found one, you throw your bobber in it.";
        chap2 = true;
    }

    public void Chap2Dialogue4()
    {
        DisableDialogueBox();
        helpInputs.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        RTHL.enabled = true;
        UItext.text = "Appuyer sur RT pour préparer le lancer";
        nextText = "c2d5";
    }

    public void Chap2Dialogue5()
    {
        UItext.text = "Viser un remous et relâcher RT pour lancer";
        nextText = "c2d6";
    }

    public void Chap2DialogueFail()
    {
        UItext.text = "Appuyez sur B pour quitter la pêche";
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
        ShowDialogueBox();
        BaalSpeaking();
        dialogueText.text = "To catch a fish, I have 2 options: tire the fish to bring it on the sand or kill it.";
        nextText = "c3d2";
    }

    public void Chap3Dialogue2()
    {
        LokasseSpeaking();
        dialogueText.text = "Kill it?";
        nextText = "c3d3";
    }

    public void Chap3Dialogue3()
    {
        BaalSpeaking();
        dialogueText.text = "When the fish is too heavy or energic, it’s simpler. Anyway, I need to keep an eye on the tension of the rope.";
        nextText = "c3d4";
    }

    public void Chap3Dialogue4()
    {
        dialogueText.text = "If the rope breaks, it’s the end. The idea is simple: tire the fish without breaking the rope.";
        nextText = "c3d5";
    }

    public void Chap3Dialogue5()
    {
        LokasseSpeaking();
        dialogueText.text = "Aaaaand you’re welcome for the enchantment.";
        nextText = "c3d6";
    }

    public void Chap3Dialogue6()
    {
        BaalSpeaking();
        dialogueText.text = " Indeed! The color helps a lot.";
        nextText = "c3d7";
    }

    public void Chap3Dialogue7()
    {
        Time.timeScale = 1;
        DisableDialogueBox();
        nextText = "";
        chap3 = true;
        TutoManager.instance.maskUI.SetActive(false);

        StartCoroutine(WaitChapter4());
    }

    IEnumerator WaitChapter4()
    {
        yield return new WaitForSeconds(3f);

        Chap4Dialogue1();
    }
    #endregion

    #region Chapitre IV
    public void Chap4Dialogue1()
    {
        ShowDialogueBox();
        dialogueText.text = "I have 3 main possible actions…";
        nextText = "c4d2";
    }

    public void Chap4Dialogue2()
    {
        helpInputs.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
        dialogueText.text = "First, I can Take the line. It brings back the fish and tire it a lot, but the rope will have a hard time.";
        nextText = "c4d3";
    }

    public void Chap4Dialogue3()
    {
        dialogueText.text = "Secondly I can Let the line go. The tension goes down, but the fish is free and can fly away very easily.";
        nextText = "c4d4";
    }

    public void Chap4Dialogue4()
    {
        helpInputs.transform.GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
        dialogueText.text = "But I also can just Block the line. The tension will rise a little, but the fish will not be able to go away. It’s very useful when it’s moving too fast.";
        nextText = "c4d5";
    }

    public void Chap4Dialogue5()
    {
        dialogueText.text = "L’idée est de réussir à affaiblir le poisson sans que la ligne casse";
        nextText = "c4d6";
    }

    public void Chap4Dialogue6()
    {
        DisableDialogueBox();
        nextText = "";
        RTFishHL.enabled = true;
        LTFishHL.enabled = true;
        UItext.text = "Lacher ces 2 boutons pour lâcher la ligne.";

        StartCoroutine(WaitChap4Dialogue7());
    }

    IEnumerator WaitChap4Dialogue7()
    {
        yield return new WaitForSeconds(3f);
        RTFishHL.enabled = false;
        LTFishHL.enabled = false;
        Chap4Dialogue7();
    }

    public void Chap4Dialogue7()
    {
        UItext.text = "Essayer de fatiguer le poisson.";
        nextText = "c4Wait";
    }

    public void Chap4Dialogue8()
    {
        nextText = "";
        chap4 = true;
        UItext.text = "";
        if (FishManager.instance.currentFishBehavior.currentStamina > 0)
        {
            staminaNeedToDown = true;
        }
    }

    #endregion

    #region Chapitre V

    public void Chap5Dialogue1()
    {
        Time.timeScale = 0f;
        fishIsDead = true;
        ShowDialogueBox();
        BaalSpeaking();
        dialogueText.text = "The fish isn’t moving, it’s exhausted. While it can’t move, I can throw it in the air to hurt him.";
        nextText = "c5d2";

    }

    public void Chap5Dialogue2()
    {
        LokasseSpeaking();
        dialogueText.text = "Ooook, so that’s how you kill him. I see…";
        nextText = "c5d3";
    }

    public void Chap5Dialogue3()
    {
        DisableDialogueBox();
        helpInputs.transform.GetChild(1).GetChild(1).GetChild(1).gameObject.SetActive(true);
        aerialText.SetActive(true);
        nextText = "c5Wait";
    }

    public void Chap5Dialogue4()
    {
        nextText = "c5Wait3";
        canRebond = true;
        aerialText.SetActive(false);
        rebondText.SetActive(true);
    }

    public void Chap5Dialogue6()
    {
        ShowDialogueBox();
        BaalSpeaking();
        dialogueText.text = "When the fish is in the air I can also slap it on the surface of the water.";
        nextText = "c5d5";
        rebondText.SetActive(false);
    }

    public void Chap5Dialogue7()
    {
        LokasseSpeaking();
        dialogueText.text = "What’s the difference?";
        nextText = "c5d6";
    }

    public void Chap5Dialogue8()
    {
        BaalSpeaking();
        dialogueText.text = "Felling the fish deals a lot of damage but you can’t make it bounce. So the best strategy is to keep it a little bit in the air before felling it to inflict as much damage as possible.";
        nextText = "c5d7";
    }

    public void Chap5Dialogue9()
    {
        DisableDialogueBox();
        canFell = true;
        helpInputs.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
        nextText = "c5Wait4";
        fellText.SetActive(true);
        canVictory = true;
    }


    #endregion

    #region Chapitre VI

    public void Chap6Dialogue1()
    {
        UItext.text = "Essayer de terminer la pêche.";
        nextText = "c6Wait";
    }

    public void Chap6Fail1()
    {
        ShowDialogueBox();
        BaalSpeaking();
        dialogueText.text = "Aaaaand that’s how you DON’T catch a SnapSnack.";
        nextText = "c6Fail1";
    }

    public void Chap6Fail2()
    {
        LokasseSpeaking();
        dialogueText.text = "I see… Interesting method…";
        nextText = "c6Fail2";
    }

    public void Chap6Fail3()
    {
        BaalSpeaking();
        dialogueText.text = " I have to be careful not to empty the Bay, or you will not have any materials for your craft.";
        nextText = "c6Fail3";
    }

    public void Chap6Fail4()
    {
        LokasseSpeaking();
        dialogueText.text = "Oooof course...";
        nextText = "c6Fail4";
    }

    public void Chap6Fail5()
    {
        textIsFinish = true;
        nextText = "";
        EndTutorial();
    }

    //Win
    public void Chap6Win1()
    {
        ShowDialogueBox();
        BaalSpeaking();
        dialogueText.text = "And that’s how you get a beautiful SnapSnack.";
        nextText = "c6Win1";
    }

    public void Chap6Win2()
    {
        LokasseSpeaking();
        dialogueText.text = "He seems not that dangerous.";
        nextText = "c6Win2";
    }

    public void Chap6Win3()
    {
        BaalSpeaking();
        dialogueText.text = " Yeah, this one was pretty calm. But be careful with them. Dossam almost lost a few fingers with them.";
        nextText = "c6Win3";
    }

    public void Chap6Win4()
    {
        LokasseSpeaking();
        dialogueText.text = " I understand why. But that seems cool. Can I try?";
        nextText = "c6Win4";
    }

    public void Chap6Win5()
    {
        BaalSpeaking();
        dialogueText.text = " Of course...";
        nextText = "c6Win5";
    }

    public void Chap6Win6()
    {
        textIsFinish = true;
        nextText = "";
        EndTutorial();
    }
}
    #endregion