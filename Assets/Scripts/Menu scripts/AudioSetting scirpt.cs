using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingscirpt : MonoBehaviour
{
    public Slider volumeSlider;
    private void Start()
    {
        volumeSlider.value = AudioListener.volume;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
