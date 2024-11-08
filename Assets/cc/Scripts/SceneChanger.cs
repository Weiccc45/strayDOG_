using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter2D(Collider2D other)
    {
        // 检查碰撞对象是否是玩家
        if (other.CompareTag("Player"))
        {
            // 跳转到指定场景
            SceneManager.LoadScene(sceneName);
        }
    }
}
