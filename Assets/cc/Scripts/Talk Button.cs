using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class TalkButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject talkUI;
    public Text dialogueText;
    public Button nextButton;
    public List<string> dialogues;

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

    private void Start()
    {
        if (Button == null || talkUI == null || dialogueText == null || nextButton == null || cameraFocusPoint == null)
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
            if (Button != null) // 检查Button是否为null
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
                talkUI.SetActive(true);
                dialogueText.text = dialogues[currentDialogueIndex];
                if (nextButton != null) nextButton.gameObject.SetActive(true);
            }
        }

        if (isPlayerInTrigger)
        {
            // 确保按钮跟随 NPC 的位置
            if (Button != null)
            {
                Vector3 npcPosition = transform.position; // 获取NPC的世界坐标
                // 将NPC位置转换为屏幕坐标并增加偏移量 (例如在NPC头顶)
                Button.transform.position = Camera.main.WorldToScreenPoint(npcPosition + new Vector3(-5.3f, -2f, 0)); // 调整偏移量
            }
        }
    }

    private void StartDialogue()
    {
        isTalking = true;
        if (playerController != null) playerController.isTalking = true;

        Button.SetActive(false);
        talkUI.SetActive(true);
        dialogueText.text = dialogues[currentDialogueIndex];
        nextButton.gameObject.SetActive(true);

        StartCoroutine(MoveCameraToFocusPoint());

        if (talkCamera != null)
        {
            talkCamera.Priority = 10; // 提升优先级
        }
        if (defaultCamera != null)
        {
            defaultCamera.Priority = 0; // 降低优先级
        }
    }

    private void EndDialogue()
    {
        isTalking = false;
        if (playerController != null) playerController.isTalking = false;

        talkUI.SetActive(false);
        nextButton.gameObject.SetActive(false);
        ResetDialogue();

        StartCoroutine(MoveCameraBack());

        if (talkCamera != null)
        {
            talkCamera.Priority = 0;
        }
        if (defaultCamera != null)
        {
            defaultCamera.Priority = 10;
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
            if (talkUI != null) talkUI.SetActive(false);
            if (nextButton != null) nextButton.gameObject.SetActive(false);
            ResetDialogue();
            EndDialogue();
        }
    }

    private void ResetDialogue()
    {
        currentDialogueIndex = 0;
    }

    private IEnumerator MoveCameraToFocusPoint()
    {
        if (cameraFocusPoint == null)
        {
            Debug.LogError("请确保已设置镜头聚焦位置！");
            yield break;
        }
        Vector3 startPosition = mainCamera.transform.position;
        Vector3 endPosition = new Vector3(cameraFocusPoint.position.x, cameraFocusPoint.position.y, mainCamera.transform.position.z);

        float elapsedTime = 0f;
        while (elapsedTime < cameraTransitionTime)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / cameraTransitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = endPosition;
    }

    private IEnumerator MoveCameraBack()
    {
        Vector3 startPosition = mainCamera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < cameraTransitionTime)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition, originalCameraPosition, elapsedTime / cameraTransitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalCameraPosition;
    }
}
