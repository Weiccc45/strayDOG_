using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject talkUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 當觸發時啟動 Button
        Button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 當退出觸發區域時停用 Button
        Button.SetActive(false);
    }

    private void Update()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            talkUI.SetActive(true);
        }
    }
}
