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
    private bool hasTalkedBefore = false;

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

        // 默认值为 0（未对话）
        hasTalkedBefore = PlayerPrefs.GetInt(gameObject.name, 0) == 1;

        Debug.Log($"NPC {gameObject.name} 初始对话状态：{(hasTalkedBefore ? "已对话过" : "未对话")}");
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
            if (Input.GetKeyDown(KeyCode.Space))
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
        currentDialogueIndex++;
        List<Dialogue> currentDialogueList = GetCurrentDialogueList();

        if (currentDialogueIndex < currentDialogueList.Count)
        {
            DisplayCurrentDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    private void DisplayCurrentDialogue()
    {
        List<Dialogue> currentDialogueList = GetCurrentDialogueList();
        if (currentDialogueIndex < currentDialogueList.Count)
        {
            dialogueText.text = currentDialogueList[currentDialogueIndex].content;
            speakerNameText.text = currentDialogueList[currentDialogueIndex].speakerName;
        }
    }

    private void StartDialogue()
    {
        isTalking = true;
        if (playerController != null) playerController.isTalking = true;

        Button.SetActive(false);
        talkUI.SetActive(true);
        currentDialogueIndex = 0; // 每次对话重新从头开始
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

        Debug.Log($"开始对话：当前对话为 {(hasTalkedBefore ? "第二次对话" : "第一次对话")}");
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

        // 更新对话状态
        if (!hasTalkedBefore)
        {
            PlayerPrefs.SetInt(gameObject.name, 1);
            PlayerPrefs.Save();
            hasTalkedBefore = true;
            Debug.Log($"NPC {gameObject.name} 对话状态更新为：已对话过");
        }
    }

    private void ResetDialogue()
    {
        currentDialogueIndex = 0;
    }

    private List<Dialogue> GetCurrentDialogueList()
    {
        if (!hasTalkedBefore && dialoguesFirstTime != null && dialoguesFirstTime.Count > 0)
        {
            return dialoguesFirstTime;
        }
        else if (hasTalkedBefore && dialoguesSecondTime != null && dialoguesSecondTime.Count > 0)
        {
            return dialoguesSecondTime;
        }

        Debug.LogError($"NPC {gameObject.name} 的对话内容未正确配置！");
        return new List<Dialogue>();
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