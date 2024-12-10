using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkImage : MonoBehaviour
{
    public Image image;          // 要閃爍的圖片
    public float blinkSpeed = 1f; // 閃爍速度（越小越快）
    private bool isBlinking = false;

    private void Start()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        if (image != null)
        {
            StartBlinking();
        }
        else
        {
            Debug.LogError("未找到 Image 組件！");
        }
    }

    public void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(BlinkCoroutine());
        }
    }

    public void StopBlinking()
    {
        isBlinking = false;
    }

    private IEnumerator BlinkCoroutine()
    {
        while (isBlinking)
        {
            // 逐漸減少透明度
            for (float alpha = 1f; alpha >= 0; alpha -= Time.deltaTime * blinkSpeed)
            {
                SetAlpha(alpha);
                yield return null;
            }

            // 逐漸增加透明度
            for (float alpha = 0; alpha <= 1f; alpha += Time.deltaTime * blinkSpeed)
            {
                SetAlpha(alpha);
                yield return null;
            }
        }
    }

    private void SetAlpha(float alpha)
    {
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}
