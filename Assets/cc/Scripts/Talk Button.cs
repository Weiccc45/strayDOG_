using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    public GameObject Button; // �]�w���s����
    public GameObject talkUI; // �]�w��ܮ� UI

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Button != null) // �ˬd�O�_�w�g���� Button
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
        if (Button != null) // �ˬd�O�_�w�g���� Button
        {
            Button.SetActive(false);
        }
    }

    private void Update()
    {
        if (Button != null && Button.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            if (talkUI != null) // �ˬd�O�_�w�g���� talkUI
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
