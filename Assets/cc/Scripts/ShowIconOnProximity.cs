using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIconOnProximity : MonoBehaviour
{
    public GameObject icon; // 关联的图案
    private bool isPlayerNearby = false;

    private void Start()
    {
        if (icon != null)
        {
            icon.SetActive(false); // 初始隐藏图案
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 检测玩家
        {
            isPlayerNearby = true;
            if (icon != null)
            {
                icon.SetActive(true); // 显示图案
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 玩家离开触发区域
        {
            isPlayerNearby = false;
            if (icon != null)
            {
                icon.SetActive(false); // 隐藏图案
            }
        }
    }
}
