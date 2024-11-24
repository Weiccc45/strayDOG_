using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // �ޤJ UI �R�W�Ŷ�

public class TalkButton : MonoBehaviour
{
    public GameObject Button;          // ���s���ܪ��C������
    public GameObject talkUI;          // ��ܮ� UI
    public Text dialogueText;          // ��ܥx���� Text ����
    public Button nextButton;          // �U�@�y���s
    public List<string> dialogues;     // �x�s����x�����C��
    private int currentDialogueIndex;  // ��e�x������
    private bool isPlayerInTrigger = false; // �������a�O�_�bĲ�o�ϰ줺

    private void Start()
    {
        if (Button == null || talkUI == null || dialogueText == null || nextButton == null)
        {
            Debug.LogError("[TalkButton] Please assign all required components in the Inspector.");
        }

        // ��l�� UI ���A
        if (Button != null) Button.SetActive(false);
        if (talkUI != null) talkUI.SetActive(false);

        // ��l�ƫ��s
        if (nextButton != null) nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(DisplayNextDialogue); // �j�w���s�I���ƥ�

        // ��l�ƥx������
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
                    // ��ܹ�ܮب���ܲĤ@�y�x��
                    talkUI.SetActive(true);
                    dialogueText.text = dialogues[currentDialogueIndex];
                    if (nextButton != null) nextButton.gameObject.SetActive(true); // ��ܤU�@�y���s
                }
                else
                {
                    // �p�G��ܮؤw�g�}�ҡA�h���i�����ާ@
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
            // �p�G�x���w�g������ܧ����A������ܮبí��m�x��
            if (talkUI != null) talkUI.SetActive(false);
            if (nextButton != null) nextButton.gameObject.SetActive(false); // ���ë��s
            ResetDialogue();
        }
    }

    private void ResetDialogue()
    {
        currentDialogueIndex = 0;
    }
}
