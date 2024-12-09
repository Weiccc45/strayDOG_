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
    public List<Dialogue> dialogues; // 存储对话和讲者的名字

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
            UnityEngine.Debug.LogError("请在 Inspector 中设置所有必需的组件！");
            return;
        }

        playerController = FindObjectOfType<walkctrl>();
        mainCamera = Camera.main;
        originalCameraPosition = mainCamera.transform.position;

        Button.SetActive(false);
        talkUI.SetActive(false);
        nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(DisplayNextDialogue);
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
                dialogueText.text = dialogues[currentDialogueIndex].content;
                speakerNameText.text = dialogues[currentDialogueIndex].speakerName;
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
                if (currentDialogueIndex < dialogues.Count)
                {
                    dialogueText.text = dialogues[currentDialogueIndex].content;
                    speakerNameText.text = dialogues[currentDialogueIndex].speakerName;
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
        // 如果在等待按键，则不执行对话跳转
        if (isWaitingForKeyPress)
        {
            UnityEngine.Debug.Log("等待玩家按下 WASD 键...");
            return;
        }

        currentDialogueIndex++;
        if (currentDialogueIndex < dialogues.Count)
        {
            dialogueText.text = dialogues[currentDialogueIndex].content;
            speakerNameText.text = dialogues[currentDialogueIndex].speakerName;

            // 如果对话内容为特定文字，进入按键等待模式
            if (dialogues[currentDialogueIndex].content == "Element 12")
            {
                isWaitingForKeyPress = true;
                UnityEngine.Debug.Log("进入等待模式：按 W, A, S, 或 D 来继续。");
            }
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
        dialogueText.text = dialogues[currentDialogueIndex].content;
        speakerNameText.text = dialogues[currentDialogueIndex].speakerName;
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
}