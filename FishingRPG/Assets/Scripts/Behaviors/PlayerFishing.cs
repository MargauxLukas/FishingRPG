﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishing : MonoBehaviour
{
    public bool isReadyToFish = false;
    public float timeCooldownRB;
    public bool hasJustPressRB = false;

    private void Update()
    {
        if (isReadyToFish)
        {
            if (Input.GetButton("Left Bumper"))         //LB
            {
                if(FishManager.instance.isAerial && !FishManager.instance.isFelling && !FishManager.instance.currentFishBehavior.isDead)
                {
                    PlayerManager.instance.FellingFish();
                }
            }
            else if (Input.GetButtonDown("Right Bumper"))   //RB
            {
                if ((FishManager.instance.currentFishBehavior.exhausted || FishManager.instance.currentFishBehavior.isDead) && PlayerManager.instance.cfvz.isNearVictoryZone)
                {
                    FishManager.instance.SetFinishPoint();
                    //FishingRodManager.instance.fishingLine.cableComponent.DesactivateLine();
                }
                else
                {
                    if (FishManager.instance.currentFishBehavior.exhausted && !FishManager.instance.currentFishBehavior.isDead)
                    {
                        if (!FishManager.instance.isAerial) { PlayerManager.instance.IsAerial(); }
                        else
                        {
                            //REPARER je rentre dedans quand j'appuie la premiere fois pour Aerial
                            if (!hasJustPressRB)
                            {
                                PlayerManager.instance.CheckDistanceWithWater();
                                hasJustPressRB = true;
                                StartCoroutine(WaitCooldownTime());
                            }
                            else
                            {
                                //Faut il remettre le CoolDown à 0 et re-attendre 0.5f seconds ?
                                Debug.Log("Déjà appuyé sur RB y'a pas longtemps");
                            }
                        }
                    }
                }
            }

            if (Input.GetAxis("Right Trigger") > 0.1f)  //RT
            {
                PlayerManager.instance.IsTakingLine();
            }
            if (Input.GetAxis("Left Trigger") > 0.1f)   //LT
            {
                PlayerManager.instance.IsBlockingLine();
            }
            
            if(Input.GetAxis("Right Trigger") < 0.1f)
            {
                PlayerManager.instance.IsNotTakingLine();
            }

            if (Input.GetAxis("Left Trigger") < 0.1f)
            {
                PlayerManager.instance.IsNotBlockingLine();
            }

            if(Input.GetAxis("D-Pad (Horizontal)") == 1)
            {
                PlayerManager.instance.UseGemFirstSlot();
            }
            if (Input.GetAxis("D-Pad (Horizontal)") == -1)
            {
                PlayerManager.instance.UseGemSecondSlot();
            }
            if (Input.GetAxis("D-Pad (Vertical)") == 1)
            {
                PlayerManager.instance.UseGemThirdSlot();
            }

            if(Input.GetButton("A Button"))
            {
                FishingManager.instance.CancelFishing();
                FishingRodManager.instance.bobberThrowed = false;
            }

            if (Input.GetKey(KeyCode.E))
            {
                PlayerManager.instance.CHEAT_SetFishToExhausted();
            }

            if(Input.GetKey(KeyCode.D))
            {
                PlayerManager.instance.CHEAT_SetFishToDead();
            }

            if(Input.GetKeyDown(KeyCode.P))
            {
                PlayerManager.instance.CHEAT_ShowData();
            }

            FishingRodManager.instance.SetFishingRodPosition(Input.GetAxis("Right Stick (Horizontal)"));
        }
    }

    IEnumerator WaitCooldownTime()
    {
        yield return new WaitForSeconds(timeCooldownRB);
        hasJustPressRB = false;
    }
}
