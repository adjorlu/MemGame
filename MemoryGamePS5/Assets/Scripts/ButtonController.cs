using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    private Button playButtonMainMenu;
    private Button optionButtonMainMenu;
    private Button quitButtonMainMenu;

    private MainMenu mainMenu;
    [SerializeField] private GameObject optionsMenu;

    private void Start()
    {
        playButtonMainMenu = GameObject.Find("PlayButton").GetComponent<Button>();
        optionButtonMainMenu = GameObject.Find("OptionsButton").GetComponent<Button>(); 
        quitButtonMainMenu = GameObject.Find("QuitButton").GetComponent<Button>();

        mainMenu = GetComponent<MainMenu>();

    }

    private void Update()
    {
        if (playButtonMainMenu.pressed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (optionButtonMainMenu.pressed) 
        {
            mainMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(true); 

        }

        if (quitButtonMainMenu.pressed)
        {
            Debug.Log("Quit button pressed from main menu");
            Application.Quit();
        }

    }
}
