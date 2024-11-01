using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    void Update()
    {
        Debug.Log("腳本正在運行");
    }
    public string nextSceneName;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("發生碰撞");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("玩家發生碰撞，正在加載下一個場景...");
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
