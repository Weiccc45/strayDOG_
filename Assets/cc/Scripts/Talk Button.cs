using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class TalkButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject talkUI;
    public UnityEngine.UI.Text dialogueText; // 对话内容
    public UnityEngine.UI.Text speakerNameText; // 讲话人的名字
    public Button nextButton;
    public List<Dialogue> dialoguesFirstTime; // 第一次对话内容
    public List<Dialogue> dialoguesSecondTime; // 第二次对话内容

    public Transform cameraFocusPoint; // 用于放大的焦点位置
    public float cameraTransitionTime = 0.5f;
    public CinemachineVirtualCamera talkCamera; // 对话时的镜头
    public CinemachineVirtualCamera defaultCamera;

    private int currentDialogueIndex = 0;
    private bool isPlayerInTrigger = false;
    private walkctrl playerController;
    private GameObject player;
    private Camera mainCamera;
    private Vector3 originalCameraPosition;
    private bool isTalking = false;
    private bool isWaitingForKeyPress = false; // 是否等待玩家按下按键
    private bool hasTalkedBefore; // 记录是否已经对话过

    [System.Serializable]
    public class Dialogue
    {
        public string speakerName; // 讲话人名字
        public string content;     // 对话内容
    }

    private void Start()
    {
        if (Button == null || talkUI == null || dialogueText == null || speakerNameText == null || nextButton == null || cameraFocusPoint == null)
        {
            Debug.LogError("请在 Inspector 中设置所有必需的组件！");
            return;
        }

        playerController = FindObjectOfType<walkctrl>();
        mainCamera = Camera.main;
        originalCameraPosition = mainCamera.transform.position;

        Button.SetActive(false);
        talkUI.SetActive(false);
        nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(DisplayNextDialogue);

        // 检查是否已与当前 NPC 交谈过
        hasTalkedBefore = PlayerPrefs.GetInt(gameObject.name, 0) == 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            Button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            if (Button != null)
            {
                Button.SetActive(false);
            }

            if (isTalking)
            {
                EndDialogue();
            }
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (!talkUI.activeSelf)
            {
                StartDialogue();
                DisplayCurrentDialogue();
                nextButton.gameObject.SetActive(true);
            }
        }

        if (isTalking)
        {
            if (isWaitingForKeyPress &&
                (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
            {
                isWaitingForKeyPress = false;
                currentDialogueIndex++;
                if (currentDialogueIndex < GetCurrentDialogueList().Count)
                {
                    DisplayCurrentDialogue();
                }
                else
                {
                    EndDialogue();
                }
            }
            else if (!isWaitingForKeyPress && Input.GetKeyDown(KeyCode.Space))
            {
                DisplayNextDialogue();
            }
        }

        if (isPlayerInTrigger)
        {
            Button.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(-5.8f, -2f, 0));
        }
    }

    private void DisplayNextDialogue()
    {
        if (isWaitingForKeyPress)
        {
            Debug.Log("等待玩家按下 WASD 键...");
            return;
        }

        currentDialogueIndex++;
        if (currentDialogueIndex < GetCurrentDialogueList().Count)
        {
            DisplayCurrentDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    private void StartDialogue()
    {
        isTalking = true;
        if (playerController != null) playerController.isTalking = true;

        Button.SetActive(false);
        talkUI.SetActive(true);
        DisplayCurrentDialogue();
        nextButton.gameObject.SetActive(true);

        StartCoroutine(MoveCamera(cameraFocusPoint.position));

        if (talkCamera != null)
        {
            talkCamera.Priority = 10;
        }
        if (defaultCamera != null)
        {
            defaultCamera.Priority = 0;
        }
    }

    private void EndDialogue()
    {
        isTalking = false;
        if (playerController != null) playerController.isTalking = false;

        if (talkUI != null) talkUI.SetActive(false);
        if (nextButton != null) nextButton.gameObject.SetActive(false);

        ResetDialogue();

        StartCoroutine(MoveCamera(originalCameraPosition));

        if (talkCamera != null)
        {
            talkCamera.Priority = 0;
        }
        if (defaultCamera != null)
        {
            defaultCamera.Priority = 10;
        }

        // 保存 NPC 对话状态
        if (!hasTalkedBefore)
        {
            PlayerPrefs.SetInt(gameObject.name, 1);
            PlayerPrefs.Save();
            hasTalkedBefore = true;
        }
    }

    private void ResetDialogue()
    {
        currentDialogueIndex = 0;
    }

    private IEnumerator MoveCamera(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = mainCamera.transform.position;

        while (elapsedTime < cameraTransitionTime)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / cameraTransitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
    }

    private List<Dialogue> GetCurrentDialogueList()
    {
        return hasTalkedBefore ? dialoguesSecondTime : dialoguesFirstTime;
    }

    private void DisplayCurrentDialogue()
    {
        List<Dialogue> currentDialogueList = GetCurrentDialogueList();
        Dialogue currentDialogue = currentDialogueList[currentDialogueIndex];
        dialogueText.text = currentDialogue.content;
        speakerNameText.text = currentDialogue.speakerName;
    }
}