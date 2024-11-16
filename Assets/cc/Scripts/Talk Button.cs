using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    public GameObject Button; // 設定按鈕物件
    public GameObject talkUI; // 設定對話框 UI

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Button != null) // 檢查是否已經指派 Button
        {
            Button.SetActive(true);
        }
        else
        {
            UnityEngine.Debug.LogError("Button has not been assigned in the Inspector!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (Button != null) // 檢查是否已經指派 Button
        {
            Button.SetActive(false);
        }
    }

    private void Update()
    {
        if (Button != null && Button.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            if (talkUI != null) // 檢查是否已經指派 talkUI
            {
                talkUI.SetActive(true);
            }
            else
            {
                UnityEngine.Debug.LogError("talkUI has not been assigned in the Inspector!");
            }
        }
    }
}
