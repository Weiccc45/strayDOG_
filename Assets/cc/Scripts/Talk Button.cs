using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    public GameObject Button;  // 按鈕提示的遊戲物件
    public GameObject talkUI;  // 對話框 UI
    private bool isPlayerInTrigger = false; // 紀錄玩家是否在觸發區域內

    private void Start()
    {
        UnityEngine.Debug.Log("[TalkButton] Script initialized.");

        if (Button == null)
        {
            UnityEngine.Debug.LogError("[TalkButton] Button is not assigned! Please assign it in the Inspector.");
        }

        if (talkUI == null)
        {
            UnityEngine.Debug.LogError("[TalkButton] talkUI is not assigned! Please assign it in the Inspector.");
        }

        if (Button != null) Button.SetActive(false);
        if (talkUI != null) talkUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        UnityEngine.Debug.Log($"[TalkButton] Trigger entered by: {other.name}");

        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            UnityEngine.Debug.Log("[TalkButton] Player entered trigger zone.");
            if (Button != null) Button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        UnityEngine.Debug.Log($"[TalkButton] Trigger exited by: {other.name}");

        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            UnityEngine.Debug.Log("[TalkButton] Player exited trigger zone.");
            if (Button != null) Button.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger)
        {
            UnityEngine.Debug.Log("[TalkButton] Player is in trigger zone.");

            if (Input.GetKeyDown(KeyCode.E))
            {
                UnityEngine.Debug.Log("[TalkButton] E key pressed.");
                if (talkUI != null)
                {
                    talkUI.SetActive(true);
                    UnityEngine.Debug.Log("[TalkButton] talkUI is now active.");
                }
                else
                {
                    UnityEngine.Debug.LogError("[TalkButton] talkUI is not assigned!");
                }
            }
        }
    }
}
