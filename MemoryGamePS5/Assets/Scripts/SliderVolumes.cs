using UnityEngine;
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
        PlayerPrefs.SetFloat("soundLevel", Mathf.Log10(soundLevelSlider.value) * 20);
        //PlayerPrefs.SetFloat("soundLevel", soundLevelSlider.value);
    }
    public void SetHapticLevel(float sliderValue)
    {
        PlayerPrefs.SetFloat("hapticLevel", Mathf.Log10(hapticLevelSlider.value) * 20);
        //PlayerPrefs.SetFloat("hapticLevel", hapticLevelSlider.value);
    }
}


