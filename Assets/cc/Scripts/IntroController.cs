using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        public string speakerName; // 讲话人名字
        public string content;    // 对话内容
    }

    public Text speakerNameText;      // 讲话人的名字
    public Text dialogueContentText;  // 对话内容
    public List<Dialogue> dialogues; // 对话列表
    public float dialogueDelay = 3f;  // 每段对话之间的延迟时间

    public VideoPlayer backgroundVideoPlayer; // 背景视频播放器
    public float videoDelayBeforeDialogue = 3f; // 视频播放后的延迟时间
    public string nextSceneName; // 下一个场景的名称
    public GameObject dialogueBox;

    private int currentDialogueIndex = 0;
    private bool isDialogueActive = false;
    private Coroutine dialogueCoroutine;

    private void Start()
    {
        if (speakerNameText == null || dialogueContentText == null || backgroundVideoPlayer == null || dialogueBox == null)
        {
            Debug.LogError("请确保已在 Inspector 中设置讲话人名字、对话内容、背景视频播放器和对话框！");
            return;
        }

        // 确保对话框在开始前隐藏
        dialogueBox.SetActive(false);

        // 确保背景视频循环播放
        backgroundVideoPlayer.isLooping = true;

        // 开始播放视频后延迟启动对话
        StartCoroutine(DelayStartDialogue());
    }

    private void Update()
    {
        if (!isDialogueActive)
            return;

        // 按空白键显示下一页
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextDialogue();
        }

        // 按下 ESC 跳过所有对话并切换场景
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SkipDialogue();
        }
    }

    private IEnumerator DelayStartDialogue()
    {
        // 等待视频播放 3 秒
        yield return new WaitForSeconds(videoDelayBeforeDialogue);

        // 显示对话框
        dialogueBox.SetActive(true);

        // 开始对话
        dialogueCoroutine = StartCoroutine(StartDialogue());
    }

    private IEnumerator StartDialogue()
    {
        isDialogueActive = true;

        while (currentDialogueIndex < dialogues.Count)
        {
            DisplayCurrentDialogue();
            yield return new WaitForSeconds(dialogueDelay);
            currentDialogueIndex++;
        }

        EndDialogue();
    }

    private void DisplayCurrentDialogue()
    {
        Dialogue currentDialogue = dialogues[currentDialogueIndex];
        speakerNameText.text = currentDialogue.speakerName;
        dialogueContentText.text = currentDialogue.content;
    }

    private void DisplayNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Count - 1)
        {
            currentDialogueIndex++;
            DisplayCurrentDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    private void SkipDialogue()
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }

        EndDialogue();

        // 直接跳转到下一个场景
        LoadNextScene();
    }

    private void EndDialogue()
    {
        isDialogueActive = false;

        // 清空对话框内容
        speakerNameText.text = "";
        dialogueContentText.text = "";

        // 停止视频循环播放并且直接结束
        if (backgroundVideoPlayer != null)
        {
            backgroundVideoPlayer.isLooping = false; // 停止循环
            backgroundVideoPlayer.Stop();  // 停止视频播放
        }

        // 直接调用视频播放结束后跳转到下一个场景
        LoadNextScene();
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // 视频结束后切换到下一个场景
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Debug.Log("正在加载下一个场景: " + nextSceneName);
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("未设置下一个场景的名称！");
        }
    }
}
