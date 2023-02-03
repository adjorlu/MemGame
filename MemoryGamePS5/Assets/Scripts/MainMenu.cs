using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
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

    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
