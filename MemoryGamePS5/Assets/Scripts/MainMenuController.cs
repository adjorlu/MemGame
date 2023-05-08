using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    private Button playButtonMainMenu;
    private Button loadButtonMainMenu;
    private Button optionButtonMainMenu;
    private Button quitButtonMainMenu;
    private Button backButtonOptionMenu;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject loadMenu;

    private void Start()
    {

        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        loadMenu.SetActive(false);

        if (mainMenu != null)
        {
            playButtonMainMenu = GameObject.Find("PlayButton").GetComponent<Button>();
            loadButtonMainMenu = GameObject.Find("LoadButton").GetComponent<Button>();
            optionButtonMainMenu = GameObject.Find("OptionsButton").GetComponent<Button>();
            quitButtonMainMenu = GameObject.Find("QuitButton").GetComponent<Button>();
        }

            
    }

    private void Update()
    {
        if (playButtonMainMenu.pressed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (loadButtonMainMenu.pressed) 
        {
            mainMenu.gameObject.SetActive(false);
            loadMenu.gameObject.SetActive(true);
        }

        if (optionButtonMainMenu.pressed)
        {
            mainMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(true);

            backButtonOptionMenu = GameObject.Find("/Canvas/OptionsMenu/BackButton").GetComponent<Button>();

        }

        if (quitButtonMainMenu.pressed)
        {
            Debug.Log("Quit button pressed from main menu");
            Application.Quit();
        }
    

        if (backButtonOptionMenu != null)
        {
            if (backButtonOptionMenu.pressed)
            {
                mainMenu.gameObject.SetActive(true);
                optionsMenu.gameObject.SetActive(false);
                loadMenu.gameObject.SetActive(false);
            }
        }
    }
}
