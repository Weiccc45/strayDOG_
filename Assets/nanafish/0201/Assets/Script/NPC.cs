using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("提示")]
    public GameObject TipObj;
    [Header("對話")]
    public GameObject DiagObj;
    //滑鼠左鍵點到NPC,執行一次

    //玩家碰到NPC,只觸發程式一次
    private void OnTriggerEnter2D(Collider2D Hit)
    {
        if (Hit.name == "Player")
        {
            TipObj.SetActive(true); 
        }   
    }

    //玩家碰到NPC,並且沒有離開NPC,這裡面的程式會持續觸發
    private void OnTriggerStay2D(Collider2D Hit)
    {

    }

    //玩家碰到NPC,並且離開NPC,只觸發程式一次
    private void OnTriggerExit2D(Collider2D Hit)
    {
        if(Hit.name == "Player")
        {
            TipObj.SetActive(false);
        }
    }
}
