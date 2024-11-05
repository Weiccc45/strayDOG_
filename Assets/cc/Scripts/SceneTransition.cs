using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public CanvasGroup fadePanel; // 拖入用于淡入淡出的黑色面板
    public float fadeDuration = 1f; // 控制淡入淡出效果的持续时间

    private void Start()
    {
        // 初始化面板为透明状态
        if (fadePanel != null)
        {
            fadePanel.alpha = 0f;
        }
    }

    // 公共方法，用于在其他脚本中调用并传入目标场景名称
    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    // 淡入淡出协程
    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        // 执行淡入效果
        yield return StartCoroutine(FadeIn());

        // 加载指定场景
        SceneManager.LoadScene(sceneName);

        // 执行淡出效果
        yield return StartCoroutine(FadeOut());
    }

    // 淡入效果的协程
    private IEnumerator FadeIn()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadePanel.alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
            yield return null;
        }
    }

    // 淡出效果的协程
    private IEnumerator FadeOut()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadePanel.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            yield return null;
        }
    }
}
