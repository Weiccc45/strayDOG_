using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneToLoad;

     void Start()
    {
        // 确保 Video Player 播放结束时调用 VideoEnded 方法
        videoPlayer.loopPointReached += VideoEnded;
        
        // 开始播放视频
        videoPlayer.Play();
    }

    void VideoEnded(VideoPlayer vp)
    {
        // 视频播放完毕后，加载新的场景
        SceneManager.LoadScene(sceneToLoad);
    }
}
