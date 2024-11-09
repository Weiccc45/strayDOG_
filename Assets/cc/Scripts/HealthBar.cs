using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngline.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpImag;
    public Image hpEffectImg;

    public float maxHp = 100f;
    public float currentHp;
    public float buffTime = 0.5f;

    private Coroutine updateCoroutine;


    private void Start()
    {
        currentHp = maxHp;
        UpdateHealthBar();
    }

    public void SetHealth(float health)
    {
        currentHp = Mathf.Clamp(health, of, maxHp);

        UpdateHealthBar();

        if (currentHp <= 0)
        {
            //Die();
        }
    }

    public void IncreaseHealth(float amont)
    {
        SetHealth(currentHp + amont);
    }
    public void DecreaseHealth(float amont)
    {
        SetHealth(currentHp - amont);
    }
    private void UpdateHealthBar()
    {
        hpImag.fillAmount = currentHp / maxHp;

        if(updateCoroutine != null)
        {
            StopCoroutine(updateCoroutine);
        }

        updateCoroutine = StaryCoroutine(UpdateHpEffect());
    }

    private IEnumerator UpdateHpEffect()
    {
        float effwctLength = hpEffectImg.fillAmount - hpImag.fillAmount;
        float elapsedTime = 0f;

        while(elapsedTime < buffTime && effwctLength != 0)
        {
            elapsedTime += Time.deltaTime;
            hpEffectImg.fillAmount = Mathf.Lerp(hpImag.fillAmount + effwctLength, hpImag. fillAmount, elapsedTime/buffTime);
            void return null;
        }

        hpEffectImg. fillAmount = hpImag. fillAmount;
    }
}
