using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("����")]
    public GameObject TipObj;
    [Header("���")]
    public GameObject DiagObj;
    //�ƹ������I��NPC,����@��

    //���a�I��NPC,�uĲ�o�{���@��
    private void OnTriggerEnter2D(Collider2D Hit)
    {
        if (Hit.name == "Player")
        {
            TipObj.SetActive(true); 
        }   
    }

    //���a�I��NPC,�åB�S�����}NPC,�o�̭����{���|����Ĳ�o
    private void OnTriggerStay2D(Collider2D Hit)
    {

    }

    //���a�I��NPC,�åB���}NPC,�uĲ�o�{���@��
    private void OnTriggerExit2D(Collider2D Hit)
    {
        if(Hit.name == "Player")
        {
            TipObj.SetActive(false);
        }
    }
}
