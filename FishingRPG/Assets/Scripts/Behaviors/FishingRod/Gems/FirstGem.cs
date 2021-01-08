using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstGem : MonoBehaviour
{
    private bool played = false;
    private bool playTimerDuration = false;
    private bool playTimerCD = false;

    private float timer = 0f;
    private float duration = 0f;
    private float cooldown = 0f;

    private int slot;

    private Color color;
    private float valueToAdd = 0f;

    public void FixedUpdate()
    {
        if (playTimerDuration)
        {
            timer += Time.fixedDeltaTime;
            PlayerManager.instance.UpdateUIGem(slot, timer, duration);
            if (timer > duration)
            {
                RemoveEffect();
                playTimerCD = true;
                playTimerDuration = false;
                timer = 0f;
            }
        }

        if(playTimerCD)
        {
            timer += Time.fixedDeltaTime;
            PlayerManager.instance.UpdateUIGemCD(slot, timer, cooldown);
            if (timer > cooldown)
            {
                Debug.Log("CD finish");
                played = false;
                playTimerCD = false;
                timer = 0f;
            }
        }

    }

    public void Play(Gem gem, int i)
    {
        if (!played)
        {
            duration = gem.duration;
            cooldown = gem.cooldown;
            slot = i;

            GemEffect();
            //mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0f);
            //valueToAdd = 255 / cooldown;

            played = true;
        }
    }

    public void GemEffect()
    {
        Debug.Log("Add Gem Effect");
        Debug.Log("Player Force : " + PlayerManager.instance.playerStats.strenght + " to " + (PlayerManager.instance.playerStats.strenght+1));
        Debug.Log("Player Dexterity : " + PlayerManager.instance.playerStats.dexterity + " to " + (PlayerManager.instance.playerStats.dexterity+1));

        PlayerManager.instance.playerStats.strenght += 1;
        PlayerManager.instance.playerStats.dexterity += 1;

        playTimerDuration = true;
    }

    public void RemoveEffect()
    {
        Debug.Log("Remove Gem Effect");

        PlayerManager.instance.playerStats.strenght -= 1;
        PlayerManager.instance.playerStats.dexterity -= 1;

        //StartCoroutine(FadeMat());
    }
}
