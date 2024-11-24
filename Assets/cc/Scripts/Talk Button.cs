using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 引入 UI 命名空間

public class TalkButton : MonoBehaviour
{
    public GameObject Button;          // 按鈕提示的遊戲物件
    public GameObject talkUI;          // 對話框 UI
    public Text dialogueText;          // 顯示台詞的 Text 元件
    public Button nextButton;          // 下一句按鈕
    public List<string> dialogues;     // 儲存角色台詞的列表
    private int currentDialogueIndex;  // 當前台詞索引
    private bool isPlayerInTrigger = false; // 紀錄玩家是否在觸發區域內

    private void Start()
    {
        if (Button == null || talkUI == null || dialogueText == null || nextButton == null)
        {
            Debug.LogError("[TalkButton] Please assign all required components in the Inspector.");
        }

        // 初始化 UI 狀態
        if (Button != null) Button.SetActive(false);
        if (talkUI != null) talkUI.SetActive(false);

        // 初始化按鈕
        if (nextButton != null) nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(DisplayNextDialogue); // 綁定按鈕點擊事件

        // 初始化台詞索引
        currentDialogueIndex = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            if (Button != null) Button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            if (Button != null) Button.SetActive(false);

            if (talkUI != null && talkUI.activeSelf)
            {
                talkUI.SetActive(false);
                ResetDialogue();
            }
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (talkUI != null && dialogueText != null)
            {
                if (!talkUI.activeSelf)
                {
                    // 顯示對話框並顯示第一句台詞
                    talkUI.SetActive(true);
                    dialogueText.text = dialogues[currentDialogueIndex];
                    if (nextButton != null) nextButton.gameObject.SetActive(true); // 顯示下一句按鈕
                }
                else
                {
                    // 如果對話框已經開啟，則不進行任何操作
                }
            }
        }
    }

    private void DisplayNextDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueIndex < dialogues.Count)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
        }
        else
        {
            // 如果台詞已經全部顯示完畢，關閉對話框並重置台詞
            if (talkUI != null) talkUI.SetActive(false);
            if (nextButton != null) nextButton.gameObject.SetActive(false); // 隱藏按鈕
            ResetDialogue();
        }
    }

    private void ResetDialogue()
    {
        currentDialogueIndex = 0;
    }
}
