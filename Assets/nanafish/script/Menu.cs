using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        //��������
        Application.LoadLevel("Game");
    }
    public void Quit()
    {
        //�����C��
        Application.Quit();
    }
}
