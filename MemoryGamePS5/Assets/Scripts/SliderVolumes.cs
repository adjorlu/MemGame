using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SliderVolumes : MonoBehaviour
{
    public Slider soundLevelSlider;
    public Slider hapticLevelSlider;


    private void Start()
    {
        soundLevelSlider.value = PlayerPrefs.GetFloat("soundLevel");
        hapticLevelSlider.value = PlayerPrefs.GetFloat("hapticLevel");
    }

    public void SetSoundLevel(float sliderValue)
    {
        PlayerPrefs.SetFloat("soundLevel", soundLevelSlider.value);
    }
    public void SetHapticLevel(float sliderValue)
    {
        PlayerPrefs.SetFloat("hapticLevel", hapticLevelSlider.value);
    }
}
