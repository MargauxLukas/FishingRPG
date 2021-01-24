using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeStaminaUI : MonoBehaviour
{
    public static LifeStaminaUI instance;

    public Image life;
    public Image lifeCD;
    public Image stamina;
    public Image staminaCD;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    public void UpdateLife(float _life)
    {
        life.fillAmount = _life;
        Debug.Log("life_ : " + _life);
        StartCoroutine(LifeCooldown(_life));
    }

    public void UpdateStamina(float _stamina)
    {
        stamina.fillAmount = _stamina;
        StartCoroutine(StaminaCooldown(_stamina));
    }

    IEnumerator LifeCooldown(float _life)
    {
        yield return new WaitForSeconds(0.5f);

        float currentAmt = lifeCD.fillAmount;
        Debug.Log("currentAmt : " + currentAmt);

        lifeCD.fillAmount = Mathf.Lerp(currentAmt, _life, 2);
    }

    IEnumerator StaminaCooldown(float _stamina)
    {
        yield return new WaitForSeconds(0.5f);

        float currentAmt = staminaCD.fillAmount;

        lifeCD.fillAmount = Mathf.Lerp(currentAmt, _stamina, 2);
    }
}
