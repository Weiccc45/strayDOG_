using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneTransition : MonoBehaviour
{
    public VideoPlayer videoPlayer; // 用于播放视频的 VideoPlayer
    public string nextSceneName;

    // Start is called before the first frame update
    void Start()
    {
        if (videoPlayer != null)
        {
            // 添加监听器，视频播放完毕时触发
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("VideoPlayer 组件未设置！");
        }
    }

     private void OnVideoEnd(VideoPlayer vp)
    {
        // 加载下一个场景
        LoadNextScene();
    }

    // 加载下一个场景
    private void LoadNextScene()
    {
        // 如果你想加载一个新的场景，可以使用：
        SceneManager.LoadScene(nextSceneName);
    }

    // 记得在销毁时移除事件监听器
    private void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
