using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour

{
    [Header("首頁")]
    public GameObject MenuPage;
    [Header("設定")]
    public GameObject SettingPage;
    //----------------------------
    [Header("控制音效的Slider")]
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
        //切換場景
        Application.LoadLevel("Game");
    }

    public void Quit()
    {
        //關閉遊戲
        Application.Quit();
    }

    public void SetSetting(bool Set)
    {
        //控制首頁開關
        MenuPage.SetActive(Set);
        SettingPage.SetActive(!Set);
    }

    public void SetMusicVolum()
    {
        //整體聲音的音量=聲音控制Slider的數值(值介於0-1之間)
        //AudioListener.Volum = ControlVolumSlider.value;
    }
}


