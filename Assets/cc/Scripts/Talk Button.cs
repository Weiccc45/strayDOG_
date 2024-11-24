using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    public GameObject Button;  // ���s���ܪ��C������
    public GameObject talkUI;  // ��ܮ� UI
    private bool isPlayerInTrigger = false; // �������a�O�_�bĲ�o�ϰ줺

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

            // Close the talkUI when leaving the trigger zone
            if (talkUI != null && talkUI.activeSelf)
            {
                talkUI.SetActive(false);
                UnityEngine.Debug.Log("[TalkButton] talkUI deactivated as player left the trigger zone.");
            }
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger)
        {
            UnityEngine.Debug.Log("[TalkButton] Player is in trigger zone.");

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (talkUI != null)
                {
                    // Toggle talkUI's active state
                    talkUI.SetActive(!talkUI.activeSelf);
                    UnityEngine.Debug.Log($"[TalkButton] talkUI active state toggled: {talkUI.activeSelf}");
                }
                else
                {
                    UnityEngine.Debug.LogError("[TalkButton] talkUI is not assigned!");
                }
            }
        }
    }
}
