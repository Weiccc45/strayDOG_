using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour

{
    [Header("����")]
    public GameObject MenuPage;
    [Header("�]�w")]
    public GameObject SettingPage;
    //----------------------------
    [Header("����Ī�Slider")]
    public Slider ControlVolumSlider;
    [Header("AudioListener")]
    public AudioListener AudioListenerObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToGame()
    {
        //��������
        Application.LoadLevel("Game");
    }

    public void Quit()
    {
        //�����C��
        Application.Quit();
    }

    public void SetSetting(bool Set)
    {
        //������}��
        MenuPage.SetActive(Set);
        SettingPage.SetActive(!Set);
    }

    public void SetMusicVolum()
    {
        //�����n�������q=�n������Slider���ƭ�(�Ȥ���0-1����)
        //AudioListener.Volum = ControlVolumSlider.value;
    }
}


