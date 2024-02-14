using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.MasKey("musicvolume"))
        {
            PlayerPrefs.SetFloat("musicvolum , 1");
            Load();
        }

        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicvolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicvolume", volumeSlider.value);
    }
}
