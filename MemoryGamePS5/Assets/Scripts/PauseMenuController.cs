using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    private Button menuButton;
    private Button resumeButton;
    private Button optionsButton;
    private Button quitButton;
    private Button exitButton;
    private Button saveButton;
    private Button backButton;
    private Canvas quitSaveMenu;

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject optionsMenu; 

    private void Start()
    {
        menuButton = GameObject.Find("MenuButton").GetComponent<Button>();
    }

    void Update()
    {
        if (menuButton.pressed)
        {
            Pause();

            if (resumeButton == null && optionsButton == null && quitButton == null)
            {
                resumeButton = GameObject.Find("/Canvas/PauseMenu/Menu/ResumeButton").GetComponent<Button>();
                optionsButton = GameObject.Find("/Canvas/PauseMenu/Menu/OptionsButton").GetComponent<Button>();
                quitButton = GameObject.Find("/Canvas/PauseMenu/Menu/QuitButton").GetComponent<Button>();
                exitButton = GameObject.Find("/Canvas/PauseMenu/QuitSaveMenu/ExitButton").GetComponent<Button>();
                saveButton = GameObject.Find("/Canvas/PauseMenu/QuitSaveMenu/SaveButton").GetComponent<Button>();
                quitSaveMenu = GameObject.Find("/Canvas/PauseMenu/QuitSaveMenu").GetComponent<Canvas>();
  
            }
        }

        if (GameIsPaused)
        {
            if (resumeButton.pressed)
            {
                Resume();
            }

            if (optionsButton.pressed)
            {
                menu.gameObject.SetActive(false);
                optionsMenu.gameObject.SetActive(true);

                backButton = GameObject.Find("/Canvas/PauseMenu/Options/BackButton").GetComponent<Button>();
            }

            if (backButton != null)
            {
                if (backButton.pressed)
                {
                    menu.gameObject.SetActive(true);
                    optionsMenu.gameObject.SetActive(false);
                }
            }

            if (quitButton.pressed)
            {
                menu.gameObject.SetActive(false);
                optionsMenu.gameObject.SetActive(false);
                quitSaveMenu.gameObject.SetActive(true);
            }

            if (saveButton.pressed)
            {
                saveButton.GetComponent<SaveButton>().SaveGame();
            }

            if (exitButton.pressed)
            {
                ExitGame();
            }
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        menuButton.gameObject.SetActive(true);
        GameIsPaused = false;
    }
    
    public void Pause()
    {
        menuButton.gameObject.SetActive(false);  
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        print("Game is paused!");
    }


    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
        print("Quitting game...");
    }
}
